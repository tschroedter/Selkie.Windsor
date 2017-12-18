using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor
{
    public abstract class BaseInstaller <T> : IWindsorInstaller
        where T : class
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            PreInstallComponents(container,
                                 store);

            Assembly assembly = GetAssembly();

            LoadFromAssembly(container,
                             assembly);

            InstallComponents(container,
                              store);
        }

        [UsedImplicitly]
        public abstract bool IsAutoDetectAllowedForAssemblyName([NotNull] string assemblyName);

        [UsedImplicitly]
        protected virtual void InstallComponents([NotNull] IWindsorContainer container,
                                                 [NotNull] IConfigurationStore store)
        {
            // install not automatically detected components here
        }

        protected virtual void PreInstallComponents([NotNull] IWindsorContainer container,
                                                    [NotNull] IConfigurationStore store)
        {
            // install components here before automatic detection
        }

        [NotNull]
        [UsedImplicitly]
        internal Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof( T ));
        }

        private void LoadFromAssembly(IWindsorContainer container,
                                      Assembly assembly)
        {
            string assemblyName = assembly.GetName().Name;

            if ( !IsAutoDetectAllowedForAssemblyName(assemblyName) )
            {
                return;
            }

            var loader = container.Resolve <IProjectComponentLoader>();

            loader.Load(container,
                        assembly);

            container.Release(loader);
        }
    }
}