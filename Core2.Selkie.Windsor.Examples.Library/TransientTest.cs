using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Examples.Library
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    [UsedImplicitly]
    public class TransientTest : ITransientTest
    {
        public int SomeInteger { get; set; }
    }

    public interface ITransientTest
    {
        int SomeInteger { get; set; }
    }
}