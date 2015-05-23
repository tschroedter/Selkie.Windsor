using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NUnit.Framework;

namespace Selkie.Windsor.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    internal sealed class LifestyleStartableTest
    {
        [SetUp]
        public void Setup()
        {
            m_Container = new WindsorContainer();
            m_Container.Install(FromAssembly.InThisApplication());

            // todo don't know how to test Lifestyle.Startable component
        }

        [TearDown]
        public void Teardown()
        {
            m_Container.Dispose();
        }

        private WindsorContainer m_Container;
    }

    //ncrunch: no coverage end
}