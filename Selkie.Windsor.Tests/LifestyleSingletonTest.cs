using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NUnit.Framework;
using Selkie.Windsor.Tests.Library;

namespace Selkie.Windsor.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class LifestyleSingletonTest
    {
        [SetUp]
        public void Setup()
        {
            m_Container = new WindsorContainer();
            m_Container.Install(FromAssembly.InThisApplication());

            m_One = m_Container.Resolve <ISingeltonTest>();
            m_Two = m_Container.Resolve <ISingeltonTest>();
        }

        [TearDown]
        public void Teardown()
        {
            m_Container.Release(m_One);
            m_Container.Release(m_Two);
            m_Container.Dispose();
        }

        private WindsorContainer m_Container;
        private ISingeltonTest m_One;
        private ISingeltonTest m_Two;

        [Test]
        public void AddingTest()
        {
            m_One.SomeInteger++;

            Assert.AreEqual(m_One.SomeInteger,
                            m_Two.SomeInteger);
        }

        [Test]
        public void CallingResolveShouldReturnOnlyOneInstanceTest()
        {
            Assert.AreEqual(m_One,
                            m_Two);
        }
    }
}