using System.Diagnostics.CodeAnalysis;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NUnit.Framework;
using Selkie.Windsor.Tests.Library;

namespace Selkie.Windsor.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    internal sealed class TypedFactoryTest
    {
        private WindsorContainer m_Container;
        private ITypedFactoryTest m_Factory;
        private ITransientTest m_One;
        private ITransientTest m_Two;

        [SetUp]
        public void Setup()
        {
            m_Container = new WindsorContainer();
            m_Container.Install(FromAssembly.InThisApplication());

            m_Factory = m_Container.Resolve <ITypedFactoryTest>();

            m_One = m_Factory.Create();
            m_Two = m_Factory.Create();
        }

        [TearDown]
        public void Teardown()
        {
            m_Factory.Release(m_One);
            m_Factory.Release(m_Two);
            m_Container.Release(m_Factory);
            m_Container.Dispose();
        }

        [Test]
        public void CallingResolveShouldReturnOnlyOneInstanceTest()
        {
            Assert.AreNotEqual(m_One,
                               m_Two);
        }

        [Test]
        public void AddingTest()
        {
            m_One.SomeInteger++;

            Assert.AreNotEqual(m_One.SomeInteger,
                               m_Two.SomeInteger);
        }
    }

    //ncrunch: no coverage end
}