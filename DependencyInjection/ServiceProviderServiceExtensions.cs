namespace DependencyInjection;

public static class ServiceProviderServiceExtensions
{
    public static TService GetRequiredService<TService>(this IServiceProvider services)
        where TService : class
    {
        return (TService)services.GetRequiredService(typeof(TService));
    }

    public static object GetRequiredService(this IServiceProvider services, Type serviceType)
    {
        return services.GetService(serviceType) ?? throw new Exception($"Service {{{serviceType.Name}}} not found!");
    }

    public static TService? GetService<TService>(this IServiceProvider services)
        where TService : class
    {
        return services.GetService(typeof(TService)) as TService;
    }
}