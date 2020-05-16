/**
 * Grab the pellets as fast as you can!
 **/

const me = {}
const enn = {}
const grid = {}
const pacTypes = ['ROCK', 'PAPER', 'SCISSORS'];

var inputs = readline().split(' ');
const width = parseInt(inputs[0]); // size of the grid
const height = parseInt(inputs[1]); // top left corner is (x=0, y=0)
grid.w = width;
grid.h = height;
grid.rows = [];
for (let i = 0; i < height; i++) {
    const row = readline(); // one line of the grid: space " " is floor, pound "#" is wall
    grid.rows.push(row.split(''));
}
grid.isWall = place => {
    return grid.rows[place.y][place.x] === '#';
}
enn.find = pac => enn.pacs.find(ennPac => ennPac.x === pac.x && ennPac.y === pac.y);
enn.enemyIsNearby = pac => {
    let lef = {x: pac.x - 2, y: pac.y};
    if (lef.x < 0) { lef = {x: grid.w - 2, y: pac.y}; }
    let lef2 = {x: pac.x - 1, y: pac.y};
    if (lef2.x < 0) { lef2 = {x: grid.w - 1, y: pac.y}; }
    
    let rig = {x: pac.x + 2, y: pac.y};
    if (rig.x > grid.w - 1) { rig = {x: 0, y: pac.y}; }
    let rig2 = {x: pac.x + 1, y: pac.y};
    if (rig2.x > grid.w - 1) { rig2 = {x: 0, y: pac.y}; }
    
    let top = {x: pac.x, y: pac.y - 2};
    if (top.y < 0) { top = null; }
    let top2 = {x: pac.x, y: pac.y - 1};
    if (top2.y < 0) { top2 = null; }
    
    let bot = {x: pac.x, y: pac.y + 2};
    if (bot.y > grid.h - 1) { bot = null; }
    let bot2 = {x: pac.x, y: pac.y + 1};
    if (bot2.y > grid.h - 1) { bot2 = null; }

    let nearbyPlaces = [lef, lef2, rig, rig2, top, top2, bot, bot2];
    for (let i = 0; i < nearbyPlaces.length; i++) {
        let place = nearbyPlaces[i];
        if (!place) { continue; }
        let found = enn.find(place);
        if (found) { return found; }
    }
    return null;
};

while (true) {
    var inputs = readline().split(' ');
    const myScore = parseInt(inputs[0]);
    const opponentScore = parseInt(inputs[1]);
    me.score = myScore;
    enn.score = opponentScore;
    me.pacs = [];
    enn.pacs = [];
    const visiblePacCount = parseInt(readline()); // all your pacs and enemy pacs in sight
    for (let i = 0; i < visiblePacCount; i++) {
        var inputs = readline().split(' ');
        const pacId = parseInt(inputs[0]); // pac number (unique within a team)
        const mine = inputs[1] !== '0'; // true if this pac is yours
        const x = parseInt(inputs[2]); // position in the grid
        const y = parseInt(inputs[3]); // position in the grid
        const typeId = inputs[4]; // unused in wood leagues
        const speedTurnsLeft = parseInt(inputs[5]); // unused in wood leagues
        const abilityCooldown = parseInt(inputs[6]); // unused in wood leagues
        let pac = {};
        pac.id = pacId;
        pac.x = x;
        pac.y = y;
        pac.type = typeId;
        pac.speedTurnsLeft = speedTurnsLeft;
        pac.abilityCooldown = abilityCooldown;
        if (mine) {
            me.pacs.push(pac);
        } else {
            enn.pacs.push(pac);
        }
    }
    const visiblePelletCount = parseInt(readline()); // all pellets in sight
    grid.pellets = [];
    grid.diamonds = [];
    for (let i = 0; i < visiblePelletCount; i++) {
        var inputs = readline().split(' ');
        const x = parseInt(inputs[0]);
        const y = parseInt(inputs[1]);
        const value = parseInt(inputs[2]); // amount of points this pellet is worth
        grid.pellets.push({x: x, y: y, value: value});
        if (value === 10) { grid.diamonds.push({x: x, y: y, value: value}); }
    }
    grid.isPellet = place => !!grid.pellets.find(pellet => pellet.x === place.x && pellet.y === place.y);

    // MAIN
    let moves = [];
    let switches = [];
    me.pacs.forEach(pac => {
        let switched = switchTypeIfEnemyNearby(pac, switches);
        if (!switched) {
            pac.target = findNearestDiamond(pac);
            pac.target = findNearestPlaceIfCurTargetIsNull(pac);
            pac.target = findEndOppositePlaceIfCurTargetNull(pac);
            //pac.target = findAnotherWayIfFaceTeammate(pac);
            moves.push(pac);
        }
    });

    let cmds = [];
    moves.forEach(pac => cmds.push(`MOVE ${pac.id} ${pac.target.x} ${pac.target.y}`));
    switches.forEach(pac => cmds.push(`SWITCH ${pac.id} ${pacTypes[random(0, 2)]}`));
    console.log(cmds.join(' | '));
}

function findNearestDiamond(pac) {
    let min = 999999, iMin = null;
    for (let i = 0; i < grid.diamonds.length; i++) {
        let diamond = grid.diamonds[i];
        if (diamond.pac) { continue; }
        let d = distance(pac, diamond);
        if (d < min) {
            min = d;
            iMin = i;
        }
    }
    if (iMin === null) { return null; }

    grid.diamonds[iMin].pac = pac;
    return grid.diamonds[iMin];
}

function switchTypeIfEnemyNearby(pac, switchs) {
    let nearbyEne = enn.enemyIsNearby(pac);
    if (nearbyEne) {
        let old = pac.targetType;
        pac.targetType = pacTypes[random(0, 2)];
        if (old !== pac.targetType) {
            switchs.push(pac);
            return true;
        }
        return false;
    }
}

function findEndOppositePlaceIfCurTargetNull(pac) {
    if (pac.target) { return pac.target; }
    if (!pac.targetOpp || samePlace(pac.targetOpp, pac)) {
        let place = {};
        place.x = pac.x < grid.w / 2 ? grid.w - 2 : 1;
        place.y = pac.y < grid.h / 2 ? grid.h - 2 : 1;
        return place;
    }
    return pac.targetOpp;
}

function findNearestPlaceIfCurTargetIsNull(pac, grid) {
    if (pac.target) { return pac.target; }

    function allNextPlaces(cur, grid) {
        let lef = {x: cur.x - 1, y: cur.y};
        if (lef.x === -1) { lef = {x: grid.w - 1, y: cur.y}; }
        let rig = {x: cur.x + 1, y: cur.y};
        if (rig.x === grid.w) { rig = {x: 0, y: cur.y}; }
        let top = {x: cur.x, y: cur.y - 1};
        if (top.y === -1) { top = null; }
        let bot = {x: cur.x, y: cur.y + 1};
        if (bot.y === grid.h) { bot = null; }

        let result = [];
        if (!grid.isWall(lef)) { result.push(lef); }
        if (!grid.isWall(rig)) { result.push(rig); }
        if (top && !grid.isWall(top)) { result.push(top); }
        if (bot && !grid.isWall(bot)) { result.push(bot); }
        return result;
    }

    let cur = {x: pac.x, y: pac.y};
    let markedPlaces = [cur];
    let queue = [cur];
    while (queue.length > 0) {
        let place = queue.shift();
        if (grid.isPellet(place)) {
            return place;
        }

        allNextPlaces(place, grid)
            .forEach(nextPlace => {
                if (!markedPlaces.find(markedPlace => markedPlace.x === nextPlace.x && markedPlace.y === nextPlace.y)) {
                    markedPlaces.push(nextPlace);
                    queue.push(nextPlace);
                }
            });
    }
    return null;
}

function samePlace(p1, p2) { return p1.x === p2.x && p1.y === p2.y; }

function random(min, max) {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min + 1)) + min; //The maximum is inclusive and the minimum is inclusive 
}

function distance(p1, p2) {
    let a = p1.x - p2.x;
    let b = p1.y - p2.y;
    return Math.sqrt(a * a + b*b);
}
