# Selkie.Windsor

The Selkie.Windsor project creates a NuGet package. The package is an extension to Castle Windsor and provides class attributes to mark and load classes automatically as singleton, transient or start-able. It also contains an ITypedFactory interface which loads interfaces inheriting from it as factories at start-up. Besides that, there are different Castle Windsor installer classes and a BasicConsoleInstaller class. 

The package includes different installers and a loader class. The loader class finds all classes using the custom class attributes and registers the class according to the attribute. The included base installer is used to create a loader class and run the registration.
Please, check the provided examples for more details.

# Examples:

Singelton
```CS
    [ProjectComponent(Lifestyle.Singleton)]
    public class SingeltonTest : ISingeltonTest
    {
        public int SomeInteger { get; set; }
    }
```

Startable
```CS
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
```

TypedFactory
```CS
    public interface ITypedFactoryTest : ITypedFactory
    {
        ITransientTest Create();
        void Release([NotNull] ITransientTest transientTest);
    }
```

# Selkie
Selkie.Windsor is part of the Selkie project which is based on Castle Windsor and EasyNetQ. The main goal of the Selkie project is to calculate and displays the shortest path for a boat travelling along survey lines from point A to B. The algorithm takes into account the minimum required turn circle of a vessel required to navigate from one line to another.

The project started as a little ant colony optimization application. Over time the application grew and was split up into different services which communicate via RabbitMQ. The whole project is used to try out TDD, BDD, DRY and SOLID.

# Selkie projects:

* Selkie ACO
* [Selkie Common](https://github.com/tschroedter/Selkie.Common)
* [Selkie EasyNetQ](https://github.com/tschroedter/Selkie.EasyNetQ)
* [Selkie Geometry] (https://github.com/tschroedter/Selkie.Geometry)
* [Selkie NUnit Extensions](https://github.com/tschroedter/Selkie.NUnit.Extensions)
* Selkie Racetrack
* Selkie Services ACO
* Selkie Services Common
* Selkie Services Lines
* Selkie Services Monitor
* Selkie Services Racetracks
* Selkie Web
* [Selkie Windsor](https://github.com/tschroedter/Selkie.Windsor)
* Selkie WPF
* [Selkie XUnit Extensions](https://github.com/tschroedter/Selkie.XUnit.Extensions)
 

