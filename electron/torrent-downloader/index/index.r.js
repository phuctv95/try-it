const { ipcRenderer } = require('electron');
const channels = require('../channels');

const downloadBtn = document.querySelector('#downloadBtn');
const openDevToolsBtn = document.querySelector('#openDevToolsBtn');
const magnetUrlInput = document.querySelector('#magnetUrlInput');
const logTextarea = document.querySelector('#logTextarea');
const downloadProgressBar = document.querySelector('#downloadProgressBar');
const browseBtn = document.querySelector('#browseBtn');
const savingLocationInput = document.querySelector('#savingLocationInput');

function writeLog(msg) {
    logTextarea.value += `${new Date().toString()}: ${msg}\n`;
    logTextarea.scrollTop = logTextarea.scrollHeight;
}

downloadBtn.addEventListener('click', () => {
    ipcRenderer.send(channels.DownloadTorrent, magnetUrlInput.value, savingLocationInput.value);
});

openDevToolsBtn.addEventListener('click', () => {
    ipcRenderer.send(channels.ToggleDevTools);
});

magnetUrlInput.addEventListener('input', () => {
    downloadBtn.disabled = magnetUrlInput.value === '';
});

browseBtn.addEventListener('click', () => ipcRenderer.send(channels.OpenSelectFolderDialog, magnetUrlInput.value));

ipcRenderer.on(channels.OnTorrentDownloading, (event, progress) => {
    downloadProgressBar.style.width = `${progress * 100}%`;
});

ipcRenderer.on(channels.OnTorrentFinished, (event, args) => {
    writeLog('Download finished.');
});

ipcRenderer.on(channels.OnSelectedFolder, (_, selectedFolder) => savingLocationInput.value = selectedFolder);
