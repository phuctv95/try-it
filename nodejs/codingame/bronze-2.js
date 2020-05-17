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
        this.floors = [];
    }
    doForEachFrame(me, comp) {
        // Strategy:
        // - find the nearest big pellets if still remaining, and trigger speed
        // - find nearest pellets if big pellets not available anymore
        // - go to a random place (but not visited yet) if this pac be blocked (face a teammate or didn't find any pellet in view)
        // - while going to a random place, if see a pellet, set target to that
        // - if near a competitor
        //     + if it's in the can-not-switch time, switch to a opposite type
        //     + otherwise, switch to a random type// MAIN
        me.pacs.forEach(myPac => {
            myPac.resetTargetIfAlreadyCameTarget();
            if (!myPac.targetMove) {
                myPac.findNearestBigPelletAndTriggerSpeed(this, me, false);
                myPac.findNearestNormalPelletInViewIfNotSetTargetYet(this);
            }
            myPac.goToARandomPlaceButNotVisitYetIfIsBlocking(this, me);
            myPac.findNearestNormalPelletInViewWhileGoToRandomPlace(this);
            myPac.switchTypeIfNearCompetitor(this, comp);
        });
        this.writeOutput(me.pacs);
    }
    writeOutput(pacs) {
        let cmds = [];
        pacs.forEach(pac => {
            if (pac.targetMove && !pac.triggerSpeed && !pac.targetSwitch) {
                cmds.push(`MOVE ${pac.id} ${pac.targetMove.x} ${pac.targetMove.y}`);
            } else if (pac.triggerSpeed) {
                cmds.push(`SPEED ${pac.id}`);
                pac.triggerSpeed = false;
            } else if (pac.targetSwitch) {
                cmds.push(`SWITCH ${pac.id} ${pac.targetSwitch}`);
                pac.targetSwitch = null;
            }
        });
        console.log(cmds.join(' | '));
    }
    findNearestBigPelletNotAssignYet(pac, me) {
        let min = Number.MAX_VALUE, iMin = null;
        for (let i = 0; i < this.bigPellets.length; i++) {
            let bigPellet = this.bigPellets[i];
            if (me.bigPelletAssigned(bigPellet)) { continue; }
            let d = tool.distance(pac, bigPellet);
            if (d < min) {
                min = d;
                iMin = i;
            }
        }
        if (iMin === null) { return null; }
    
        pac.bigPellet = this.bigPellets[iMin];
        return this.bigPellets[iMin];
    }
    bigPelletAvailable() { return this.bigPellets.length > 0; }
    findPellet(place) { return this.pellets.find(pellet => pellet.x === place.x && pellet.y === place.y); }
    isWall(place) { return this.rows[place.y][place.x] === '#'; }
    getRandomPlaceButNotVisitYet() {
        return this.notVisitedFloors[tool.random(0, this.notVisitedFloors.length - 1)];
    }
    initFloors(rows) {
        this.floors = [];
        for (let i = 0; i < rows.length; i++) {
            for (let j = 0; j < rows[i].length; j++) {
                let item = rows[i][j];
                if (item === ' ') {
                    this.floors.push({x: j, y: i});
                }
            }
        }
        this.notVisitedFloors = [...this.floors];
    }
    updateNotVisitedFloors(pac) {
        let foundIndex = this.notVisitedFloors.findIndex(f => f.x === pac.x && f.y === pac.y);
        if (foundIndex > -1) {
            this.notVisitedFloors.splice(foundIndex, 1);
        }
    }
    oppositeTypeOf(type) {
        if (type === 'ROCK') {
            return 'PAPER';
        } else if (type === 'PAPER') {
            return 'SCISSORS';
        } else {
            return 'ROCK';
        }
    }
}
class Player {
    constructor() {
        this.pacs = [];
        this.score = null;
    }
    bigPelletAssigned(bigPellet) {
        return !!this.pacs.find(pac => pac.bigPellet && tool.samePlace(pac.bigPellet, bigPellet));
    }
}
class Pac {
    constructor() {
        this.id = null;
        this.x = null;
        this.y = null;
        this.prev = null;
        this.type = null;
        this.speedTurnsLeft = null;
        this.abilityCooldown = null;
        this.targetMove = null;
        this.targetSwitch = null;
        this.triggerSpeed = false;
        this.bigPellet = null;
    }
    findNearestBigPelletAndTriggerSpeed(game, me, wantToTriggerSpeed) {
        const nearestBigPelletNotAssignYet = game.findNearestBigPelletNotAssignYet(this, me);
        if (!nearestBigPelletNotAssignYet) { return; }
        this.triggerSpeed = wantToTriggerSpeed;
        this.targetMove = nearestBigPelletNotAssignYet;
    }
    findNearestNormalPelletInViewIfNotSetTargetYet(game) {
        if (this.targetMove) { return; }
        this.findNearestNormalPelletInView(game);
    }
    allNextPlacesOf(place, game, oneMoreLayer) {
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

        if (oneMoreLayer) {
            l = {x: place.x - 2, y: place.y};
            if (l.x < 0) { l = {x: game.w - 1, y: place.y}; }
            r = {x: place.x + 2, y: place.y};
            if (r.x > game.w - 1) { r = {x: 0, y: place.y}; }
            t = {x: place.x, y: place.y - 2};
            if (t.y < 0) { t = null; }
            b = {x: place.x, y: place.y + 2};
            if (b.y > game.h - 1) { b = null; }

            if (!game.isWall(l)) { result.push(l); }
            if (!game.isWall(r)) { result.push(r); }
            if (t && !game.isWall(t)) { result.push(t); }
            if (b && !game.isWall(b)) { result.push(b); }
        }
        
        return result;
    }
    findNearestNormalPelletInView(game) {
                let cur = this;
        let markedPlaces = [cur];
        let queue = [cur];
        while (queue.length > 0) {
            let place = queue.shift();
            let pellet = game.findPellet(place);
            if (pellet) {
                this.targetMove = pellet;
                return pellet;
            }
            this.allNextPlacesOf(place, game, false)
                .forEach(nextPlace => {
                    if (!markedPlaces.find(markedPlace => tool.samePlace(markedPlace, nextPlace))) {
                        markedPlaces.push(nextPlace);
                        queue.push(nextPlace);
                    }
                });
        }
        return null;
    }
    resetTargetIfAlreadyCameTarget() {
        if (this.targetMove && tool.samePlace(this, this.targetMove)) {
            this.targetMove = null;
            this.isGoingToARandomPlace = false;
        }
    }
    updateAfterRereshFrame(pac) {
        this.prev = {x: this.x, y: this.y};
        this.x = pac.x;
        this.y = pac.y;
        this.type = pac.type;
        this.speedTurnsLeft = pac.speedTurnsLeft;
        this.abilityCooldown = pac.abilityCooldown;
        this.upToDate = true;
    }
    goToARandomPlaceButNotVisitYetIfIsBlocking(game, me) {
        if (this.isBlocking()) {
            this.targetMove = game.getRandomPlaceButNotVisitYet();
            this.bigPellet = null;
            this.isGoingToARandomPlace = true;
            this.delayFindPelletAfterGoToRandomPlace = 3;
        }
    }
    isBlocking() {
        return this.prev && tool.samePlace(this, this.prev);
    }
    findNearestNormalPelletInViewWhileGoToRandomPlace(game) {
        this.delayFindPelletAfterGoToRandomPlace--;
        if (!this.isGoingToARandomPlace || !this.delayFindPelletAfterGoToRandomPlace || this.delayFindPelletAfterGoToRandomPlace > 0) {
            return;
        }
        let found = this.findNearestNormalPelletInView(game);
        if (found) {
            this.isGoingToARandomPlace = false;
        }
    }
    switchTypeIfNearCompetitor(game, comp) {
        if (this.isInCooldown()) { return; }
        let nearCompetitorPac = this.findNearCompetitorPac(game, comp);
        if (nearCompetitorPac) {
            if (nearCompetitorPac.isInCooldown()) {
                this.targetSwitch = game.oppositeTypeOf(nearCompetitorPac.type);
            } else {
                this.targetSwitch = game.pacTypes[tool.random(0, game.pacTypes.length - 1)];
            }
        }
    }
    findNearCompetitorPac(game, comp) {
        let nextPlaces = this.allNextPlacesOf(this, game, true);
        if (!nextPlaces || !nextPlaces.length) { return null; }

        for (let i = 0; i < nextPlaces.length; i++) {
            let nextPlace = nextPlaces[i];
            let found = comp.pacs.find(pac => pac.x === nextPlace.x && pac.y === nextPlace.y);
            if (found) { return found; }
        }
        return null;
    }
    isInCooldown() { return this.abilityCooldown > 0; }
}
class Pellet {
    constructor() {
        this.x = null;
        this.y = null;
        this.value = null;
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
game.initFloors(game.rows);

while (true) {
    var inputs = readline().split(' ');
    me.score = parseInt(inputs[0]);
    comp.score = parseInt(inputs[1]);
    game.numOfPacs = parseInt(readline()); // all your pacs and enemy pacs in sight
    me.pacs.forEach(p => p.upToDate = false);
    comp.pacs.forEach(p => p.upToDate = false);
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
        let pacs = mine ? me.pacs : comp.pacs;
        let found = pacs.find(p => p.id === pac.id);
        pac.upToDate = true;
        if (found) {
            found.updateAfterRereshFrame(pac);
        } else {
            pacs.push(pac);
        }
        game.updateNotVisitedFloors(pac);
    }
    me.pacs = me.pacs.filter(p => p.upToDate);
    comp.pacs = comp.pacs.filter(p => p.upToDate);
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
