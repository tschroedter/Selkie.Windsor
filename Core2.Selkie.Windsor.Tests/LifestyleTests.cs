using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Core2.Selkie.Windsor.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class LifestyleTests
    {
        [Test]
        public void LifestyleSingletonParseTest()
        {
            Lifestyle actual;

            Enum.TryParse("Singleton",
                          out actual);

            Assert.AreEqual(Lifestyle.Singleton,
                            actual);
        }

        [Test]
        public void LifestyleSingletonToStringTest()
        {
            Assert.AreEqual("Singleton",
                            Lifestyle.Singleton.ToString());
        }

        [Test]
        public void LifestyleTransientParseTest()
        {
            Lifestyle actual;

            Enum.TryParse("Transient",
                          out actual);

            Assert.AreEqual(Lifestyle.Transient,
                            actual);
        }

        [Test]
        public void LifestyleTransientToStringTest()
        {
            Assert.AreEqual("Transient",
                            Lifestyle.Transient.ToString());
        }
    }
}