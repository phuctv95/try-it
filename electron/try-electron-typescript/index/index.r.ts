import { Channels } from './../channels';
import { ipcRenderer } from "electron";

const clickMeBtn = document.querySelector('#clickMeBtn') ?? new Element();

clickMeBtn.addEventListener('click', () => ipcRenderer.send(Channels.ShowDialogMessage, 'Hello world!'));
