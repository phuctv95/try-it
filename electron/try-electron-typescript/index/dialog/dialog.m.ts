import { Channels } from './../../channels';
import { BrowserWindow, ipcMain } from "electron";

export function createDialogWindow(msg: string) {
    const win = new BrowserWindow({
        height: 200,
        width: 350,
        webPreferences: {
            nodeIntegration: true
        },
    });
    win.loadFile('./index/dialog/dialog.html');
    win.webContents.on('dom-ready', e => win.webContents.send(Channels.UpdateDialogContent, msg));
}
