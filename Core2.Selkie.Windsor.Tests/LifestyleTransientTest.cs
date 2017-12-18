using System.Diagnostics.CodeAnalysis;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core2.Selkie.Windsor.Tests.Library;
using NUnit.Framework;

namespace Core2.Selkie.Windsor.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class LifestyleTransientTest
    {
        [SetUp]
        public void Setup()
        {
            IWindsorInstaller installer = new Installer();

            m_Container = new WindsorContainer();
            m_Container.Install(installer);

            m_One = m_Container.Resolve <ITransientTest>();
            m_Two = m_Container.Resolve <ITransientTest>();
        }

        [TearDown]
        public void Teardown()
        {
            m_Container.Release(m_One);
            m_Container.Release(m_Two);
            m_Container.Dispose();
        }

        private WindsorContainer m_Container;
        private ITransientTest m_One;
        private ITransientTest m_Two;

        [Test]
        public void AddingTest()
        {
            m_One.SomeInteger++;

            Assert.AreNotEqual(m_One.SomeInteger,
                               m_Two.SomeInteger);
        }

        [Test]
        public void CallingResolveShouldReturnOnlyOneInstanceTest()
        {
            Assert.AreNotEqual(m_One,
                               m_Two);
        }
    }
}