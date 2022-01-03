namespace DependencyInjection;

public static class ServiceCollectionBuildExtensions
{
    public static IServiceProvider BuildServiceProvider(this IServiceCollection services)
    {
        return new ServiceProvider(services);
    }
}