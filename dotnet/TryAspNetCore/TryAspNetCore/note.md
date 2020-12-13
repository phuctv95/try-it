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

- The `Configure()` method is used to specify how the app responds to HTTP requests.