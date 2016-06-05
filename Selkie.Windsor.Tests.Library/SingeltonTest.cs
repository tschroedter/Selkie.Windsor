using System.Diagnostics.CodeAnalysis;

namespace Selkie.Windsor.Tests.Library
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Singleton)]
    public class SingeltonTest : ISingeltonTest
    {
        public int SomeInteger { get; set; }
    }

    public interface ISingeltonTest
    {
        int SomeInteger { get; set; }
    }
}