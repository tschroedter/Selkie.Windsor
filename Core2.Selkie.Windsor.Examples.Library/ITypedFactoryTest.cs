﻿using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Examples.Library
{
    public interface ITypedFactoryTest : ITypedFactory
    {
        ITransientTest Create();
        void Release([NotNull] ITransientTest transientTest);
    }
}