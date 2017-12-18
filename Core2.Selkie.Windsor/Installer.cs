using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.Facilities.Startable;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.Windsor.Internals;
using Core2.Selkie.Windsor.ProjectComponents;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class Installer : BaseInstaller <Installer>
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.",
                                           StringComparison.Ordinal);
        }

        protected override void InstallComponents(IWindsorContainer container,
                                                  IConfigurationStore store)
        {
            // install not automatically detected components here 
        }

        protected override void PreInstallComponents(IWindsorContainer container,
                                                     IConfigurationStore store)
        {
            base.PreInstallComponents(container,
                                      store);

            CheckAndAddFacilities(container);
            CheckAndAddResolvers(container);

            var loggerInstaller = new LoggerInstaller();
            loggerInstaller.Install(container,
                                    store);

            var loaderInstaller = new ProjectComponentLoaderInstaller();
            loaderInstaller.Install(container,
                                    store);
        }

        private static void CheakAndAddArrayResolver(IWindsorContainer container)
        {
            if ( !container.Kernel.HasComponent(typeof( ArrayResolver )) )
            {
                container.Kernel.Resolver
                         .AddSubResolver(new ArrayResolver(container.Kernel));
            }
        }

        private static void CheakAndAddCollectionResolver(IWindsorContainer container)
        {
            if ( !container.Kernel.HasComponent(typeof( CollectionResolver )) )
            {
                container.Kernel.Resolver
                         .AddSubResolver(new CollectionResolver(container.Kernel));
            }
        }

        private static void CheakAndAddListResolver(IWindsorContainer container)
        {
            if ( !container.Kernel.HasComponent(typeof( ListResolver )) )
            {
                container.Kernel.Resolver
                         .AddSubResolver(new ListResolver(container.Kernel));
            }
        }

        private static void CheckAndAddFacilities(IWindsorContainer container)
        {
            CheckAndAddTypedFactoryFacility(container);
            CheckAndAddStartableFacility(container);
        }

        private static void CheckAndAddResolvers(IWindsorContainer container)
        {
            CheakAndAddArrayResolver(container);
            CheakAndAddListResolver(container);
            CheakAndAddCollectionResolver(container);
        }

        private static void CheckAndAddStartableFacility(IWindsorContainer container)
        {
            if ( !container.Kernel.GetFacilities().Any(x => x is StartableFacility) )
            {
                container.AddFacility <StartableFacility>();
            }
        }

        private static void CheckAndAddTypedFactoryFacility(IWindsorContainer container)
        {
            if ( !container.Kernel.GetFacilities().Any(x => x is TypedFactoryFacility) )
            {
                container.AddFacility <TypedFactoryFacility>();
            }
        }
    }
}