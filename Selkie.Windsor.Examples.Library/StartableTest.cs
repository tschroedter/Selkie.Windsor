using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Core;

namespace Selkie.Windsor.Examples.Library
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
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

        public int SomeInteger { get; set; }
    }

    public interface IStartableTest
    {
        int SomeInteger { get; set; }
    }

    //ncrunch: no coverage end
}