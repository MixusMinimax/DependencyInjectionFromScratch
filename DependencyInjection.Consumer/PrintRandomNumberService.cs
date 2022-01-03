namespace DependencyInjection.Consumer;

public interface IPrintRandomNumberService
{
    public void PrintRandomNumber();
}

public class PrintRandomNumberService : IPrintRandomNumberService
{
    private readonly IRandom _random;
    private readonly IPrintToConsoleService _logger;

    public PrintRandomNumberService(IRandom random, IPrintToConsoleService logger)
    {
        _random = random;
        _logger = logger;
    }

    public void PrintRandomNumber()
    {
        _logger.WriteLine($"Random Number: {_random.Next()}");
    }
}