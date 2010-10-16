using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Simple
{
    class Program
    {
        static void Main()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IFoo>()
                                        .ImplementedBy<DefaultFoo>());
            container.Register(Component.For<IBar>()
                                        .ImplementedBy<DefaultBar>());
            container.Register(Component.For<IBaz>()
                                        .ImplementedBy<DefaultBaz>());

            var foo = container.Resolve<IFoo>();

            foo.DoFoo();

            container.Dispose();
        }
    }

    public interface IFoo
    {
        void DoFoo();
    }

    public interface IBar
    {
        void DoBar();
    }

    public interface IBaz
    {
        void DoBaz();
    }

    public class DefaultFoo : IFoo, IDisposable
    {
        private readonly IBar _bar;
        private readonly IBaz _baz;

        public DefaultFoo(IBar bar, IBaz baz)
        {
            Console.WriteLine("constructing default foo");

            _bar = bar;
            _baz = baz;
        }

        public void DoFoo()
        {
            Console.WriteLine("default foo stuff");

            _bar.DoBar();
            _baz.DoBaz();
        }

        public void Dispose()
        {
            Console.WriteLine("disposing default foo");
        }
    }

    public class DefaultBar : IBar, IDisposable
    {
        private readonly IBaz _baz;

        public DefaultBar(IBaz baz)
        {
            Console.WriteLine("constructing default bar");

            _baz = baz;
        }

        public void DoBar()
        {
            Console.WriteLine("default bar stuff");

            _baz.DoBaz();
        }

        public void Dispose()
        {
            Console.WriteLine("disposing default bar");
        }
    }

    public class DefaultBaz : IBaz, IDisposable
    {
        public DefaultBaz()
        {
            Console.WriteLine("constructing default baz");
        }

        public void DoBaz()
        {
            Console.WriteLine("default baz stuff");
        }

        public void Dispose()
        {
            Console.WriteLine("disposing default baz");
        }
    }
}