## Basic

Basic:
- Electron is a framework that enables you to create desktop applications with JavaScript, HTML, and CSS.
- These applications can then be packaged to run directly on macOS, Windows, or Linux, or distributed via the Mac App Store or the Microsoft Store.
- Electron uses Chromium and Node.js.

main.js:
- This is the entry point Electron application, it will run the Main process.
- An Electron application can have only one Main process.

.html file:
- This web page represents the Renderer process.
- You can create multiple browser windows, where each window uses its own independent Renderer.

Electron consists of three main pillars:
- *Chromium* for displaying web content.
- *Node.js* for working with the local filesystem and the operating system.
- *Custom APIs* for working with often-needed OS native functions.

Electron has two types of processes: Main and Renderer.
- The Main process *creates* web pages by creating `BrowserWindow` instances. Each `BrowserWindow` instance runs the web page in its Renderer process. When a `BrowserWindow` instance is destroyed, the corresponding Renderer process gets terminated as well.
- The Main process *manages* all web pages and their corresponding Renderer processes.
- The Renderer process *manages* only the corresponding web page. A crash in one Renderer process does not affect other Renderer processes.
- The Renderer process *communicates* with the Main process via IPC to perform GUI operations in a web page. Calling native GUI-related APIs from the Renderer process directly is restricted due to security concerns and potential resource leakage.
- In short:
    + Main process creates/manages a set of BrowserWindow, each of BrowserWindow is corresponding with a Renderer process.
    + Main process and Renderer process communicates with eachother by IPC.

Electron API:
- Electron APIs are assigned based on the *process type*, meaning that some modules can be used from either the Main or Renderer process, and some from both.
- To access the *Node.js API* from the Renderer process, you need to set the nodeIntegration preference to true.
- Electron exposes full access to *Node.js API* and its modules both in the Main and the Renderer processes.

Install a Node.js module just like normal:
- E.g. `npm install aws-sdk`
- Use it: `const S3 = require('aws-sdk/clients/s3')`.
