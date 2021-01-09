# Dependency Injection

## Idea

The direction of dependency within the application should be in the direction of abstraction, not implementation details.

Problem of tight coupling (between a class and its dependency): error-prone, not easy to change dependency, not easy to test, not modular.

## Why?

- Application or class can be independent of how its objects are created.
- The way objects are created be specified in separate configuration files?
- Generally, it's more testable, modular, and maintainable.

## Roles

Dependency injection involves four roles:
- the *service* object(s) to be used
- the *client* object that is depending on the service(s) it uses
- the *interfaces* that define how the client may use the services
- the *injector*, which is responsible for constructing the services and injecting them into the client

# Testing

- OpenCover: `dotnet $(UserProfile)\.nuget\packages\opencover\x.y.z\tools\netcoreapp3.0\OpenCover.Console.exe -target:"path\to\dotnet.exe" -targetargs:"test TestProjectName" -register:user`.
- ReportGenerator:
    + https://danielpalme.github.io/ReportGenerator/usage.html
    + `dotnet $(UserProfile)\.nuget\packages\reportgenerator\x.y.z\tools\netcoreapp3.0\ReportGenerator.dll "-reports:OpenCover.xml" "-targetdir:coveragereport" -reporttypes:Html "-assemblyfilters:-NUnit3.TestAdapter;-Tests" "-classfilters:-System.Runtime.*;-Microsoft.CodeAnalysis.*"`
