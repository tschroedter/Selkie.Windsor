using System;
using System.Diagnostics.CodeAnalysis;

namespace Core2.Selkie.Windsor.Tests.Library
{
    [ExcludeFromCodeCoverage]
    public class Installer : BaseInstaller <Installer>
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.Windsor.Tests",
                                           StringComparison.Ordinal);
        }
    }
}