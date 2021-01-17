const { app, BrowserWindow } = require('electron');
const { createWindow } = require('./index/index.m');

app.whenReady().then(createWindow);

app.on('window-all-closed', () => {
    if (process.platform !== 'darwin') {
        app.quit();
    }
});

app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) {
        createWindow();
    }
});

try {
	require('electron-reloader')(module);
} catch {}
