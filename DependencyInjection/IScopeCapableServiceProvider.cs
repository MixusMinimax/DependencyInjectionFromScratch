namespace DependencyInjection;

public interface IScopeCapableServiceProvider
{
    public IScopedServiceProvider CreateScope();
}