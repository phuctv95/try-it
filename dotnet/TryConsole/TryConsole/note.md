﻿## using

Compare with old approach:
- Old approach: use `try` and call `Dispose()` in `finally`.
- New approach: use `using`, it automatically call `Dispose()` even an exception occurs.

When to use:
- when use an instance of a class implemented the `IDisposable` which is the unmanaged resource.
- if not, it leads to memory leaks.

## Reflection

The classes in the `System.Reflection` namespace, together with `System.Type`, enable you to obtain information about loaded assemblies and the types defined within them, such as classes, interfaces, and value types.

## Pattern matching

Patterns test that a value has a certain **shape**, and can **extract** information from the value when it has the matching shape.


## Asynchronous streams

Starting with C# 8.0, you can create and consume streams asynchronously. A method that returns an asynchronous stream has three properties:

	1. It's declared with the `async` modifier.
	2. It returns an `IAsyncEnumerable<T>`.
	3. The method contains `yield return` statements to return successive elements in the asynchronous stream.

Consuming an asynchronous stream requires you to add the `await` keyword before the `foreach` keyword when you enumerate the elements of the stream.

A method can both consume and produce an asynchronous stream.

## Asynchronous disposable

Starting with C# 8.0, the language supports asynchronous disposable types that implement the `System.IAsyncDisposable` interface.

You use the `await using` statement to work with an asynchronously disposable object.

## Indices and ranges

Indices and ranges provide a succinct syntax for accessing *single* elements or *ranges* in a sequence.
- The index from end operator `^`, which specifies that an index is relative to the end of the sequence (start from 1).
- The range operator `..`, which specifies the start and end of a range as its operands (the start of the range is inclusive, but the end of the range is exclusive).

## Null-coalescing assignment

You can use the `??=` operator to assign the value of its right-hand operand to its left-hand operand only if the left-hand operand evaluates to `null`.

## .NET Core

.NET is a free, open-source development platform for building many kinds of apps.

Some characteristics: cross platform, open source, multiple languages (C#, F# and Visual Basic), can be used with various continuous integration tools and environments, .NET Interactive

## .NET Core SDK

- .NET Core SDK is a set of libraries and tools that allow developers to create .NET Core applications and libraries.
- It contains:
	+ .NET Core CLI
	+ .NET Core libraries and runtime
	+ The `dotnet` driver

## .NET CLI

- .NET CLI is a cross-platform toolchain for developing, building, running, and publishing .NET applications.

## .NET Core run-time configuration

.NET Core supports the use of configuration files and environment variables to configure the behavior of .NET Core applications at run time.

runtimeconfig.json:
- Applies to current application.
- When a project is built, an `[appname].runtimeconfig.json` file is generated in the output directory.
- If a `runtimeconfig.template.json` file exists in the same folder as the project file, any configuration options it contains are merged into the `[appname].runtimeconfig.json` file.

MSBuild properties:
- Applies to current application.
- Are in the `.csproj`.
- MSBuild properties take precedence over options set in the `runtimeconfig.template.json` file.
- They also overwrite any options you set in the `[appname].runtimeconfig.json`.

Environment variables:
- Apply to all .NET Core apps.
- Set environment variables from the Windows Control Panel, or by command line, or by calling `Environment.SetEnvironmentVariable(String, String)`.


## .NET Standard

.NET Standard is a specification that represents a set of APIs that all .NET platforms have to implement. This unifies the .NET platforms and prevents future fragmentation.

.NET Core is a concrete .NET platform and implements the .NET Standard.

## ASP.NET Core

Middleware is software that's assembled into an app pipeline to handle requests and responses. Each component:
- Chooses whether to pass the request to the next component in the pipeline.
- Can perform work before and after the next component in the pipeline.

The `Startup` class configures services and the app's request pipeline.

## Dependency Injection

- ASP.NET Core includes a built-in dependency injection (DI) framework that makes configured services available throughout an app.
- Code to configure (or register) services is added to the `Startup.ConfigureServices()` method.
