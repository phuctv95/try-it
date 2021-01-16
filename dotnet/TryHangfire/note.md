## Basic

Idea:
- Hangfire allows you to kick off method calls outside of the request processing pipeline in a very easy, but reliable way.
- These method invocations are performed in a *background thread* and called *background jobs*.

3 main components:
- Client:
    + Is responsible for creating background jobs and saving them into Storage.
    + Background job is a unit of work that should be performed outside of the current execution context.
    + It can create 3 kinds of jobs, they are *fire-and-forget*, *delayed* and *recurring*.
- Job storage:
    + Hangfire keeps background jobs and other information that relates to the processing inside a *persistent storage*.
- Server:
    + Hangfire Server processes background jobs by querying the Storage.
    + It is implemented as a set of dedicated (not thread pool’s) background threads that fetch jobs from a storage and process them.
    + Server is also responsible to keep the storage clean and remove old data automatically.

IoC container: https://docs.hangfire.io/en/latest/background-methods/using-ioc-containers.html
