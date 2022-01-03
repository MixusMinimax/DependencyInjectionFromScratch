namespace DependencyInjection;

public interface IScopedServiceProvider : IServiceProvider, IDisposable, IScopeCapableServiceProvider
{
    public Guid ScopeId { get; }
}