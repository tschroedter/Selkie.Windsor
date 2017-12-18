using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Interfaces
{
    public interface IComponentInfo
    {
        [NotNull]
        [UsedImplicitly]
        string Name { get; }

        [UsedImplicitly]
        Lifestyle Lifestyle { get; }
    }
}