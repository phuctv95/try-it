# RxJS

https://rxjs.dev/

## Observable

Observables are lazy Push collections of multiple values.

|      | Single   | Multiple   |
|------|----------|------------|
| Pull | Function | Iterator   |
| Push | Promise  | Observable |


An Observable is a lazily evaluated computation that can synchronously or asynchronously return zero to (potentially) infinite values from the time it's invoked onwards.

A `subscribe` call is simply a way to start an "Observable execution" and deliver values or events to an Observer of that execution.

In an Observable Execution, zero to infinite Next notifications may be delivered. If either an Error or Complete notification is delivered, then nothing else can be delivered afterwards.

When you subscribe, you get back a `Subscription`, which represents the ongoing execution. Just call `unsubscribe()` to cancel the execution.

## Observer

An Observer is a consumer of values delivered by an Observable.

Observers are simply a set of callbacks, one for each type of notification delivered by the Observable: `next`, `error`, and `complete`.

## Operators

A Pipeable Operator is a function that takes an Observable as its input and returns another Observable. It is a pure operation: the previous Observable stays unmodified.

A Creation Operator is a function that can be used to create an Observable with some common predefined behavior or by joining other Observables.

Higher-order Observables:
- Is when an observable emits other observables.
- We can flatten them by using some operators like `concatAll()`, `mergeAll()`.

## Subject

An RxJS Subject is a special type of Observable that allows values to be multicasted to many Observers. While plain Observables are unicast (each subscribed Observer owns an independent execution of the Observable), Subjects are multicast.

A Subject is like an Observable, but can multicast to many Observers. Subjects are like EventEmitters: they maintain a registry of many listeners.

That means:
- When calling `subscribe()`, Observable will execute all the `next(v)` synchronously or asynchronously for current Observer, then goes on for the next Observer.
- When calling `subscribe()`, Subject simply registers the given Observer in a list of Observers. Whenever the Subject calling `next(v)`, it will trigger all the callbacks provided from Observers.
- If all `next(v)` of Observerables are asynchronously, they'll be similar to Subject, that's my opinion.

A Subject is also Observer, because it has `next(v)`, `error(e)` and `complete()`, so it can be passed an argument to the `subscribe()` of any Observable.

BehaviorSubject:
- One of the variants of Subjects is the `BehaviorSubject`, which has a notion of "the current value". It stores the latest value emitted to its consumers, and whenever a new Observer subscribes, it will immediately receive the "current value" from the `BehaviorSubject`.

ReplaySubject:
- A ReplaySubject records multiple values from the Observable execution and replays them to new subscribers.

AsyncSubject:
- The AsyncSubject is a variant where only the last value of the Observable execution is sent to its observers, and only when the execution completes.
