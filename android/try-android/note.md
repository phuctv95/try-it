## Basic

Basic:
- `MainActivity.kt` is the entry point for your app.
- `activity_main` defines the UI.
- `AndroidManifest.xml` describes the fundamental characteristics of the app.
- `build.gradle` (one for app, one for project): controls how the Gradle plugin builds your app.

UI:
- The user interface (UI) for an Android app is built as a hierarchy of *layouts* and *widgets*.

4 types of app components:
- Activities:
    + An activity is a screen that user interact on.
    + Implemented as a subclass of `Activity`.
- Services:
    + For keeping an app running in background. They do not have UI.
    + Implemented as a subclass of `Service`.
- Broadcast receivers:
    + Is a component that enables the system to deliver events to the app outside of a regular user flow, allowing the app to respond to system-wide broadcast announcements.
    + The system can deliver broadcasts even to apps that aren't currently running.
    + E.g. an app can schedule an alarm to post a notification to tell the user about an upcoming event.
    + Some other examples: a broadcast announcing that the screen has turned off, the battery is low, or a picture was captured.
    + Implemented as a subclass of `BroadcastReceiver`.
- Content providers:
    + A content provider manages a shared set of app data that you can store in the file system, in a SQLite database, on the web, or on any other persistent storage location that your app can access.
    +  For example, the Android system provides a content provider that manages the user's contact information. As such, any app with the proper permissions can query the content provider, such as `ContactsContract.Data`, to read and write information about a particular person.
    
Activate a component:
- Three of the four component types—activities, services, and broadcast receivers—are activated by an asynchronous message called an *intent*.
- Content providers are activated when targeted by a request from a `ContentResolver`.

Manifest file:
- Identifies any user permissions the app requires.
- Declares the minimum API Level required.
- Declares hardware and software features used or required by the app.
- Declares API libraries the app needs to be linked against.
- Declaring components.

