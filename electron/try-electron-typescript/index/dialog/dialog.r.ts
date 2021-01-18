import { Channels } from './../../channels';
import { ipcRenderer } from "electron";

const msgControl = document.querySelector('#msgControl') ?? new Element();

console.log('A message from dialog renderer.');

ipcRenderer.on(Channels.UpdateDialogContent, (e, msg) => {
    msgControl.textContent = msg;
});