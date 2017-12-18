using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Core2.Selkie.Windsor.Interfaces;

namespace Core2.Selkie.Windsor.Example
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static void Main()
        {
            IWindsorContainer container = new WindsorContainer();
            var installer = new Installer();
            container.Install(installer);

            var singelton = new SingeltonExample();
            singelton.Example(container);

            var transient = new TransientExample();
            transient.Example(container);

            var startable = new StartableExample();
            startable.Example();

            var factory = new TypedFactoryExample();
            factory.Example(container);

            var logger = container.Resolve <ISelkieLogger>();
            logger.Info("Hello World!");
            container.Release(logger);

            container.Dispose();

            Console.ReadLine();
        }
    }
}