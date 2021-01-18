import { Channels } from './../channels';
import { BrowserWindow, ipcMain } from "electron";
import { createDialogWindow } from './dialog/dialog.m';

export function createIndexWindow() {
    const mainWindow = new BrowserWindow({
        height: 600,
        width: 800,
        webPreferences: {
            nodeIntegration: true
        },
    });
    mainWindow.loadFile('./index/index.html');
}

ipcMain.on(Channels.ShowDialogMessage, (e, msg) => {
    createDialogWindow(msg);
});
