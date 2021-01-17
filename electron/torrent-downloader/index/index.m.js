const { BrowserWindow, ipcMain } = require('electron');
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

    win.loadFile('index/index.html');
    win.setMenuBarVisibility(false);
}

ipcMain.on(channels.OpenDevTools, (event, args) => {
    win.webContents.toggleDevTools();
});
