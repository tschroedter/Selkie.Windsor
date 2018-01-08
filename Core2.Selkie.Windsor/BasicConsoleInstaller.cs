using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core2.Selkie.Windsor.Internals;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor
{
    [ExcludeFromCodeCoverage]
    public abstract class BasicConsoleInstaller
    {
        [UsedImplicitly]
        protected const string SelkieWindsorDllName = "Core2.Selkie.Windsor.dll";

        [UsedImplicitly]
        protected const string PrefixForCore2SelkieDlls = "Core2.Selkie.";

        [UsedImplicitly]
        protected readonly ConsoleLogger Logger = new ConsoleLogger();

        [UsedImplicitly]
        protected IEnumerable <Assembly> AllAssemblies = new Assembly[0];

        [ExcludeFromCodeCoverage]
        public virtual void Install([NotNull] IWindsorContainer container,
                                    [NotNull] IConfigurationStore store)
        {
            AllAssemblies = AllAssembly().OrderBy(x => x.FullName).ToArray();

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            Assembly selkieWindsor = GetSelkieWindsor(AllAssemblies);

            container.Install(FromAssembly.Instance(selkieWindsor)); // need to install first, other depend on it

            AllAssemblies =
                AllAssemblies.Where(x => x != selkieWindsor &&
                                         x != entryAssembly &&
                                         !IsAssemblyNameIgnored(x.GetName()
                                                                 .Name))
                             .ToArray(); // don't install twice and avoid endless loop

            DisplayAssemblies(AllAssemblies);

            CallInstallerForAllAssemblies(container,
                                          AllAssemblies);
        }

        [UsedImplicitly]
        public virtual bool IsAssemblyNameIgnored([NotNull] string assemblyName)
        {
            return false;
        }

        [UsedImplicitly]
        public abstract bool IsAutoDetectAllowedForAssemblyName([NotNull] string assemblyName);

        [UsedImplicitly]
        // ReSharper disable once VirtualMemberNeverOverridden.Global
        protected virtual void InstallComponents([NotNull] IWindsorContainer container,
                                                 [NotNull] IConfigurationStore store)
        {
            // install not automatically detected components here
        }

        [UsedImplicitly]
        private void AddDllsToAssemblyList([NotNull] IEnumerable <FileInfo> dlls,
                                           [NotNull] ICollection <Assembly> allAssembly)
        {
            foreach ( FileInfo fileInfo in dlls )
            {
                Logger.Debug($"Trying to load assembly for DLL {fileInfo.FullName}...");

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
                    string message = $"Could not get assembly for DLL '{fileInfo.FullName}'!";

                    Logger.Error(exception,
                                 message);

                    throw;
                }
            }
        }

        [NotNull]
        private IEnumerable <Assembly> AllAssembly()
        {
            var allAssembly = new List <Assembly>();

            string directory = AppDomain.CurrentDomain.BaseDirectory;

            var directoryrInfo = new DirectoryInfo(directory);

            FileInfo[] dlls = directoryrInfo.GetFiles("*.dll");

            AddDllsToAssemblyList(dlls,
                                  allAssembly);

            return allAssembly;
        }

        private void CallAssemblyInstaller([NotNull] IWindsorContainer container,
                                           [NotNull] Assembly assembly)
        {
            string assemblyName = assembly.GetName().Name;

            Logger.Info($"{assemblyName} - Checking...");

            if ( IsIgnoredAssemblyName(assemblyName) )
            {
                Logger.Warn($"{assemblyName} - Ignored!");

                return;
            }

            if ( IsAutoDetectAllowedForAssemblyName(assemblyName) )
            {
                Logger.Info($"{assemblyName} - Processing...");

                container.Install(FromAssembly.Instance(assembly));
            }
            else
            {
                Logger.Warn($"{assemblyName} - Ignored! (because of IsAllowedAutoDetect(\"{assemblyName}\") returned false')");
            }
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
            Logger.Info("Running installers for the following assemblies:");

            foreach ( Assembly assembly in all )
            {
                Logger.Info(assembly.FullName);
            }
        }

        private Assembly FindAssembly([NotNull] string fullname)
        {
            AssemblyName assemblyName = AssemblyName.GetAssemblyName(fullname);
            Logger.Debug($"Found AssemblyName {assemblyName.FullName}...");

            Assembly assembly = Assembly.Load(assemblyName);
            Logger.Debug($"...loaded assembly {assembly.FullName}!");

            return assembly;
        }

        [NotNull]
        private Assembly GetSelkieWindsor([NotNull] IEnumerable <Assembly> all)
        {
            Assembly assembly = all.FirstOrDefault(IsCore2SelkieWindsorAssembly);

            if ( assembly == null )
            {
                throw new ArgumentException($"Could not find {SelkieWindsorDllName} in application folder!");
            }

            return assembly;
        }

        private bool IsCore2SelkieWindsorAssembly([NotNull] Assembly assembly)
        {
            string name = assembly.ManifestModule.Name;

            return IsCore2SelkieWindsorAssemblyName(name);
        }

        private bool IsCore2SelkieWindsorAssemblyName(string name)
        {
            return string.Compare(name,
                                  SelkieWindsorDllName,
                                  StringComparison.CurrentCultureIgnoreCase) == 0;
        }

        private bool IsFilenameIgnored([NotNull] string filename)
        {
            bool isFileIgnored = !filename.StartsWith(PrefixForCore2SelkieDlls) &&
                                 !IsAutoDetectAllowedForAssemblyName(filename);

            if ( isFileIgnored )
            {
                Logger.Debug($"...{filename} " +
                             $"was ignored because of fixed prefix '{PrefixForCore2SelkieDlls}' or " +
                             $"IsAllowedAutoDetect(\"{filename}\") returned false!");
            }

            return isFileIgnored;
        }

        private bool IsIgnoredAssemblyName(string name)
        {
            return name.IndexOf("Console",
                                StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                   name.IndexOf("SpecFlow",
                                StringComparison.InvariantCultureIgnoreCase) >= 0;
        }
    }
}