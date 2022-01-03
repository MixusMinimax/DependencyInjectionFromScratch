namespace DependencyInjection.Consumer;

public interface IPrintToConsoleService
{
    public void WriteLine(string message);
}

public class PrintToConsoleService : IPrintToConsoleService
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}