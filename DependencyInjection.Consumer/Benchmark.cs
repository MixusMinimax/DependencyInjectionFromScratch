using BenchmarkDotNet.Attributes;

namespace DependencyInjection.Consumer;

public class Benchmark
{
    private IServiceProvider _serviceProvider;

    [GlobalSetup(Targets = new[] { nameof(UsingReflection), nameof(DeepUsingReflection) })]
    public void SetupUsingReflection()
    {
        _serviceProvider = new LegacyServiceProvider(
            new ServiceCollection()
                .AddSingleton<IPrintToConsoleService, PrintToConsoleService>()
                .AddTransient<IPrintRandomNumberService, PrintRandomNumberService>()
                .AddScoped<IRandom>(_ => new MockRandom { Value = 69 })
                .AddTransient<IScopeIdPrinter, ScopeIdPrinter>()
                .AddDeepDependency()
        );
    }

    [GlobalSetup(Targets = new[] { nameof(UsingDelegate), nameof(DeepUsingDelegate) })]
    public void SetupUsingDelegate()
    {
        _serviceProvider = new ServiceProvider(
            new ServiceCollection()
                .AddSingleton<IPrintToConsoleService, PrintToConsoleService>()
                .AddTransient<IPrintRandomNumberService, PrintRandomNumberService>()
                .AddScoped<IRandom>(_ => new MockRandom { Value = 69 })
                .AddTransient<IScopeIdPrinter, ScopeIdPrinter>()
                .AddDeepDependency()
        );
    }

    [Benchmark]
    public void UsingReflection()
    {
        _serviceProvider.GetRequiredService<IPrintRandomNumberService>();
        _serviceProvider.GetRequiredService<IScopeIdPrinter>();
    }

    [Benchmark]
    public void UsingDelegate()
    {
        _serviceProvider.GetRequiredService<IPrintRandomNumberService>();
        _serviceProvider.GetRequiredService<IScopeIdPrinter>();
    }

    [Benchmark]
    public void DeepUsingReflection()
    {
        _serviceProvider.GetRequiredService<DeepDependency>().Foo();
    }

    [Benchmark]
    public void DeepUsingDelegate()
    {
        _serviceProvider.GetRequiredService<DeepDependency>().Foo();
    }
}