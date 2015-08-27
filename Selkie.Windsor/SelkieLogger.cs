using System;
using Castle.Core.Logging;
using JetBrains.Annotations;

namespace Selkie.Windsor
{
    [ProjectComponent(Lifestyle.Transient)]
    public class SelkieLogger : ISelkieLogger
    {
        private readonly ILogger m_Logger;

        public SelkieLogger([NotNull] ILogger logger)
        {
            m_Logger = logger;
        }

        public void Debug(string message)
        {
            m_Logger.Debug(message);
        }

        public void Debug(string message,
                          Exception exception)
        {
            m_Logger.Debug(message,
                           exception);
        }

        public void Error(string message)
        {
            m_Logger.Error(message);
        }

        public void Error(string message,
                          Exception exception)
        {
            m_Logger.Error(message,
                           exception);
        }

        public void Fatal(string message)
        {
            m_Logger.Fatal(message);
        }

        public void Fatal(string message,
                          Exception exception)
        {
            m_Logger.Fatal(message,
                           exception);
        }

        public void Info(string message)
        {
            m_Logger.Info(message);
        }

        public void Info(string message,
                         Exception exception)
        {
            m_Logger.Info(message,
                          exception);
        }

        public void Warn(string message)
        {
            m_Logger.Warn(message);
        }

        public void Warn(string message,
                         Exception exception)
        {
            m_Logger.Warn(message,
                          exception);
        }

        public void Debug(string format,
                          params object[] args)
        {
            m_Logger.DebugFormat(format,
                                 args);
        }

        public void Error(string format,
                          params object[] args)
        {
            m_Logger.ErrorFormat(format,
                                 args);
        }

        public void Fatal(string format,
                          params object[] args)
        {
            m_Logger.FatalFormat(format,
                                 args);
        }

        public void Info(string format,
                         params object[] args)
        {
            m_Logger.InfoFormat(format,
                                args);
        }

        public void Warn(string format,
                         params object[] args)
        {
            m_Logger.WarnFormat(format,
                                args);
        }
    }
}