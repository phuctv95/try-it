const { BrowserWindow, ipcMain } = require('electron');
const WebTorrent = require('webtorrent');
const homedir = require('os').homedir();
const path = require('path');
const channels = require('./channels');

module.exports = {
    createWindow: createWindow
};

let win;

function createWindow() {
    win = new BrowserWindow({
        width: 800,
        height: 600,
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

ipcMain.on(channels.DownloadTorrent, (event, magnetUrl) => {
    let client = new WebTorrent();
    client.add(
        magnetUrl,
        {
            path: path.join(homedir, 'Desktop', 'New folder')
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
