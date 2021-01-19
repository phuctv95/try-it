const { BrowserWindow, dialog, ipcMain } = require('electron');
const WebTorrent = require('webtorrent');
const homedir = require('os').homedir();
const path = require('path');
const channels = require('../channels');

module.exports = {
    createWindow: createWindow
};

let win;

function createWindow() {
    win = new BrowserWindow({
        width: 900,
        height: 700,
        webPreferences: {
            nodeIntegration: true
        }
    });

    win.loadFile(path.join(__dirname, 'index.html'));
    win.setMenuBarVisibility(false);
}

function onDownloadingReport(torrent) {
    let { progress, numPeers, downloaded, length, timeRemaining, downloadSpeed, uploadSpeed} = torrent;
    let report = { progress, numPeers, downloaded, length, timeRemaining, downloadSpeed, uploadSpeed};
    win.webContents.send(channels.OnTorrentDownloading, report);
}

ipcMain.on(channels.ToggleDevTools, (event, arg) => {
    win.webContents.toggleDevTools();
});

ipcMain.on(channels.DownloadTorrent, (event, magnetUrl, selectedFolder) => {
    let client = new WebTorrent();
    client.add(
        magnetUrl,
        {
            path: selectedFolder
        },
        torrent => {
            let interval = setInterval(() => onDownloadingReport(torrent), 1000);
            torrent.on('done',
                () => {
                    win.webContents.send(channels.OnTorrentFinished);
                    torrent.destroy();
                    clearInterval(interval);
                });
        });
});

ipcMain.on(channels.OpenSelectFolderDialog, (_, __) => {
    let selected = dialog.showOpenDialogSync(win, {properties: ['openDirectory']});
    if (!selected) { return; }

    win.webContents.send(channels.OnSelectedFolder, selected[0]);
});
