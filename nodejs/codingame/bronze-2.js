const tool = {
    random: (min, max) => {
        min = Math.ceil(min);
        max = Math.floor(max);
        return Math.floor(Math.random() * (max - min + 1)) + min;
    },
    containsPac: (list, pac) => {
        return !!list.find(item => item.x === pac.x && item.y === pac.y);
    },
    distance: (p1, p2) => {
        let a = p1.x - p2.x;
        let b = p1.y - p2.y;
        return Math.sqrt(a * a + b*b);
    },
    samePlace: (p1, p2) => { return p1.x === p2.x && p1.y === p2.y; }
};
class Game {
    get bigPellets() { return this.pellets.filter(p => p.isTheBig()); }
    constructor() {
        this.initGame = true;
        this.w = null;
        this.h = null;
        this.rows = [];
        this.numOfPacs = null;
        this.visiblePelletCount = null;
        this.pellets = null;
        this.pacTypes = ['ROCK', 'PAPER', 'SCISSORS'];
        this.cache = {};
    }
    doForEachFrame(me, comp) {
        // Strategy:
        // - find the nearest big pellets if still remaining, and trigger speed
        // - find nearest pellets if big pellets not available anymore
        // - go to a random place to find another pellet if don't see any pellets in current view
        // - if face teammate, turn back
        // - if near a competitor
        //     + if it's in the can-not-switch time, switch to a opposite type
        //     + otherwise, switch to a random type// MAIN
        me.pacs.forEach(myPac => {
            myPac.findNearestBigPelletAndTriggerSpeed(this);
            myPac.findNearestNormalPelletIfBigPelletOver(this);
            // myPac.goRandomPlaceIfDontFindAnyPelletInCurrentView();
            // myPac.turnBackIfFaceTeammate();
            // myPac.switchTypeIfNearCompetitor();
        });
        this.writeOutput(me.pacs);
    }
    writeOutput(pacs) {
        let cmds = [];
        pacs.forEach(pac => {
            if (pac.targetMove) {
                cmds.push(`MOVE ${pac.id} ${pac.targetMove.x} ${pac.targetMove.y}`);
            } else if (pac.triggerSpeed) {
                cmds.push(`SPEED ${pac.id}`);
            } else if (pac.targetSwitch) {
                cmds.push(`SPEED ${pac.id} ${pac.targetSwitch}`);
            }
        });
        console.log(cmds.join(' | '));
    }
    findNearestBigPelletNotAssignYet(pac) {
        let min = Number.MAX_VALUE, iMin = null;
        for (let i = 0; i < this.bigPellets.length; i++) {
            let bigPellet = this.bigPellets[i];
            if (bigPellet.pac) { continue; }
            let d = tool.distance(pac, bigPellet);
            if (d < min) {
                min = d;
                iMin = i;
            }
        }
        if (iMin === null) { return null; }
    
        this.bigPellets[iMin].pac = pac;
        return this.bigPellets[iMin];
    }
    bigPelletAvailable() { return this.bigPellets.length > 0; }
    findPellet(place) { return this.pellets.find(pellet => pellet.x === place.x && pellet.y === place.y); }
    isWall(place) { return this.rows[place.y][place.x] === '#'; }
}
class Player {
    constructor() {
        this.pacs = null;
        this.score = null;
    }
}
class Pac {
    constructor() {
        this.id = null;
        this.x = null;
        this.y = null;
        this.type = null;
        this.speedTurnsLeft = null;
        this.abilityCooldown = null;
        this.targetMove = null;
        this.targetSwitch = null;
        this.triggerSpeed = false;
    }
    findNearestBigPelletAndTriggerSpeed(game) {
        const nearestBigPelletNotAssignYet = game.findNearestBigPelletNotAssignYet(this);
        if (!nearestBigPelletNotAssignYet) { return; }

        if (this.notTriggerSpeedYet(game)) {
            this.triggerSpeed = true;
            this.setTriggerSpeedCache(game);
        } else {
            this.targetMove = nearestBigPelletNotAssignYet;
        }
    }
    notTriggerSpeedYet(game) {
        return !game.cache[`SPEED_${this.id}`];
    }
    setTriggerSpeedCache(game) {
        game.cache[`SPEED_${this.id}`] = true;
    }
    findNearestNormalPelletIfBigPelletOver(game) {
        if (game.bigPelletAvailable()) { return; }
        
        function allNextPlaces(place, game) {
            let l = {x: place.x - 1, y: place.y};
            if (l.x < 0) { l = {x: game.w - 1, y: place.y}; }
            let r = {x: place.x + 1, y: place.y};
            if (r.x > game.w - 1) { r = {x: 0, y: place.y}; }
            let t = {x: place.x, y: place.y - 1};
            if (t.y < 0) { t = null; }
            let b = {x: place.x, y: place.y + 1};
            if (b.y > game.h - 1) { b = null; }
    
            let result = [];
            if (!game.isWall(l)) { result.push(l); }
            if (!game.isWall(r)) { result.push(r); }
            if (t && !game.isWall(t)) { result.push(t); }
            if (b && !game.isWall(b)) { result.push(b); }
            return result;
        }

        let cur = this;
        let markedPlaces = [cur];
        let queue = [cur];
        while (queue.length > 0) {
            let place = queue.shift();
            let pellet = game.findPellet(place);
            if (pellet) {
                this.targetMove = pellet;
                return;
            }
            allNextPlaces(place, game)
                .forEach(nextPlace => {
                    if (!markedPlaces.find(markedPlace => tool.samePlace(markedPlace, nextPlace))) {
                        markedPlaces.push(nextPlace);
                        queue.push(nextPlace);
                    }
                });
        }
    }
}
class Pellet {
    constructor() {
        this.x = null;
        this.y = null;
        this.value = null;
        this.pac = null;
    }
    isTheBig() { return this.value && this.value === 10; }
}

const game = new Game();
const me = new Player();
const comp = new Player();

var inputs = readline().split(' ');
game.w = parseInt(inputs[0]); // size of the grid
game.h = parseInt(inputs[1]); // top left corner is (x=0, y=0)
for (let i = 0; i < game.h; i++) {
    game.rows.push(readline().split('')); // one line of the grid: space " " is floor, pound "#" is wall
}

while (true) {
    var inputs = readline().split(' ');
    me.score = parseInt(inputs[0]);
    comp.score = parseInt(inputs[1]);
    game.numOfPacs = parseInt(readline()); // all your pacs and enemy pacs in sight
    me.pacs = [];
    comp.pacs = [];
    for (let i = 0; i < game.numOfPacs; i++) {
        var inputs = readline().split(' ');
        let pac = new Pac();
        pac.id = parseInt(inputs[0]); // pac number (unique within a team)
        pac.x = parseInt(inputs[2]); // position in the grid
        pac.y = parseInt(inputs[3]); // position in the grid
        pac.type = inputs[4]; // unused in wood leagues
        pac.speedTurnsLeft = parseInt(inputs[5]); // unused in wood leagues
        pac.abilityCooldown = parseInt(inputs[6]); // unused in wood leagues
        const mine = inputs[1] !== '0'; // true if this pac is yours
        if (mine)
            { me.pacs.push(pac); }
        else
            { comp.pacs.push(pac); }
    }
    game.visiblePelletCount = parseInt(readline()); // all pellets in sight
    game.pellets = [];
    for (let i = 0; i < game.visiblePelletCount; i++) {
        var inputs = readline().split(' ');
        let pellet = new Pellet();
        pellet.x = parseInt(inputs[0]);
        pellet.y = parseInt(inputs[1]);
        pellet.value = parseInt(inputs[2]); // amount of points this pellet is worth
        game.pellets.push(pellet);
    }

    game.doForEachFrame(me, comp);
}
