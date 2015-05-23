using System;

namespace Selkie.Windsor
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ProjectComponentAttribute
        : Attribute,
          IComponentInfo
    {
        private readonly string m_Name;

        public ProjectComponentAttribute()
            : this(Lifestyle.Singleton)
        {
            m_Name = GetType().FullName;
        }

        public ProjectComponentAttribute(Lifestyle lifestyle)
        {
            Lifestyle = lifestyle;
            m_Name = GetType().FullName;
        }

        public Lifestyle Lifestyle { get; private set; }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
    }
}