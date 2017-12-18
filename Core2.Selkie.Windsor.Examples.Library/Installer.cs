using System;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Examples.Library
{
    [UsedImplicitly]
    public class Installer : BaseInstaller <Installer>
    {
        public override bool IsAutoDetectAllowedForAssemblyName(string assemblyName)
        {
            return assemblyName.StartsWith("Core2.Selkie.",
                                           StringComparison.Ordinal);
        }
    }
}