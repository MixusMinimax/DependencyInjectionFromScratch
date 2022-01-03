namespace DependencyInjection.Consumer;

public class DeepDependency
{
    private readonly A _a;

    public DeepDependency(A a)
    {
        _a = a;
    }

    public int Foo()
    {
        // Console.WriteLine("DeepDependency.Foo()");
        return _a.Foo();
    }

    public class A
    {
        private readonly B _b;

        public A(B b)
        {
            _b = b;
        }

        public int Foo()
        {
            // Console.WriteLine("A.Foo()");
            return _b.Foo();
        }
    }

    public class B
    {
        private readonly C _c;

        public B(C c)
        {
            _c = c;
        }

        public int Foo()
        {
            // Console.WriteLine("B.Foo()");
            return _c.Foo();
        }
    }

    public class C
    {
        private readonly D _d;

        public C(D d)
        {
            _d = d;
        }

        public int Foo()
        {
            // Console.WriteLine("C.Foo()");
            return _d.Foo();
        }
    }

    public class D
    {
        private readonly IRandom _random;

        public D(IRandom random)
        {
            _random = random;
        }

        public int Foo()
        {
            // Console.WriteLine("D.Foo()");
            return _random.Next();
        }
    }
}

public static class DeepDependencyExtensions
{
    public static IServiceCollection AddDeepDependency(this IServiceCollection services)
    {
        return services
            .AddTransient<DeepDependency>()
            .AddTransient<DeepDependency.A>()
            .AddTransient<DeepDependency.B>()
            .AddTransient<DeepDependency.C>()
            .AddTransient<DeepDependency.D>();
    }
}