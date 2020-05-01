import * as fs from 'fs';
import * as path from 'path';
import { execSync } from 'child_process';

export { scheduleChangeBackground };

/**
 * Schedule changing GNOME background
 * @param {string} folder folder contains wallpapers
 * @param {number} period period in minute for next change
 */
async function scheduleChangeBackground(folder, period, toBeShuffled) {
    let files = fs.readdirSync(folder);
    if (toBeShuffled) { shuffle(files); }

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        let filePath = path.join(folder, file);
        let command = `gsettings set org.gnome.desktop.background picture-uri file://${filePath}`;
        console.log(`[${new Date().toLocaleString()}] Setting background using file: ${filePath}...`);
        execSync(command, {stdio: 'inherit'});

        await sleep(period * 60 * 1000);
        if (i === files.length - 1) { i = -1; }
    }
}

function shuffle(arr) {
    for (let i = arr.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [arr[i], arr[j]] = [arr[j], arr[i]];
    }
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}
