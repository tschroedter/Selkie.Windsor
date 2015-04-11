using JetBrains.Annotations;

namespace Selkie.Windsor.Examples.Library
{
    public interface ITypedFactoryTest : ITypedFactory
    {
        ITransientTest Create();
        void Release([NotNull] ITransientTest transientTest);
    }
}