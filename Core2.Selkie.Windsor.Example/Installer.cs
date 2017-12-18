using System;
using Castle.MicroKernel.Registration;

namespace Core2.Selkie.Windsor.Example
{
    public class Installer
        : BasicConsoleInstaller,
          IWindsorInstaller
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.",
                                           StringComparison.Ordinal);
        }
    }
}