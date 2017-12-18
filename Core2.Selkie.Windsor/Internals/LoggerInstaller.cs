using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Facilities.Logging;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Internals
{
    [ExcludeFromCodeCoverage]
    internal class LoggerInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] [UsedImplicitly] IConfigurationStore store)
        {
            if ( !container.Kernel.GetFacilities().Any(x => x is LoggingFacility) )
            {
                container.AddFacility <LoggingFacility>(f => f.LogUsing <NLogFactory>().WithConfig("nlog.config"));
            }
        }
    }
}