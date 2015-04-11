using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Selkie.Windsor
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

            IProjectComponentLoader loader = container.Resolve <IProjectComponentLoader>();

            loader.Load(container,
                        assembly);

            container.Release(loader);

            InstallComponents(container,
                              store);
        }

        protected virtual void PreInstallComponents([NotNull] IWindsorContainer container,
                                                    [NotNull] IConfigurationStore store)
        {
        }

        [NotNull]
        internal Assembly GetAssembly()
        {
            return Assembly.GetAssembly(typeof ( T ));
        }

        protected virtual void InstallComponents([NotNull] IWindsorContainer container,
                                                 [NotNull] IConfigurationStore store)
        {
        }
    }
}