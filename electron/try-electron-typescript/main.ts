import { app, BrowserWindow } from 'electron';
import { createIndexWindow } from './index/index.m';
const electronReload = require('electron-reload');

electronReload(__dirname);

app.whenReady().then(() => {
    createIndexWindow();

    app.on('activate', function () {
        if (BrowserWindow.getAllWindows().length === 0) {
            createIndexWindow();
        }
    });
});

app.on('window-all-closed', function () {
    if (process.platform !== 'darwin') {
        app.quit();
    }
})
