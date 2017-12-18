using System.Reflection;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.Interfaces
{
    public interface IProjectComponentLoader
    {
        void Load([NotNull] IWindsorContainer container,
                  [NotNull] Assembly assembly);
    }
}