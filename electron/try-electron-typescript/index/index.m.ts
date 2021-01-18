import { Channels } from './../channels';
import { BrowserWindow, ipcMain } from "electron";
import { createDialogWindow } from './dialog/dialog.m';
import * as path from 'path';

export function createIndexWindow() {
    const mainWindow = new BrowserWindow({
        height: 600,
        width: 800,
        webPreferences: {
            nodeIntegration: true
        },
    });
    mainWindow.loadFile(path.join(__dirname, 'index.html'));
}

ipcMain.on(Channels.ShowDialogMessage, (e, msg) => {
    createDialogWindow(msg);
});
