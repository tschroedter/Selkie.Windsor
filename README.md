# Selkie.Windsor

The Selkie.Windsor project creates a NuGet package. The package is an extension to Castle Windsor and provides class attributes to mark and load classes automatically as singleton, transient or start-able. It also contains an ITypedFactory interface which loads interfaces inheriting from it as factories at start-up. Besides that, there are different Castle Windsor installer classes and a BasicConsoleInstaller class. 

The package includes different installers and a loader class. The loader class finds all classes using the custom class attributes and registers the class according to the attribute. The included base installer is used to create a loader class and run the registration.
Please, check the provided examples for more details.

# Examples:

Singelton

    [ProjectComponent(Lifestyle.Singleton)]
    public class SingeltonTest : ISingeltonTest
    {
        public int SomeInteger { get; set; }
    }

Startable

    [ProjectComponent(Lifestyle.Startable)]
    public class StartableTest : IStartableTest,
                                 IStartable
    {
        public void Start()
        {
            Console.WriteLine("\t\t\t--==> StartableTest is started!");
        }

        public void Stop()
        {
            Console.WriteLine("\t\t\t--==> StartableTest is stopped!");
        }

        public int SomeInteger { get; set; }
    }

TypedFactory

    public interface ITypedFactoryTest : ITypedFactory
    {
        ITransientTest Create();
        void Release([NotNull] ITransientTest transientTest);
    }

# Selkie
Selkie.Windsor is part of the Selkie project which is based on Castle Windsor and EasyNetQ. The main goal of the Selkie project is to calculate and displays the shortest path from point A to B traveling along survey lines.

The project started as a little ant colony optimization application. Over time the application grew and was split up into different services which communicate via RabbitMQ. The whole project is used to try out TDD, BDD, DRY and SOLID.

# Selkie projects:

* Selkie ACO
* Selkie Common
* Selkie EasyNetQ
* Selkie Geometry
* Selkie NUnit Extensions
* Selkie Racetrack
* Selkie Services ACO
* Selkie Service Common
* Selkie Services Lines
* Selkie Services Monitor
* Selkie Services Racetracks
* Selkie WPF
* Selkie Web
* Selkie Windsor
* Selkie XUnit Extensions
 

