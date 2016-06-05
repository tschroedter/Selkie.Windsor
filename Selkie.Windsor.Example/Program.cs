using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Selkie.Windsor.Example
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());

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