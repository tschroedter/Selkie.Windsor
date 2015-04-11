using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Selkie.Windsor.Example
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    internal class Program
    {
        private static void Main()
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());

            SingeltonExample singelton = new SingeltonExample();
            singelton.Example(container);

            TransientExample transient = new TransientExample();
            transient.Example(container);

            StartableExample startable = new StartableExample();
            startable.Example();

            TypedFactoryExample factory = new TypedFactoryExample();
            factory.Example(container);

            container.Dispose();

            Console.ReadLine();
        }
    }

    //ncrunch: no coverage end
}