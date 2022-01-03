namespace DependencyInjection;

public static class ServiceCollectionServiceExtensions
{
    // Singleton

    public static IServiceCollection AddSingleton(
        this IServiceCollection collection,
        Type serviceType,
        Type implementationType)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationType is null) throw new ArgumentNullException(nameof(implementationType));
        return collection.Add(serviceType, implementationType, ServiceLifetime.Singleton);
    }

    public static IServiceCollection AddSingleton<TService, TImplementation>(
        this IServiceCollection collection)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        return collection.AddSingleton(typeof(TService), typeof(TImplementation));
    }

    public static IServiceCollection AddSingleton(
        this IServiceCollection collection,
        Type serviceType)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        return collection.AddSingleton(serviceType, serviceType);
    }

    public static IServiceCollection AddSingleton<TService>(
        this IServiceCollection collection)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        return collection.AddSingleton(typeof(TService));
    }

    public static IServiceCollection AddSingleton(
        this IServiceCollection collection,
        Type serviceType,
        Func<IServiceProvider, object> implementationFactory)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.Add(serviceType, implementationFactory, ServiceLifetime.Singleton);
    }

    public static IServiceCollection AddSingleton<TService>(
        this IServiceCollection collection,
        Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.AddSingleton(typeof(TService), implementationFactory);
    }

    public static IServiceCollection AddSingleton<TService, TImplementation>(
        this IServiceCollection collection,
        Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.AddSingleton<TService>(implementationFactory);
    }

    public static IServiceCollection AddSingleton(
        this IServiceCollection collection,
        Type serviceType,
        object implementationInstance)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationInstance is null) throw new ArgumentNullException(nameof(implementationInstance));
        collection.Add(new ServiceDescriptor(serviceType, implementationInstance, ServiceLifetime.Singleton));
        return collection;
    }

    public static IServiceCollection AddSingleton<TService>(
        this IServiceCollection collection,
        TService implementationInstance)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationInstance is null) throw new ArgumentNullException(nameof(implementationInstance));
        return collection.AddSingleton(typeof(TService), implementationInstance);
    }

    public static IServiceCollection AddSingleton<TService, TImplementation>(
        this IServiceCollection collection,
        TImplementation implementationInstance)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationInstance is null) throw new ArgumentNullException(nameof(implementationInstance));
        return collection.AddSingleton<TService>(implementationInstance);
    }

    // Scoped

    public static IServiceCollection AddScoped(
        this IServiceCollection collection,
        Type serviceType,
        Type implementationType)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationType is null) throw new ArgumentNullException(nameof(implementationType));
        return collection.Add(serviceType, implementationType, ServiceLifetime.Scoped);
    }

    public static IServiceCollection AddScoped<TService, TImplementation>(
        this IServiceCollection collection)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        return collection.AddScoped(typeof(TService), typeof(TImplementation));
    }

    public static IServiceCollection AddScoped(
        this IServiceCollection collection,
        Type serviceType)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        return collection.AddScoped(serviceType, serviceType);
    }

    public static IServiceCollection AddScoped<TService>(
        this IServiceCollection collection)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        return collection.AddScoped(typeof(TService));
    }

    public static IServiceCollection AddScoped(
        this IServiceCollection collection,
        Type serviceType,
        Func<IServiceProvider, object> implementationFactory)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.Add(serviceType, implementationFactory, ServiceLifetime.Scoped);
    }

    public static IServiceCollection AddScoped<TService>(
        this IServiceCollection collection,
        Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.AddScoped(typeof(TService), implementationFactory);
    }

    public static IServiceCollection AddScoped<TService, TImplementation>(
        this IServiceCollection collection,
        Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.AddScoped<TService>(implementationFactory);
    }

    // Transient

    public static IServiceCollection AddTransient(
        this IServiceCollection collection,
        Type serviceType,
        Type implementationType)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationType is null) throw new ArgumentNullException(nameof(implementationType));
        return collection.Add(serviceType, implementationType, ServiceLifetime.Transient);
    }

    public static IServiceCollection AddTransient<TService, TImplementation>(
        this IServiceCollection collection)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        return collection.AddTransient(typeof(TService), typeof(TImplementation));
    }

    public static IServiceCollection AddTransient(
        this IServiceCollection collection,
        Type serviceType)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        return collection.AddTransient(serviceType, serviceType);
    }

    public static IServiceCollection AddTransient<TService>(
        this IServiceCollection collection)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        return collection.AddTransient(typeof(TService));
    }

    public static IServiceCollection AddTransient(
        this IServiceCollection collection,
        Type serviceType,
        Func<IServiceProvider, object> implementationFactory)
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.Add(serviceType, implementationFactory, ServiceLifetime.Transient);
    }

    public static IServiceCollection AddTransient<TService>(
        this IServiceCollection collection,
        Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.AddTransient(typeof(TService), implementationFactory);
    }

    public static IServiceCollection AddTransient<TService, TImplementation>(
        this IServiceCollection collection,
        Func<IServiceProvider, TImplementation> implementationFactory)
        where TService : class
        where TImplementation : class, TService
    {
        if (collection is null) throw new ArgumentNullException(nameof(collection));
        if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));
        return collection.AddTransient<TService>(implementationFactory);
    }

    private static IServiceCollection Add(
        this IServiceCollection collection,
        Type serviceType,
        Type implementationType,
        ServiceLifetime lifetime)
    {
        var descriptor = new ServiceDescriptor(serviceType, implementationType, lifetime);
        collection.Add(descriptor);
        return collection;
    }

    private static IServiceCollection Add(
        this IServiceCollection collection,
        Type serviceType,
        Func<IServiceProvider, object> implementationFactory,
        ServiceLifetime lifetime)
    {
        var descriptor = new ServiceDescriptor(serviceType, implementationFactory, lifetime);
        collection.Add(descriptor);
        return collection;
    }
}