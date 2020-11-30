## using

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