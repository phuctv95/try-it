import { Channels } from './../../channels';
import { BrowserWindow } from "electron";
import * as path from 'path';

export function createDialogWindow(msg: string) {
    const win = new BrowserWindow({
        height: 200,
        width: 350,
        webPreferences: {
            nodeIntegration: true
        },
    });
    win.loadFile(path.join(__dirname, 'dialog.html'));
    win.webContents.on('dom-ready', e => win.webContents.send(Channels.UpdateDialogContent, msg));
}
