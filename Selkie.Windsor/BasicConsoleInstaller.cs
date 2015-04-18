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

namespace Selkie.Windsor
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public class BasicConsoleInstaller
    {
        public void Install([NotNull] IWindsorContainer container,
                            [NotNull] IConfigurationStore store)
        {
            IEnumerable <Assembly> all = AllAssembly()
                .ToArray();

            Assembly selkieWindsor = GetSelkieWindsor(all); // need to install first, other depend on it
            container.Install(FromAssembly.Instance(selkieWindsor));

            foreach ( Assembly assembly in all )
            {
                CallAssemblyInstaller(container,
                                      assembly);
            }

            InstallComponents(container,
                              store);
        }

        [NotNull]
        private Assembly GetSelkieWindsor([NotNull] IEnumerable <Assembly> all)
        {
            Assembly assembly = all.First(IsSelkieWindsorAssembly);

            return assembly;
        }

        private static bool IsSelkieWindsorAssembly([NotNull] Assembly assembly)
        {
            string name = assembly.ManifestModule.Name;

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

            if ( IsSelkieWindsorAssembly(assembly) )
            {
                return;
            }

            if ( name.StartsWith("Selkie.",
                                 StringComparison.Ordinal) )
            {
                container.Install(FromAssembly.Instance(assembly));
            }
        }

        [NotNull]
        private IEnumerable <Assembly> AllAssembly()
        {
            // todo use .AssemblyResolved event and hook up installers ad that point
            List <Assembly> allAssembly = new List <Assembly>();

            string directory = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo directoryrInfo = new DirectoryInfo(directory);

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
            return dllInfo.Name.StartsWith("NSubstitute") || dllInfo.Name.StartsWith("NLog") || dllInfo.Name.StartsWith("NUnit") || dllInfo.Name.StartsWith("XUnit");
        }
    }

    //ncrunch: no coverage end
}