using Castle.Facilities.Logging;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Selkie.Windsor.Installers
{
    public class LoggerInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            container.AddFacility <LoggingFacility>(f => f.UseNLog());
        }
    }
}