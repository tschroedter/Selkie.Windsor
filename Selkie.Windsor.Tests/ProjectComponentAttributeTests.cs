using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Selkie.Windsor.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class ProjectComponentAttributeTests
    {
        [SetUp]
        public void Setup()
        {
            m_Transient = new ProjectComponentAttribute(Lifestyle.Transient);
            m_Singelton = new ProjectComponentAttribute();
        }

        private ProjectComponentAttribute m_Singelton;
        private ProjectComponentAttribute m_Transient;

        [Test]
        public void SingeltonLifestyleTest()
        {
            Assert.AreEqual(Lifestyle.Singleton,
                            m_Singelton.Lifestyle);
        }

        [Test]
        public void SingeltonNameTest()
        {
            Assert.AreEqual(m_Singelton.GetType().FullName,
                            m_Singelton.Name);
        }

        [Test]
        public void TransientLifestyleTest()
        {
            Assert.AreEqual(Lifestyle.Transient,
                            m_Transient.Lifestyle);
        }

        [Test]
        public void TransientNameTest()
        {
            Assert.AreEqual(m_Transient.GetType().FullName,
                            m_Transient.Name);
        }
    }
}