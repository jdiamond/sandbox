using System;
using System.Collections.Generic;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace WindsorFactory
{
    class Program
    {
        static void Main()
        {
            var container = new WindsorContainer();

            container.Kernel.AddFacility<TypedFactoryFacility>();

            container.Register(Component.For<IService>()
                                        .ImplementedBy<DefaultService>());
            container.Register(Component.For<IWorkerFactory>()
                                        .AsFactory());
            container.Register(Component.For<IWorker>()
                                        .ImplementedBy<FooWorker>()
                                        .LifeStyle.Transient);
            container.Register(Component.For<IWorker>()
                                        .ImplementedBy<BarWorker>()
                                        .LifeStyle.Transient);

            var service = container.Resolve<IService>();

            service.ProvideService();

            Console.WriteLine("disposing container");
            container.Dispose();
        }
    }

    public interface IService
    {
        void ProvideService();
    }

    public interface IWorkerFactory
    {
        IEnumerable<IWorker> GetWorkers();
        void ReleaseWorkers(IEnumerable<IWorker> workers);
    }

    public interface IWorker
    {
        void DoWork();
    }

    public class DefaultService : IService
    {
        private readonly IWorkerFactory _workerFactory;

        public DefaultService(IWorkerFactory workerFactory)
        {
            _workerFactory = workerFactory;
        }

        public void ProvideService()
        {
            Console.WriteLine("providing service");

            var workers = _workerFactory.GetWorkers();

            foreach (var worker in workers)
            {
                worker.DoWork();
            }

            _workerFactory.ReleaseWorkers(workers);

            Console.WriteLine("done providing service");
        }
    }

    public class FooWorker : IWorker
    {
        public void DoWork()
        {
            Console.WriteLine("doing foo work");
        }
    }

    public class BarWorker : IWorker, IDisposable
    {
        public void DoWork()
        {
            Console.WriteLine("doing bar work");
        }

        public void Dispose()
        {
            Console.WriteLine("disposing bar worker");
        }
    }
}