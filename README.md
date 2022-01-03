# DependencyInjectionFromScratch
Exposes the same API as Micrsosoft.DependencyInjection

This was just done for fun, because I wanted to know whether I can do it. I only looked at the source code of Microsoft's DI so that the exposed API would match (All the different AddTransient/AddSingleton/AddScoped method signatures)
This way, this should be a drop-in replacement.

In no way do I expect this to be better than Microsoft's DI, but maybe I can get it to run faster.
Also, this was written in maybe seven hours, I didn't time myself.

### Current state of the project

- Works with lifetimes (Transient, Scoped, Singleton)
- Works with scopes
- Everything is mockable
- Dependency injection works using reflection

### TODO

- Check for circular dependencies
- More attributes for special constructor parameters (for now: ScopeIdAttribute to inject the id of the current scope)
- Use IL Code generation to construct services instead of reflecion
  - Store parameter types
  - Create Func<> object based on ConstructorInfo
  - `Expression.Call().Compile()`?
- Write Tests
- Benchmark against Microsoft's Dependency Injection
