using System;
using Castle.MicroKernel.Registration;

namespace Core2.Selkie.Windsor.Tests
{
    public class Installer
        : BasicConsoleInstaller,
          IWindsorInstaller
    {
        public override bool IsAssemblyNameIgnored(string assemblyName)
        {
            return string.Compare(assemblyName,
                                  "Core2.Selkie.Windsor.Tests",
                                  StringComparison.Ordinal) == 0;
        }

        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.Windsor.Tests",
                                           StringComparison.Ordinal);
        }
    }
}