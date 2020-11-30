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
