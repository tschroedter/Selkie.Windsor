using JetBrains.Annotations;

namespace Selkie.Windsor.Tests.Library
{
    public interface ITypedFactoryTest : ITypedFactory
    {
        ITransientTest Create();
        void Release([NotNull] ITransientTest transientTest);
    }
}