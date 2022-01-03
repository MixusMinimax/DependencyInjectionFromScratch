using System.Linq.Expressions;
using BenchmarkDotNet.Running;
using DependencyInjection;
using DependencyInjection.Consumer;

BenchmarkRunner.Run<Benchmark>();
return;

var services =
    new ServiceProvider(
        new ServiceCollection()
            .AddSingleton<IPrintToConsoleService, PrintToConsoleService>()
            .AddTransient<IPrintRandomNumberService, PrintRandomNumberService>()
            .AddSingleton<Random>()
            .AddScoped<IRandom, MockRandom>()
            .AddScoped<IScopeIdPrinter, ScopeIdPrinter>()
            .AddDeepDependency()
    );

var printService = services.GetRequiredService<IPrintToConsoleService>();
printService.WriteLine("Hello, World!");

services.GetRequiredService<IPrintRandomNumberService>().PrintRandomNumber();
services.GetRequiredService<IPrintRandomNumberService>().PrintRandomNumber();
services.GetRequiredService<IScopeIdPrinter>().PrintScopeId();

using (var scope = services.CreateScope())
{
    scope.GetRequiredService<IPrintRandomNumberService>().PrintRandomNumber();
    scope.GetRequiredService<IPrintRandomNumberService>().PrintRandomNumber();
    scope.GetRequiredService<IScopeIdPrinter>().PrintScopeId();
}

using (var scope = services.CreateScope())
{
    scope.GetRequiredService<IPrintRandomNumberService>().PrintRandomNumber();
    scope.GetRequiredService<IPrintRandomNumberService>().PrintRandomNumber();
    scope.GetRequiredService<IScopeIdPrinter>().PrintScopeId();
}

Console.WriteLine($"DeepDependency returned: {services.GetRequiredService<DeepDependency>().Foo()}");

public class MockRandom : IRandom
{
    public int Value { get; init; }

    public MockRandom(Random random)
    {
        Value = random.Next();
    }
    
    public MockRandom()
    {
    }

    public int Next()
    {
        return Value;
    }
}

class Test
{
    public Test(string a, int b, bool c)
    {
        Console.WriteLine((a, b, c));
    }
}