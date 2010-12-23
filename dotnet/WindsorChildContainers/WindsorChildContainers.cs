using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace WindsorChildContainers
{
    class Program
    {
        static void Main()
        {
            var container = new WindsorContainer();

            container.Register(Component.For<IFoo>().ImplementedBy<DefaultFoo>().LifeStyle.Transient);
            container.Register(Component.For<IBar>().ImplementedBy<DefaultBar>());

            Console.WriteLine("using root container");
            var foo1 = container.Resolve<IFoo>();
            foo1.DoFoo();

            var childContainer = new WindsorContainer();
            container.AddChildContainer(childContainer);

            //childContainer.Register(Component.For<IFoo>().ImplementedBy<AlternateFoo>());
            childContainer.Register(Component.For<IBar>().ImplementedBy<AlternateBar>());

            Console.WriteLine("using child container");
            var foo2 = childContainer.Resolve<IFoo>();
            foo2.DoFoo();
        }
    }

    public interface IFoo
    {
        void DoFoo();
    }

    public class DefaultFoo : IFoo
    {
        private readonly IBar _bar;

        public DefaultFoo(IBar bar)
        {
            _bar = bar;
        }

        public void DoFoo()
        {
            Console.WriteLine("default foo stuff");

            _bar.DoBar();
        }
    }

    public class AlternateFoo : IFoo
    {
        private readonly IBar _bar;

        public AlternateFoo(IBar bar)
        {
            _bar = bar;
        }

        public void DoFoo()
        {
            Console.WriteLine("alternate foo stuff");

            _bar.DoBar();
        }
    }

    public interface IBar
    {
        void DoBar();
    }

    public class DefaultBar : IBar
    {
        public void DoBar()
        {
            Console.WriteLine("default bar stuff");
        }
    }

    public class AlternateBar : IBar
    {
        public void DoBar()
        {
            Console.WriteLine("alternate bar stuff");
        }
    }
}
