## Startup class

### Dependency injection (services)

- `ConfigureServices()` is called before `Configure()`.
- Service lifetimes:
	+ Transient:
		- are created each time they're requested from the service container (created each time request a service).
		- works best for lightweight, stateless services.
		- are disposed at the end of the request.
	+ Scoped:
		- are created once per client request (connection) (singleton in the request).
		- are disposed at the end of the request.
	+ Singleton:
		- are created the first time they're requested.
		- are disposed when the `ServiceProvider` is disposed on application shutdown.

### Request pipeline

- Middleware is software that's assembled into an app pipeline to handle requests and responses. Each component:
	+ Chooses whether to pass the request to the next component in the pipeline.
	+ Can perform work before and after the next component in the pipeline
- Is in configured in `Configure()`.
- A middleware component can invoke the next component in the pipeline or short-circuiting the pipeline (prevents further middleware from processing).
- Workflow:  
	![](imgs/request-delegate-pipeline.png)
- Call `next.Invoke()` to invoke the next middleware.
- Typical order:  
	![](imgs/middleware-pipeline.svg)
- An individual request delegate can be specified in-line as an anonymous method (called in-line middleware), or it can be defined in a reusable class.

## Host

- A host is an object that encapsulates an app's resources, such as: Dependency injection (DI), Logging, Configuration, `IHostedService` implementations.

## Server

- The server implementation listens for HTTP requests.
- *Kestrel* is the default web server specified by the ASP.NET Core.

## Configuration

- Can be read in `Startup` class by using `Configuration` property like: `Configuration["Logging:LogLevel:Default"]`.

## Logging

- .NET Core supports a logging API that works with a variety of built-in and third-party logging providers.
- Call `CreateDefaultBuilder`, which adds the following logging providers: Console, Debug, EventSource, EventLog (in Windows).
