using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.Core.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Core2.Selkie.Windsor.Interfaces;
using JetBrains.Annotations;

namespace Core2.Selkie.Windsor.ProjectComponents
{
    [ExcludeFromCodeCoverage]
    internal sealed class ProjectComponentLoader : IProjectComponentLoader
    {
        public ProjectComponentLoader([NotNull] ILogger logger)
        {
            m_Logger = logger;
        }

        private readonly ILogger m_Logger;

        public void Load(IWindsorContainer container,
                         Assembly assembly)
        {
            m_Logger.Info($"Loading ProjectComponent's for module '{assembly.ManifestModule.Name}'...");

            InstallLifestyleSingelton(container,
                                      assembly);

            InstallLifestyleTransient(container,
                                      assembly);

            InstallLifestyleStartable(container,
                                      assembly);

            InstallITypedFactory(container,
                                 assembly);
        }

        private static bool IsLifestyleSingelton(ProjectComponentAttribute component)
        {
            return component != null && component.Lifestyle == Lifestyle.Singleton;
        }

        private static bool IsLifestyleStartable(ProjectComponentAttribute component)
        {
            return component != null && component.Lifestyle == Lifestyle.Startable;
        }

        private static bool IsLifestyleTransient(ProjectComponentAttribute component)
        {
            return component != null && component.Lifestyle == Lifestyle.Transient;
        }

        private ProjectComponentAttribute[] GetProjectComponentAttributes(Type type)
        {
            object[] customAttributes = type.GetCustomAttributes(false);

            ProjectComponentAttribute[] projectComponentAttributes =
                customAttributes.Select(customAttribute => customAttribute as ProjectComponentAttribute)
                                .Where(x => x != null)
                                .ToArray();

            return projectComponentAttributes;
        }

        private void InstallITypedFactory(IWindsorContainer container,
                                          Assembly assembly)
        {
            m_Logger.Info($"{assembly.ManifestModule.Name}: ITypedFactory...");
            container.Install()
                     .Register(
                               Types.FromAssembly(assembly)
                                    .BasedOn <ITypedFactory>()
                                    .If(IsNotUnitTestTypedFactory)
                                    .Configure(c => c.AsFactory().LifestyleTransient()));
        }

        private void InstallLifestyleSingelton(IWindsorContainer container,
                                               Assembly assembly)
        {
            m_Logger.Info($"{assembly.ManifestModule.Name}: LifestyleSingelton...");
            container.Install()
                     .Register(
                               Classes.FromAssembly(assembly)
                                      .Where(LifestyleSingelton)
                                      .WithServiceDefaultInterfaces()
                                      .LifestyleSingleton());
        }

        private void InstallLifestyleStartable(IWindsorContainer container,
                                               Assembly assembly)
        {
            m_Logger.Info($"{assembly.ManifestModule.Name}: LifestyleStartable...");
            container.Install()
                     .Register(
                               Classes.FromAssembly(assembly)
                                      .Where(LifestyleStartable)
                                      .WithServiceDefaultInterfaces()
                                      .LifestyleSingleton());
        }

        private void InstallLifestyleTransient(IWindsorContainer container,
                                               Assembly assembly)
        {
            m_Logger.Info($"{assembly.ManifestModule.Name}: LifestyleTransient...");
            container.Install()
                     .Register(
                               Classes.FromAssembly(assembly)
                                      .Where(LifestyleTransient)
                                      .WithServiceDefaultInterfaces()
                                      .LifestyleTransient());
        }

        private bool IsLifestyle([NotNull] Type type,
                                 Func <ProjectComponentAttribute, bool> isLifestyle)
        {
            ProjectComponentAttribute[] attributes = GetProjectComponentAttributes(type);

            ProjectComponentAttribute[] selected = attributes.Where(isLifestyle).ToArray();

            LogAttributes(type,
                          selected);

            return selected.Any();
        }

        private bool IsNotUnitTestTypedFactory(Type type)
        {
            bool isUnitTest = type.FullName.Contains("NUnit") ||
                              type.FullName.Contains("XUnit");

            if ( !isUnitTest )
            {
                LogTypeAndLifestyle(type.FullName,
                                    "ITypedFactory");
            }

            return !isUnitTest;
        }

        private bool LifestyleSingelton([NotNull] Type type)
        {
            return IsLifestyle(type,
                               IsLifestyleSingelton);
        }

        private bool LifestyleStartable([NotNull] Type type)
        {
            return IsLifestyle(type,
                               IsLifestyleStartable);
        }

        private bool LifestyleTransient([NotNull] Type type)
        {
            return IsLifestyle(type,
                               IsLifestyleTransient);
        }

        private void LogAttributes([NotNull] Type type,
                                   [NotNull] ProjectComponentAttribute[] attributes)
        {
            if ( !attributes.Any() )
            {
                return;
            }

            var builder = new StringBuilder();

            foreach ( ProjectComponentAttribute customAttribute in attributes )
            {
                ProjectComponentAttribute componentAttribute = customAttribute;
                ProjectComponentAttribute attribute = componentAttribute;

                builder.Append($"{attribute.Lifestyle} ");
            }

            LogTypeAndLifestyle(type.FullName,
                                builder.ToString());
        }

        private void LogTypeAndLifestyle(string type,
                                         string lifestyle)
        {
            m_Logger.Info($"{type} has the following Lifestyle: {lifestyle}");
        }
    }
}