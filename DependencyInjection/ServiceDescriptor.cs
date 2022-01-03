namespace DependencyInjection;

public class ServiceDescriptor
{
    public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        : this(serviceType, lifetime)
    {
        ImplementationType = implementationType;
    }

    public ServiceDescriptor(Type serviceType, object implementationInstance, ServiceLifetime lifetime)
        : this(serviceType, lifetime)
    {
        ImplementationInstance = implementationInstance;
    }

    public ServiceDescriptor(Type serviceType, Func<IServiceProvider, object> implementationFactory,
        ServiceLifetime lifetime)
        : this(serviceType, lifetime)
    {
        ImplementationFactory = implementationFactory;
    }

    private ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        Lifetime = lifetime;
    }

    public ServiceLifetime Lifetime { get; }
    public Type ServiceType { get; }
    public Type? ImplementationType { get; }
    public object? ImplementationInstance { get; }
    public Func<IServiceProvider, object>? ImplementationFactory { get; }

    public override string ToString()
    {
        var ret = $"[{Lifetime}] {ServiceType} => ";
        if (ImplementationType is not null)
            ret += $"{nameof(ImplementationType)}: {ImplementationType.Name}";
        else if (ImplementationInstance is not null)
            ret += $"{nameof(ImplementationInstance)}: {ImplementationInstance}";
        else if (ImplementationFactory is not null)
            ret += $"{nameof(ImplementationFactory)}: {ImplementationFactory.Method}";

        return ret;
    }

    public Type GetImplementationType()
    {
        if (ImplementationType is not null) return ImplementationType;

        if (ImplementationInstance is not null) return ImplementationInstance.GetType();

        if (ImplementationFactory is not null) return ImplementationFactory.GetType().GetGenericArguments()[^1];

        throw new Exception("No ImplementationType defined!");
    }
}