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

            LoadFromAssembly(container,
                             assembly);

            InstallComponents(container,
                              store);
        }

        [NotNull] // todo change method to return bool: bool InstallDllWithName(string currentName);
        public abstract string GetPrefixOfDllsToInstall();

        private void LoadFromAssembly(IWindsorContainer container,
                                      Assembly assembly)
        {
            string name = assembly.ManifestModule.Name;

            if ( !name.StartsWith(GetPrefixOfDllsToInstall()) )
            {
                return;
            }

            var loader = container.Resolve <IProjectComponentLoader>();

            loader.Load(container,
                        assembly);

            container.Release(loader);
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