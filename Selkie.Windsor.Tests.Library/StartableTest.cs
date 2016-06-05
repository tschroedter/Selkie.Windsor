using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Core;

namespace Selkie.Windsor.Tests.Library
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Startable)]
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
    }

    public interface IStartableTest
    {
    }
}