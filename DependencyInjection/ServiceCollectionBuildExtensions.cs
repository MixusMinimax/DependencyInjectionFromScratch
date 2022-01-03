namespace DependencyInjection;

public static class ServiceCollectionBuildExtensions
{
    public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
    {
        return new ServiceProvider(services);
    }
    
    [Obsolete("This uses Reflection and is slower")]
    public static IServiceProvider BuildLegacyServiceProvider(this IServiceCollection services)
    {
        return new LegacyServiceProvider(services);
    }
}