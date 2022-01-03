namespace DependencyInjection;

public class ServiceInstance
{
    public ServiceDescriptor ServiceDescriptor { private set; get; } = default!;
    public object? Service { private set; get; }
    public Guid Scope { private set; get; } = Guid.Empty;

    public static ServiceInstanceBuilder Builder => new();

    public bool IsValidFor(Guid scope)
    {
        return ServiceDescriptor.Lifetime switch
        {
            ServiceLifetime.Transient => false,
            ServiceLifetime.Singleton => true,
            ServiceLifetime.Scoped => scope == Scope,
            _ => false
        };
    }

    public struct ServiceInstanceBuilder
    {
        private readonly ServiceInstance _instance = new();
        private bool _built;

        public ServiceInstanceBuilder SetServiceDescriptor(ServiceDescriptor serviceDescriptor)
        {
            if (_built) throw new Exception("ServiceInstance is already built!");
            _instance.ServiceDescriptor = serviceDescriptor;
            return this;
        }

        public ServiceInstanceBuilder SetService(object? service)
        {
            if (_built) throw new Exception("ServiceInstance is already built!");
            _instance.Service = service;
            return this;
        }

        public ServiceInstanceBuilder SetScope(Guid scope)
        {
            if (_built) throw new Exception("ServiceInstance is already built!");
            _instance.Service = scope;
            return this;
        }

        public ServiceInstance Build()
        {
            if (_built) throw new Exception("ServiceInstance is already built!");
            _built = true;
            return _instance;
        }
    }
}