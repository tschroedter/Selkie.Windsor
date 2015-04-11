using JetBrains.Annotations;

namespace Selkie.Windsor
{
    public interface IComponentInfo
    {
        [NotNull]
        string Name { get; }

        Lifestyle Lifestyle { get; }
    }
}