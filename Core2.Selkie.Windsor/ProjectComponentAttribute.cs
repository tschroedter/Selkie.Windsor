using System;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor
{
    [AttributeUsage(AttributeTargets.Class,
        Inherited = false)]
    public sealed class ProjectComponentAttribute
        : Attribute,
          IComponentInfo
    {
        [UsedImplicitly]
        public ProjectComponentAttribute()
            : this(Lifestyle.Singleton)
        {
            Name = GetType().FullName;
        }

        public ProjectComponentAttribute(Lifestyle lifestyle)
        {
            Lifestyle = lifestyle;
            Name = GetType().FullName;
        }

        public Lifestyle Lifestyle { get; }

        public string Name { get; }
    }
}