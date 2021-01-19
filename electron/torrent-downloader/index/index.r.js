const { ipcRenderer } = require('electron');
const moment = require('moment');
const channels = require('../channels');
const { formatBytes } = require('../helper');

const downloadBtn = document.querySelector('#downloadBtn');
const openDevToolsBtn = document.querySelector('#openDevToolsBtn');
const magnetUrlInput = document.querySelector('#magnetUrlInput');
const logTextarea = document.querySelector('#logTextarea');
const downloadProgressBar = document.querySelector('#downloadProgressBar');
const browseBtn = document.querySelector('#browseBtn');
const savingLocationInput = document.querySelector('#savingLocationInput');

function writeLog(msg) {
    logTextarea.value += `${new Date().toLocaleString()}: ${msg}\n`;
    logTextarea.scrollTop = logTextarea.scrollHeight;
}

function isEnoughInputToDownload() {
    return magnetUrlInput.value !== '' && savingLocationInput.value !== '';
}

function setDownloadProgressBarValue(value) {
    downloadProgressBar.textContent = value;
    downloadProgressBar.style.width = value;
}

downloadBtn.addEventListener('click', () => {
    writeLog('Start downloading...');
    downloadBtn.disabled = true;
    downloadBtn.textContent = 'Downloading...';
    ipcRenderer.send(channels.DownloadTorrent, magnetUrlInput.value, savingLocationInput.value);
});

openDevToolsBtn.addEventListener('click', () => {
    ipcRenderer.send(channels.ToggleDevTools);
});

magnetUrlInput.addEventListener('input', () => downloadBtn.disabled = !isEnoughInputToDownload());

savingLocationInput.addEventListener('input', () => downloadBtn.disabled = !isEnoughInputToDownload());

browseBtn.addEventListener('click', () => ipcRenderer.send(channels.OpenSelectFolderDialog, magnetUrlInput.value));

ipcRenderer.on(channels.OnTorrentDownloading, (event, report) => {
    let remaining = moment.duration(report.timeRemaining / 1000, 'seconds').humanize() + ' remaining';
    writeLog(`${report.numPeers} peer(s)`
        + ` | ${formatBytes(report.downloaded)}/${formatBytes(report.length)}`
        + ` | ${remaining}`
        + ` | ⬇ ${formatBytes(report.downloadSpeed)}/s ⬆ ${formatBytes(report.uploadSpeed)}/s`);
    let progress = `${parseFloat(report.progress * 100).toFixed(2)}%`;
    setDownloadProgressBarValue(progress);
});

ipcRenderer.on(channels.OnTorrentFinished, (event, args) => {
    downloadBtn.disabled = false;
    downloadBtn.textContent = 'Download';
    setDownloadProgressBarValue('100%');
    writeLog('Download finished.');
});

ipcRenderer.on(channels.OnSelectedFolder, (_, selectedFolder) => {
    savingLocationInput.value = selectedFolder;
    downloadBtn.disabled = !isEnoughInputToDownload();
});
