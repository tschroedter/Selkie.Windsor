using System.Diagnostics.CodeAnalysis;
using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Selkie.Windsor.Installers;

namespace Selkie.Windsor
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class Installer : BaseInstaller <Installer>
    {
        protected override void PreInstallComponents(IWindsorContainer container,
                                                     IConfigurationStore store)
        {
            base.PreInstallComponents(container,
                                      store);

            container.AddFacility <TypedFactoryFacility>();
            container.AddFacility <StartableFacility>();

            var loggerInstaller = new LoggerInstaller();
            loggerInstaller.Install(container,
                                    store);

            var loaderInstaller = new ProjectComponentLoaderInstaller();
            loaderInstaller.Install(container,
                                    store);
        }
    }

    //ncrunch: no coverage end
}