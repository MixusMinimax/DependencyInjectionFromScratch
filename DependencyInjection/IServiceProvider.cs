namespace DependencyInjection;

public interface IServiceProvider
{
    public object? GetService(Type serviceType);
}