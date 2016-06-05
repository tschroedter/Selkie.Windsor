﻿using System.Diagnostics.CodeAnalysis;

namespace Selkie.Windsor.Tests.Library
{
    [ExcludeFromCodeCoverage]
    [ProjectComponent(Lifestyle.Transient)]
    public class TransientTest : ITransientTest
    {
        public int SomeInteger { get; set; }
    }

    public interface ITransientTest
    {
        int SomeInteger { get; set; }
    }
}