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
        width: 800,
        height: 650,
        webPreferences: {
            nodeIntegration: true
        }
    });

    win.loadFile(path.join(__dirname, 'index.html'));
    win.setMenuBarVisibility(false);
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
            torrent.on('download',
                bytes => {
                    win.webContents.send(channels.OnTorrentDownloading, torrent.progress);
                });
            torrent.on('done',
                () => {
                    win.webContents.send(channels.OnTorrentFinished);
                    torrent.destroy();
                });
        });
});

ipcMain.on(channels.OpenSelectFolderDialog, (_, __) => {
    let selectedFolder = dialog.showOpenDialogSync({properties: ['openDirectory']})[0];
    win.webContents.send(channels.OnSelectedFolder, selectedFolder);
});
