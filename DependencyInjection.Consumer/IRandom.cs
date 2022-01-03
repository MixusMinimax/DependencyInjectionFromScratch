namespace DependencyInjection.Consumer;

public interface IRandom
{
    public int Next();
}

public class DefaultRandom : IRandom
{
    private readonly Random _random;

    public DefaultRandom(Random random)
    {
        _random = random;
    }

    public int Next() => _random.Next();
}