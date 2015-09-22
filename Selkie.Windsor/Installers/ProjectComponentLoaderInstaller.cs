using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Selkie.Windsor.Installers
{
    public class ProjectComponentLoaderInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            if ( !container.Kernel.HasComponent(typeof ( IProjectComponentLoader )) )
            {
                var logger = container.Resolve <ILogger>();

                // ReSharper disable MaximumChainedReferences
                container.Register(Component.For <IProjectComponentLoader>()
                                            .UsingFactoryMethod(() => ProjectComponentLoaderBuilder.CreateLoader(logger))
                                            .LifestyleTransient());
                // ReSharper restore MaximumChainedReferences
            }
        }
    }
}