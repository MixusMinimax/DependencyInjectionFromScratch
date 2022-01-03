using DependencyInjection.Attributes;

namespace DependencyInjection.Consumer;

public interface IScopeIdPrinter
{
    public void PrintScopeId();
}

public class ScopeIdPrinter : IScopeIdPrinter
{
    private readonly IPrintToConsoleService _logger;
    private readonly Guid _scopeId;

    public ScopeIdPrinter(IPrintToConsoleService logger, [ScopeId] Guid scopeId)
    {
        _logger = logger;
        _scopeId = scopeId;
    }

    public void PrintScopeId()
    {
        _logger.WriteLine($"Scope Id = {{{_scopeId}}}");
    }
}