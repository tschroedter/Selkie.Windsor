using Castle.Core.Logging;
using JetBrains.Annotations;

namespace Selkie.Windsor.Installers
{
    public class ProjectComponentLoaderBuilder
    {
        [NotNull]
        public static IProjectComponentLoader CreateLoader([NotNull] ILogger logger)
        {
            return new ProjectComponentLoader(logger);
        }
    }
}