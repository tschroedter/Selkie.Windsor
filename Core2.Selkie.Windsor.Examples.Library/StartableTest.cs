using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Core;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Examples.Library
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Startable)]
    [UsedImplicitly]
    public class StartableTest
        : IStartableTest,
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

    public interface IStartableTest
    {
        [UsedImplicitly]
        int SomeInteger { get; set; }
    }
}