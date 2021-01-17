const { ipcRenderer } = require('electron');
const channels = require('./channels');

const downloadBtn = document.querySelector('#downloadBtn');
const openDevToolsBtn = document.querySelector('#openDevToolsBtn');
const magnetUrlInput = document.querySelector('#magnetUrlInput');
const logTextarea = document.querySelector('#logTextarea');
const downloadProgressBar = document.querySelector('#downloadProgressBar');

function writeLog(msg) {
    logTextarea.value += `${msg}\n`;
    logTextarea.scrollTop = logTextarea.scrollHeight;
}

downloadBtn.addEventListener('click', () => {
    ipcRenderer.send(channels.DownloadTorrent, magnetUrlInput.value);
});

openDevToolsBtn.addEventListener('click', () => {
    ipcRenderer.send(channels.ToggleDevTools);
});

magnetUrlInput.addEventListener('input', () => {
    downloadBtn.disabled = magnetUrlInput.value === '';
});

ipcRenderer.on(channels.OnTorrentDownloading, (event, ...args) => {
    writeLog(`just downloaded ${args[0]}`
        + ` | total downloaded ${args[1]}`
        + ` | download speed ${args[2]}`
        + ` | progress ${args[3]}`
        );
    downloadProgressBar.style.width = `${args[3] * 100}%`;
});

ipcRenderer.on(channels.OnTorrentFinished, (event, args) => {
    writeLog('Download finished.');
});
