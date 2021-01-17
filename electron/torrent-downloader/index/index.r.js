const { ipcRenderer } = require('electron');
const channels = require('./channels');

const downloadBtn = document.querySelector('#downloadBtn');
const openDevToolsBtn = document.querySelector('#openDevToolsBtn');
const magnetUrlInput = document.querySelector('#magnetUrlInput');

downloadBtn.addEventListener('click', () => {
    console.log('Downloading...');
});

openDevToolsBtn.addEventListener('click', () => {
    ipcRenderer.send(channels.OpenDevTools);
});

magnetUrlInput.addEventListener('input', () => {
    downloadBtn.disabled = magnetUrlInput.value === '';
});
