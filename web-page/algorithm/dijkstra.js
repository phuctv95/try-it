function dijkstraDemo(numberOfNodes, svgId, width, height) {
    let graph = new Graph();
    graph.generateRandomNodesAndNeighbor(numberOfNodes, width, height);
    graph.setARandomStartNode();
    graph.dijkstra();
    graph.draw(svgId, width, height);
};

class Graph {
    get connections() {
        let connections = this.nodes[0].neighbors.map(nei => new Object({first: this.nodes[0], second: nei}));
        for (let i = 1; i < this.nodes.length; i++) {
            const node = this.nodes[i];
            node.neighbors.forEach(nei => {
                let conn = {first: node, second: nei};
                let connectExist = !!connections.find(_conn =>
                    (_conn.first.id === conn.first.id && _conn.second.id === conn.second.id)
                    || (_conn.first.id === conn.second.id && _conn.second.id === conn.first.id)
                );
                if (!connectExist) {
                    connections.push(conn);
                }
            });
        }
        return connections;
    }
    constructor(canvasId) {
        this.nodes = null;
        this.startNode = null;
        this.canvasId = canvasId;
        this.NODE_RADIUS = 5;
        this.CONNECTION_COLOR = 'black';
        this.CONNECTION_COLOR_MOUSEOVER = 'yellow';
    }
    generateRandomNodesAndNeighbor(numberOfNodes, width, height) {
        this.nodes = [];
        for (let i = 0; i < numberOfNodes; i++) {
            this.nodes.push(new DijkstraNode(i, random(0, width), random(0, height)));
        }
    
        for (let i = 0; i < numberOfNodes; i++) {
            const node = this.nodes[i], maxNeighbor = 3;
            const numOfNeighbor = random(1, maxNeighbor);
            for (let j = node.neighbors.length + 1; j <= numOfNeighbor; j++) {
                let neighbor = this.getARandomNeighbor(node, maxNeighbor);
                if (!neighbor) { continue; }
                node.neighbors.push(neighbor);
                neighbor.neighbors.push(node);
            }
        }
    }
    setARandomStartNode() {
        this.startNode = this.nodes[random(0, this.nodes.length - 1)];
    }
    dijkstra() {
        let q = [];
        this.nodes.forEach(node => {
            node.dist = Number.MAX_VALUE;
            q.push(node);
        });
        this.startNode.dist = 0;
    
        while (q.length > 0) {
            if (!q.find(x => x.dist !== Number.MAX_VALUE)) { break; }

            let u = this.getNodeHasMinDistance(q);
            q = q.filter(node => node !== u);
            u.neighbors
                .filter(nei => !!q.find(x => x.id === nei.id))
                .forEach(nei => {
                    let dist = u.dist + u.distanceTo(nei);
                    if (dist < nei.dist) {
                        nei.dist = dist;
                        nei.previous = u;
                    }
                });
        }
    }
    draw(svgId, width, height) {
        let svg = document.querySelector(`#${svgId}`);
        svg.setAttribute('width', `${width}px`);
        svg.setAttribute('height', `${height}px`);
        let connections = this.connections;
        svg.innerHTML = '';
        connections.forEach(conn => {
            let connEle = this.drawConnection(svg, conn);
            conn.htmlEle = connEle;
        });
        this.nodes.forEach(node => {
            let nodeEle = this.drawNode(svg, node);
            this.addMouseBehaviorToNode(node, connections, nodeEle);
        });
    }
    getARandomNeighbor(node, maxNeighbor) {
        let listNodesToGet = this.nodes
            .filter(x => x.id !== node.id)
            .filter(x => x.neighbors.length < maxNeighbor)
            .filter(x => !node.hasNeighbor(x));
        if (listNodesToGet.length === 0) { return null; }
        if (listNodesToGet.length === 1) { return listNodesToGet[0]; }
        let index = random(0, listNodesToGet.length - 1);
        return listNodesToGet[index];
    }
    getNodeHasMinDistance(nodes) {
        let min = Number.MAX_VALUE, result = null;
        nodes.forEach(node => {
            if (node.dist < min) {
                min = node.dist;
                result = node;
            }
        });
        return result;
    }
    drawNode(svg, node) {
        let nodeEle = document.createElementNS('http://www.w3.org/2000/svg', 'circle');
        nodeEle.setAttribute('cx', `${node.x}`);
        nodeEle.setAttribute('cy', `${node.y}`);
        nodeEle.setAttribute('r', `${this.NODE_RADIUS}`);
        nodeEle.setAttribute('fill', node === this.startNode ? 'yellow' : `white`);
        svg.appendChild(nodeEle);
        // let textEle = document.createElementNS('http://www.w3.org/2000/svg', 'text');
        // textEle.setAttribute('x', `${node.x - 3}`);
        // textEle.setAttribute('y', `${node.y - 10}`);
        // textEle.setAttribute('fill', `black`);
        // textEle.textContent = `${node.id}`;
        // svg.appendChild(textEle);
        return nodeEle;
    }
    drawConnection(svg, conn) {
        let connEle = document.createElementNS('http://www.w3.org/2000/svg', 'line');
        connEle.setAttribute('x1', `${conn.first.x}`);
        connEle.setAttribute('y1', `${conn.first.y}`);
        connEle.setAttribute('x2', `${conn.second.x}`);
        connEle.setAttribute('y2', `${conn.second.y}`);
        connEle.setAttribute('style', `stroke: ${this.CONNECTION_COLOR};`);
        svg.appendChild(connEle);
        return connEle;
    }
    addMouseBehaviorToNode(node, connections, nodeEle) {
        nodeEle.addEventListener('mouseover', () => {
            nodeEle.setAttribute('r', `${this.NODE_RADIUS + 3}`);

            if (node === this.startNode) { return; }
            let path = [node], nodeNow = node;
            while (nodeNow.previous) {
                path.push(nodeNow.previous);
                nodeNow = nodeNow.previous;
            }
            for (let i = 1; i < path.length; i++){
                const conn = {first: path[i - 1], second: path[i]};
                const connEle = connections.find(_conn =>
                    (_conn.first.id === conn.first.id && _conn.second.id === conn.second.id)
                    || (_conn.first.id === conn.second.id && _conn.second.id === conn.first.id)
                ).htmlEle;
                connEle.setAttribute('style', `stroke: ${this.CONNECTION_COLOR_MOUSEOVER};`);
            }
        });
        nodeEle.addEventListener('mouseout', () => {
            nodeEle.setAttribute('r', `${this.NODE_RADIUS}`);

            if (node === this.startNode) { return; }
            connections.forEach(conn => {
                conn.htmlEle.setAttribute('style', `stroke: ${this.CONNECTION_COLOR};`);
            });
        });
    }
}

class Node {
    constructor(id, x, y) {
        this.id = id;
        this.x = x;
        this.y = y;
        this.neighbors = [];
    }
    hasNeighbor(node) {
        return !!this.neighbors.find(x => x.id === node.id);
    }
    distanceTo(node) {
        let a = this.x - node.x;
        let b = this.y - node.y;
        return Math.sqrt(a*a + b*b);
    }
}

class DijkstraNode extends Node {
    constructor(id, x, y) {
        super(id, x, y);
        this.dist = null;
        this.previous = null;
    }
}