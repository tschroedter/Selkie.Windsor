﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.Windsor
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public abstract class BasicConsoleInstaller
    {
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

            var logger = container.Resolve <ILogger>();

            DisplayAssemblies(allAssemblies,
                              logger);

            CallInstallerForAllAssemblies(container,
                                          allAssemblies,
                                          logger);

            container.Release(logger);
        }

        private void CallInstallerForAllAssemblies(IWindsorContainer container,
                                                   IEnumerable <Assembly> allAssemblies,
                                                   ILogger logger)
        {
            foreach ( Assembly assembly in allAssemblies )
            {
                CallAssemblyInstaller(container,
                                      assembly,
                                      logger);
            }
        }

        private static void DisplayAssemblies(IEnumerable <Assembly> all,
                                              ILogger logger)
        {
            logger.Info("Running installers for the following assemblies:");

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
                                           [NotNull] Assembly assembly,
                                           [NotNull] ILogger logger)
        {
            string name = assembly.ManifestModule.Name;

            logger.Info("{0} - Checking...".Inject(name));

            if ( IsIgnoredAssemblyName(name) )
            {
                Console.WriteLine("{0} - Ignored!",
                                  name);

                return;
            }

            if ( name.StartsWith(GetPrefixOfDllsToInstall(),
                                 StringComparison.Ordinal) )
            {
                logger.Info("{0} - Processing...".Inject(name));

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
                                StringComparison
                                    .InvariantCultureIgnoreCase) >= 0;
        }

        [NotNull]
        private IEnumerable <Assembly> AllAssembly()
        {
            // todo use .AssemblyResolved event and hook up installers ad that point
            var allAssembly = new List <Assembly>();

            string directory = AppDomain.CurrentDomain.BaseDirectory;

            var directoryrInfo = new DirectoryInfo(directory);

            FileInfo[] dlls = directoryrInfo.GetFiles("*.dll");

            AddDllsToAssemblyList(dlls,
                                  allAssembly);

            return allAssembly;
        }

        private static void AddDllsToAssemblyList([NotNull] IEnumerable <FileInfo> dlls,
                                                  [NotNull] ICollection <Assembly> allAssembly)
        {
            foreach ( FileInfo dllInfo in dlls )
            {
                if ( IsIgnored(dllInfo) )
                {
                    continue;
                }

                AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllInfo.Name);

                if ( assemblyName.Name != dllInfo.Name )
                {
                    Assembly assm = Assembly.Load(assemblyName);

                    allAssembly.Add(assm);
                }
            }
        }

        private static bool IsIgnored([NotNull] FileInfo dllInfo)
        {
            return dllInfo.Name.StartsWith("NSubstitute") || dllInfo.Name.StartsWith("NLog") ||
                   dllInfo.Name.StartsWith("NUnit") || dllInfo.Name.StartsWith("XUnit");
        }
    }

    //ncrunch: no coverage end
}