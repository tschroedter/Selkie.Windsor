using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.ProjectComponents
{
    [ExcludeFromCodeCoverage]
    internal class ProjectComponentLoaderInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] [UsedImplicitly] IConfigurationStore store)
        {
            if ( container.Kernel.HasComponent(typeof( IProjectComponentLoader )) )
            {
                return;
            }

            var logger = container.Resolve <ILogger>();

            container.Register(Component.For <IProjectComponentLoader>()
                                        .UsingFactoryMethod(() => ProjectComponentLoaderBuilder.CreateLoader(logger))
                                        .LifestyleTransient());
        }
    }
}