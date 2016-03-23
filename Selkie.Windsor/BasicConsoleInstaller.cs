using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JetBrains.Annotations;
using NLog;
using Selkie.Windsor.Extensions;

namespace Selkie.Windsor
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public abstract class BasicConsoleInstaller
    {
        private const string PrefixForSelkieDlls = "Selkie.";

        private readonly Logger m_Logger = LogManager.GetLogger("Selkie.Windsor.BasicConsoleInstaller");

        [NotNull]
        public abstract string GetPrefixOfDllsToInstall();

        [ExcludeFromCodeCoverage]
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            IEnumerable <Assembly> allAssemblies = AllAssembly().OrderBy(x => x.FullName).ToArray();

            Assembly selkieWindsor = GetSelkieWindsor(allAssemblies);
            container.Install(FromAssembly.Instance(selkieWindsor)); // need to install first, other depend on it
            allAssemblies = allAssemblies.Where(x => !IsSelkieWindsorAssembly(x));

            DisplayAssemblies(allAssemblies);

            CallInstallerForAllAssemblies(container,
                                          allAssemblies);
        }

        private void CallInstallerForAllAssemblies([NotNull] IWindsorContainer container,
                                                   [NotNull] IEnumerable <Assembly> allAssemblies)
        {
            foreach ( Assembly assembly in allAssemblies )
            {
                CallAssemblyInstaller(container,
                                      assembly);
            }
        }

        private void DisplayAssemblies(IEnumerable <Assembly> all)
        {
            m_Logger.Info("Running installers for the following assemblies:");

            foreach ( Assembly assembly in all )
            {
                Console.WriteLine(assembly.FullName);
            }
        }

        [NotNull]
        private Assembly GetSelkieWindsor([NotNull] IEnumerable <Assembly> all)
        {
            Assembly assembly = all.First(IsSelkieWindsorAssembly);

            return assembly;
        }

        private bool IsSelkieWindsorAssembly([NotNull] Assembly assembly)
        {
            string name = assembly.ManifestModule.Name;

            return IsSelkieWindsorAssemblyName(name);
        }

        private bool IsSelkieWindsorAssemblyName(string name)
        {
            return string.Compare(name,
                                  "Selkie.Windsor.dll",
                                  StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        protected virtual void InstallComponents([NotNull] IWindsorContainer container,
                                                 [NotNull] IConfigurationStore store)
        {
        }

        private void CallAssemblyInstaller([NotNull] IWindsorContainer container,
                                           [NotNull] Assembly assembly)
        {
            string name = assembly.ManifestModule.Name;

            m_Logger.Info("{0} - Checking...".Inject(name));

            if ( IsIgnoredAssemblyName(name) )
            {
                Console.WriteLine("{0} - Ignored!",
                                  name);

                return;
            }

            if ( name.StartsWith(GetPrefixOfDllsToInstall(),
                                 StringComparison.Ordinal) )
            {
                m_Logger.Info("{0} - Processing...".Inject(name));

                container.Install(FromAssembly.Instance(assembly));
            }
            else
            {
                Console.WriteLine("{0} - Ignored! (because of prefix filter '{1}')",
                                  name,
                                  GetPrefixOfDllsToInstall());
            }
        }

        private bool IsIgnoredAssemblyName(string name)
        {
            return name.IndexOf("Console",
                                StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                   name.IndexOf("SpecFlow",
                                StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        [NotNull]
        private IEnumerable <Assembly> AllAssembly()
        {
            // todo use .AssemblyResolved event andd hook up installers ad that point
            var allAssembly = new List <Assembly>();

            string directory = AppDomain.CurrentDomain.BaseDirectory;

            var directoryrInfo = new DirectoryInfo(directory);

            FileInfo[] dlls = directoryrInfo.GetFiles("*.dll");

            AddDllsToAssemblyList(dlls,
                                  allAssembly);

            return allAssembly;
        }

        private void AddDllsToAssemblyList([NotNull] IEnumerable <FileInfo> dlls,
                                           [NotNull] ICollection <Assembly> allAssembly)
        {
            foreach ( FileInfo fileInfo in dlls )
            {
                m_Logger.Debug("Trying to load assembly for DLL {0}...".Inject(fileInfo.FullName));

                if ( IsFilenameIgnored(fileInfo.Name) )
                {
                    continue;
                }

                try
                {
                    Assembly assembly = FindAssembly(fileInfo.FullName);

                    allAssembly.Add(assembly);
                }
                catch ( Exception exception )
                {
                    string message = "Could not get assembly for DLL '{0}'!".Inject(fileInfo.FullName);

                    m_Logger.Error(exception,
                                   message);

                    throw;
                }
            }
        }

        private Assembly FindAssembly([NotNull] string fullname)
        {
            AssemblyName assemblyName = AssemblyName.GetAssemblyName(fullname);
            m_Logger.Debug("Found AssemblyName {0}...".Inject(assemblyName.FullName));

            Assembly assembly = Assembly.Load(assemblyName);
            m_Logger.Debug("...loaded assembly {0}!".Inject(assembly.FullName));

            return assembly;
        }

        private bool IsFilenameIgnored([NotNull] string filename)
        {
            bool isFileIgnored = !filename.StartsWith(PrefixForSelkieDlls) &&
                                 !filename.StartsWith(GetPrefixOfDllsToInstall());

            if ( isFileIgnored )
            {
                m_Logger.Debug("...{0} ".Inject(filename) +
                               "was ignored because of fixed prefix '{0}' or ".Inject(PrefixForSelkieDlls) +
                               "custom prefix '{0}'!".Inject(GetPrefixOfDllsToInstall()));
            }

            return isFileIgnored;
        }
    }

    //ncrunch: no coverage end
}