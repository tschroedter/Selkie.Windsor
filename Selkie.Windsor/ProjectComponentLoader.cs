﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.Core.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using JetBrains.Annotations;
using Selkie.Windsor.Extensions;

namespace Selkie.Windsor
{
    // ReSharper disable MaximumChainedReferences
    public class ProjectComponentLoader : IProjectComponentLoader
    {
        private readonly ILogger m_Logger;

        public ProjectComponentLoader([NotNull] ILogger logger)
        {
            m_Logger = logger;
        }

        public virtual void Load(IWindsorContainer container,
                                 Assembly assembly)
        {
            m_Logger.Info("Loading ProjectComponent's for module '{0}'...".Inject(assembly.ManifestModule.Name));

            InstallLifestyleSingelton(container,
                                      assembly);

            InstallLifestyleTransient(container,
                                      assembly);

            InstallLifestyleStartable(container,
                                      assembly);

            InstallITypedFactory(container,
                                 assembly);
        }

        private void InstallITypedFactory(IWindsorContainer container,
                                          Assembly assembly)
        {
            // todo maybe there is a way to log which factories are discovered/registered
            m_Logger.Info("{0}: ITypedFactory...".Inject(assembly.ManifestModule.Name));
            container.Install()
                     .Register(Types.FromAssembly(assembly)
                                    .BasedOn <ITypedFactory>()
                                    .If(IsNotUnitTest)
                                    .Configure(c => c.AsFactory()
                                                     .LifestyleTransient()));
        }

        private void InstallLifestyleStartable(IWindsorContainer container,
                                               Assembly assembly)
        {
            m_Logger.Info("{0}: LifestyleStartable...".Inject(assembly.ManifestModule.Name));
            container.Install()
                     .Register(Classes.FromAssembly(assembly)
                                      .Where(LifestyleStartable)
                                      .WithServiceDefaultInterfaces()
                                      .LifestyleSingleton());
        }

        private void InstallLifestyleTransient(IWindsorContainer container,
                                               Assembly assembly)
        {
            m_Logger.Info("{0}: LifestyleTransient...".Inject(assembly.ManifestModule.Name));
            container.Install()
                     .Register(Classes.FromAssembly(assembly)
                                      .Where(LifestyleTransient)
                                      .WithServiceDefaultInterfaces()
                                      .LifestyleTransient());
        }

        private void InstallLifestyleSingelton(IWindsorContainer container,
                                               Assembly assembly)
        {
            m_Logger.Info("{0}: LifestyleSingelton...".Inject(assembly.ManifestModule.Name));
            container.Install()
                     .Register(Classes.FromAssembly(assembly)
                                      .Where(LifestyleSingelton)
                                      .WithServiceDefaultInterfaces()
                                      .LifestyleSingleton());
        }

        private static bool IsNotUnitTest(Type c)
        {
            return !c.FullName.Contains("NUnit") && !c.FullName.Contains("XUnit");
        }

        protected bool IsLifestyle([NotNull] Type type,
                                   Func <ProjectComponentAttribute, bool> isLifestyle)
        {
            if ( IsExcludedNamespace(type) )
            {
                return false;
            }

            ProjectComponentAttribute[] attributes = GetProjectComponentAttributes(type);

            ProjectComponentAttribute[] selected = attributes.Where(isLifestyle)
                                                             .ToArray();

            LogAttributes(type,
                          selected);

            return selected.Any();
        }

        protected bool LifestyleSingelton([NotNull] Type type)
        {
            return IsLifestyle(type,
                               IsLifestyleSingelton);
        }

        protected bool LifestyleTransient([NotNull] Type type)
        {
            return IsLifestyle(type,
                               IsLifestyleTransient);
        }

        protected bool LifestyleStartable([NotNull] Type type)
        {
            return IsLifestyle(type,
                               IsLifestyleStartable);
        }

        private ProjectComponentAttribute[] GetProjectComponentAttributes(Type type)
        {
            object[] customAttributes = type.GetCustomAttributes(false);

            ProjectComponentAttribute[] projectComponentAttributes = customAttributes.Select(customAttribute => customAttribute as ProjectComponentAttribute)
                                                                                     .Where(x => x != null)
                                                                                     .ToArray();

            return projectComponentAttributes;
        }

        private static bool IsExcludedNamespace([NotNull] Type type)
        {
            string fullName = type.FullName;

            return !fullName.StartsWith("Selkie.");
        }

        private void LogAttributes([NotNull] Type type,
                                   [NotNull] ProjectComponentAttribute[] attributes)
        {
            if ( !attributes.Any() )
            {
                return;
            }

            StringBuilder builder = new StringBuilder();

            foreach ( ProjectComponentAttribute customAttribute in attributes )
            {
                ProjectComponentAttribute componentAttribute = customAttribute;
                ProjectComponentAttribute attribute = componentAttribute;

                builder.Append("{0} ".Inject(attribute.Lifestyle));
            }

            m_Logger.Info("{0} has the following Lifestyle: {1}".Inject(type.FullName,
                                                                        builder));
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        protected static bool IsLifestyleSingelton(ProjectComponentAttribute component)
        {
            return component != null && component.Lifestyle == Lifestyle.Singleton;
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        protected static bool IsLifestyleTransient(ProjectComponentAttribute component)
        {
            return component != null && component.Lifestyle == Lifestyle.Transient;
        }

        // ReSharper disable once CodeAnnotationAnalyzer
        protected static bool IsLifestyleStartable(ProjectComponentAttribute component)
        {
            return component != null && component.Lifestyle == Lifestyle.Startable;
        }
    }
}