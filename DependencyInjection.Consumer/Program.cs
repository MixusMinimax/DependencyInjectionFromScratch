using DependencyInjection;
using DependencyInjection.Consumer;

var services = new ServiceCollection()
    .AddSingleton<IPrintToConsoleService, PrintToConsoleService>()
    .AddTransient<IPrintRandomNumberService, PrintRandomNumberService>()
    .AddSingleton<Random>()
    .AddScoped<IRandom, MockRandom>()
    .AddScoped<IScopeIdPrinter, ScopeIdPrinter>()
    .BuildServiceProvider();

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

public class MockRandom : IRandom
{
    public int Value { get; init; }

    public MockRandom(Random random)
    {
        Value = random.Next();
    }

    public int Next()
    {
        return Value;
    }
}