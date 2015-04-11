using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using JetBrains.Annotations;
using Selkie.Windsor.Examples.Library;

namespace Selkie.Windsor.Example
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class TransientExample
    {
        public void Example([NotNull] IWindsorContainer container)
        {
            Console.WriteLine("Transient example...");

            ITransientTest one = container.Resolve <ITransientTest>();
            Console.WriteLine("Resolved 'ITransientTest' the first time...");

            ITransientTest two = container.Resolve <ITransientTest>();
            Console.WriteLine("Resolved 'ITransientTest' the second time...");

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

    //ncrunch: no coverage end
}