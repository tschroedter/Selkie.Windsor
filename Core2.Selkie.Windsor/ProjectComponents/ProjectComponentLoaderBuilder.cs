using System.Diagnostics.CodeAnalysis;
using Castle.Core.Logging;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.ProjectComponents
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    internal class ProjectComponentLoaderBuilder
    {
        [NotNull]
        public static IProjectComponentLoader CreateLoader([NotNull] ILogger logger)
        {
            return new ProjectComponentLoader(logger);
        }
    }
}