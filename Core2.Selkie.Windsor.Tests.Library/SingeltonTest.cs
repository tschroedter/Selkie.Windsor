using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Tests.Library
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Singleton)]
    [UsedImplicitly]
    public class SingeltonTest : ISingeltonTest
    {
        public int SomeInteger { get; set; }
    }

    public interface ISingeltonTest
    {
        int SomeInteger { get; set; }
    }
}