using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using DependencyInjection.Attributes;

namespace DependencyInjection;

public sealed class ServiceProvider : IScopeCapableServiceProvider
{
    private static readonly Guid RootScope = Guid.Empty;
    private readonly Dictionary<Guid, Dictionary<Type, ServiceInstance>> _serviceCache;
    private readonly Dictionary<Type, ServiceDescriptor> _serviceDefinitions;

    private readonly Dictionary<Type, (ParameterInfo[] Parameters, Func<object[], object> Constructor)>
        _implementationFactories = new();

    public ServiceProvider(IServiceCollection serviceCollection)
    {
        _serviceDefinitions = serviceCollection.ToDictionary(e => e.ServiceType);
        _serviceCache = new Dictionary<Guid, Dictionary<Type, ServiceInstance>>
        {
            [RootScope] = new()
        };
        foreach (var service in _serviceDefinitions.Values)
        {
            var implementationType = service.ImplementationType;
            if (implementationType is null) continue;
            var constructor = implementationType.GetConstructors().First(info => info.IsPublic);
            var dependencies = constructor.GetParameters();
            var parameters = dependencies
                .Select(e => Expression.Parameter(e.ParameterType, e.Name)).ToArray();
            var inner = Expression.Lambda(Expression.New(constructor, parameters), parameters);
            var args = Expression.Parameter(typeof(object[]), "args");
            var body = Expression.Invoke(inner,
                parameters.Select((p, i) =>
                    Expression.Convert(Expression.ArrayIndex(args, Expression.Constant(i)), p.Type)).ToArray());
            var factory = Expression.Lambda<Func<object[], object>>(body, args).Compile();
            _implementationFactories[implementationType] = (
                dependencies,
                factory
            );
        }
    }

    public IScopedServiceProvider CreateScope()
    {
        return new ScopedServiceProvider(this, Guid.NewGuid());
    }

    public object? GetService(Type serviceType)
    {
        return GetService(this, serviceType, RootScope);
    }

    private object? GetService(IServiceProvider services, Type serviceType, Guid scopeId)
    {
        if (!_serviceDefinitions.ContainsKey(serviceType)) return null;
        var descriptor = _serviceDefinitions[serviceType];
        if (descriptor.Lifetime is ServiceLifetime.Singleton)
            scopeId = RootScope;

        return (TryGetServiceInstance(serviceType, scopeId, out var instance)
            ? instance
            : CreateServiceInstance(services, serviceType, scopeId)).Service;
    }

    private bool TryGetServiceInstance(Type serviceType, Guid scopeId,
        [MaybeNullWhen(false)] out ServiceInstance result)
    {
        if (!_serviceDefinitions.ContainsKey(serviceType))
            throw new ArgumentException("service type does not exist!");
        var descriptor = _serviceDefinitions[serviceType];
        if (descriptor.Lifetime is ServiceLifetime.Singleton)
            scopeId = RootScope;
        result = null;
        var ret = _serviceCache.TryGetValue(scopeId, out var services)
                  && services.TryGetValue(serviceType, out result);
        return ret;
    }

    private ServiceInstance CreateServiceInstance(IServiceProvider services, Type serviceType, Guid scopeId)
    {
        if (!_serviceDefinitions.ContainsKey(serviceType))
            throw new ArgumentException("service type does not exist!");
        var descriptor = _serviceDefinitions[serviceType];
        if (descriptor.Lifetime is ServiceLifetime.Singleton)
            scopeId = RootScope;

        _serviceCache[scopeId] =
            _serviceCache.TryGetValue(scopeId, out var serviceInstances)
                ? serviceInstances
                : serviceInstances = new Dictionary<Type, ServiceInstance>();

        if (serviceInstances.ContainsKey(serviceType)) throw new Exception("ServiceInstance already exists!");

        var builder = ServiceInstance.Builder
            .SetServiceDescriptor(descriptor)
            .SetScope(scopeId);

        if (descriptor.ImplementationType is not null)
            builder.SetService(InstantiateService(services, descriptor.ImplementationType, scopeId));
        else if (descriptor.ImplementationFactory is not null)
            builder.SetService(descriptor.ImplementationFactory(services));
        else
            builder.SetService(descriptor.ImplementationInstance);

        var serviceInstance = builder.Build();

        if (descriptor.Lifetime is ServiceLifetime.Scoped or ServiceLifetime.Singleton)
            serviceInstances[serviceType] = serviceInstance;

        return serviceInstance;
    }

    private object InstantiateService(IServiceProvider services, Type implementationType, Guid scopeId)
    {
        if (implementationType is null) throw new ArgumentNullException(nameof(implementationType));
        if (!_implementationFactories.ContainsKey(implementationType))
            throw new Exception($"No factory for {implementationType.Name}");
        var (dependencies, factory) = _implementationFactories[implementationType];
        var parameters = new object[dependencies.Length];
        foreach (var parameterInfo in dependencies)
        {
            var paramType = parameterInfo.ParameterType;
            ref var param = ref parameters[parameterInfo.Position];
            if (paramType.IsInstanceOfType(services) && paramType.IsAssignableTo(typeof(IServiceProvider)))
            {
                param = services;
            }
            else if (paramType.IsAssignableFrom(typeof(Guid)) &&
                     Attribute.IsDefined(parameterInfo, typeof(ScopeIdAttribute)))
            {
                param = scopeId;
            }
            else
            {
                param = services.GetRequiredService(paramType);
            }
        }

        return factory(parameters);
    }

    private void ClearCache(Guid scopeId)
    {
        _serviceCache.Remove(scopeId);
    }

    private sealed class ScopedServiceProvider : IScopedServiceProvider
    {
        private readonly ServiceProvider _services;

        public ScopedServiceProvider(ServiceProvider services, Guid scopeId)
        {
            _services = services;
            ScopeId = scopeId;
        }

        public Guid ScopeId { get; }

        public object? GetService(Type serviceType)
        {
            return _services.GetService(this, serviceType, ScopeId);
        }

        public void Dispose()
        {
            _services.ClearCache(ScopeId);
        }

        public IScopedServiceProvider CreateScope()
        {
            return _services.CreateScope();
        }
    }
}