using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using JetBrains.Annotations;
using Selkie.Windsor.Examples.Library;

namespace Selkie.Windsor.Example
{
    [ExcludeFromCodeCoverage]
    public class SingeltonExample
    {
        public void Example([NotNull] IWindsorContainer container)
        {
            Console.WriteLine("Singelton example...");

            var one = container.Resolve <ISingeltonTest>();
            Console.WriteLine("Resolved 'ISingeltonTest' the first time...");

            var two = container.Resolve <ISingeltonTest>();
            Console.WriteLine("Resolved 'ISingeltonTest' the second time...");

            Console.WriteLine("one == two are the same? {0}",
                              one == two);

            one.SomeInteger++;
            Console.WriteLine("Increase SomeNumber for one. - Current number for one = {0} and two = {1}",
                              one.SomeInteger,
                              two.SomeInteger);

            bool isSameValue = one.SomeInteger == two.SomeInteger;
            Console.WriteLine("one.SomeInteger == two.SomeInteger ? {0}",
                              isSameValue);

            container.Release(one);
            container.Release(two);
        }
    }
}