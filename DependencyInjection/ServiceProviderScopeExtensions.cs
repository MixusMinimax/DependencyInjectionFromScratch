namespace DependencyInjection;

public static class ServiceProviderScopeExtensions
{
    public static IScopedServiceProvider CreateScope(this IServiceProvider services)
    {
        if (services is IScopeCapableServiceProvider scopeCapableServiceProvider)
            return scopeCapableServiceProvider.CreateScope();
        throw new Exception("Scope is not supported by this ServiceProvider!");
    }
}