using System.Reflection;
using Castle.Windsor;
using JetBrains.Annotations;

namespace Selkie.Windsor
{
    public interface IProjectComponentLoader
    {
        void Load([NotNull] IWindsorContainer container,
                  [NotNull] Assembly assembly);
    }
}