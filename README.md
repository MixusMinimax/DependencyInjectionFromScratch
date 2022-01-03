# DependencyInjectionFromScratch
Exposes the same API as Micrsosoft.DependencyInjection

This was just done for fun, because I wanted to know whether I can do it. I only looked at the source code of Microsoft's DI so that the exposed API would match (All the different AddTransient/AddSingleton/AddScoped method signatures)
This way, this should be a drop-in replacement.

In no way do I expect this to be better than Microsoft's DI, but maybe I can get it to run faster.
Also, this was written in maybe seven hours, I didn't time myself.

### Current state of the project

- Works with lifetimes (Transient, Scoped, Singleton)
- Works with scopes
- Everything is mockable (Essentially all classes implement an interface with the same name, so that you can replace anything during testing.)
- Differend method signatures of `ServiceProvider.GetService` and `ServiceProvider.GetRequiredService` are defined as extension methods, so that all `IServiceProvider` implementations only need to implement `GetService(Type serviceType)`
- Dependency injection works using reflection. The first public Constructor is used.
  - The service provider itself can be injected. The static type can be anything that the current serviceProvider can be assigned to.
  - The Guid of the current scope can be injected
- Use IL Code generation to construct services instead of reflecion
  - Store parameter types
  - Create Func<> object based on ConstructorInfo
  - `Expression...Compile()`

### TODO

- Check for circular dependencies
- More attributes for special constructor parameters (for now: ScopeIdAttribute to inject the id of the current scope)
- Write Tests
- Benchmark against Microsoft's Dependency Injection

### Reflection vs Linq Expression Compilation

```js
|              Method |       Mean |    Error |  StdDev |
|-------------------- |-----------:|---------:|--------:|
|     UsingReflection | 1,206.8 ns | 10.24 ns | 9.58 ns |
|       UsingDelegate |   842.1 ns |  4.25 ns | 3.97 ns |
| DeepUsingReflection | 1,350.5 ns |  6.12 ns | 5.73 ns |
|   DeepUsingDelegate |   960.6 ns |  5.67 ns | 5.30 ns |
```
