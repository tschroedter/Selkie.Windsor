using System;
using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Core2.Selkie.Windsor.Examples.Library;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Example
{
    [ExcludeFromCodeCoverage]
    public class TypedFactoryExample
    {
        public void Example([NotNull] IWindsorContainer container)
        {
            Console.WriteLine("TypedFactoryTest example...");

            var factory = container.Resolve <ITypedFactoryTest>();

            ITransientTest one = factory.Create();
            Console.WriteLine("Created 'ITransientTest' the first time...");

            ITransientTest two = factory.Create();
            Console.WriteLine("Created 'ITransientTest' the second time...");

            Console.WriteLine("one == two are the same? {0}",
                              one == two);

            one.SomeInteger++;
            Console.WriteLine("Increase SomeNumber for one. - Current number for one = {0} and two = {1}",
                              one.SomeInteger,
                              two.SomeInteger);

            bool isSameValue = one.SomeInteger == two.SomeInteger;
            Console.WriteLine("one.SomeInteger == two.SomeInteger ? {0}",
                              isSameValue);

            factory.Release(one);
            factory.Release(two);
            container.Release(factory);
        }
    }
}