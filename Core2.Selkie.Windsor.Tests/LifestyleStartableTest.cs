using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NUnit.Framework;

namespace Core2.Selkie.Windsor.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class LifestyleStartableTest
    {
        [SetUp]
        public void Setup()
        {
            IWindsorInstaller installer = new Library.Installer();

            m_Container = new WindsorContainer();
            m_Container.Install(installer);

            // todo don't know how to test Lifestyle.Startable component
        }

        [TearDown]
        public void Teardown()
        {
            m_Container.Dispose();
        }

        private WindsorContainer m_Container;
    }
}