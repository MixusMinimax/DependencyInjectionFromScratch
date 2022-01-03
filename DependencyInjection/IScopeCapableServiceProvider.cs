namespace DependencyInjection;

public interface IScopeCapableServiceProvider : IServiceProvider
{
    public IScopedServiceProvider CreateScope();
}