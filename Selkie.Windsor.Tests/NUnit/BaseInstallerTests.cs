using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NSubstitute;
using NUnit.Framework;

namespace Selkie.Windsor.Tests.NUnit
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    internal sealed class BaseInstallerTests
    {
        private IWindsorContainer m_Container;
        private Assembly m_ExecutingAssembly;
        private TestBaseInstaller m_Installer;
        private IProjectComponentLoader m_Loader;
        private IConfigurationStore m_Store;

        [SetUp]
        public void Setup()
        {
            m_Loader = Substitute.For <IProjectComponentLoader>();

            m_Container = Substitute.For <IWindsorContainer>();
            m_Store = Substitute.For <IConfigurationStore>();

            m_Container.Resolve <IProjectComponentLoader>()
                       .Returns(m_Loader);

            m_ExecutingAssembly = Assembly.GetAssembly(typeof ( TestBaseInstaller ));

            m_Installer = new TestBaseInstaller();

            m_Installer.Install(m_Container,
                                m_Store);
        }

        [Test]
        public void InstallCallsLoadTest()
        {
            m_Loader.Received()
                    .Load(m_Container,
                          m_ExecutingAssembly);
        }

        [Test]
        public void InstallComponentsWasCalledTest()
        {
            Assert.True(m_Installer.WasCalledInstallComponents);
        }

        private class TestBaseInstaller : BaseInstaller <TestBaseInstaller>
        {
            public bool WasCalledInstallComponents { get; private set; }

            protected override void InstallComponents(IWindsorContainer container,
                                                      IConfigurationStore store)
            {
                base.InstallComponents(container,
                                       store);

                WasCalledInstallComponents = true;
            }
        }
    }

    //ncrunch: no coverage end
}