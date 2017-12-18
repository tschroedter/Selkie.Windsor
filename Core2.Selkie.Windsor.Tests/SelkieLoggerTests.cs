using System;
using Castle.Core.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Core2.Selkie.Windsor.Tests
{
    public sealed class SelkieLoggerTests
    {
        private ILogger m_Logger;
        private SelkieLogger m_Sut;

        [Test]
        public void Debug_CallsLogger_ForFormatAndArgs()
        {
            // Arrange
            // Act
            m_Sut.Debug("Hello {0}",
                        "World");

            // Assert
            m_Logger.Received().DebugFormat("Hello {0}",
                                            Arg.Is <object[]>(x => ( string ) x [ 0 ] == "World"));
        }

        [Test]
        public void Debug_CallsLogger_ForString()
        {
            // Arrange
            // Act
            m_Sut.Debug("Hello");

            // Assert
            m_Logger.Received().Debug("Hello");
        }

        [Test]
        public void Debug_CallsLogger_ForStringAndException()
        {
            // Arrange
            var exception = new Exception();

            // Act
            m_Sut.Debug("Hello",
                        exception);

            // Assert
            m_Logger.Received().Debug("Hello",
                                      exception);
        }

        [Test]
        public void Error_CallsLogger_ForFormatAndArgs()
        {
            // Arrange
            // Act
            m_Sut.Error("Hello {0}",
                        "World");

            // Assert
            m_Logger.Received().ErrorFormat("Hello {0}",
                                            Arg.Is <object[]>(x => ( string ) x [ 0 ] == "World"));
        }

        [Test]
        public void Error_CallsLogger_ForString()
        {
            // Arrange
            // Act
            m_Sut.Error("Hello");

            // Assert
            m_Logger.Received().Error("Hello");
        }

        [Test]
        public void Error_CallsLogger_ForStringAndException()
        {
            // Arrange
            var exception = new Exception();

            // Act
            m_Sut.Error("Hello",
                        exception);

            // Assert
            m_Logger.Received().Error("Hello",
                                      exception);
        }

        [Test]
        public void Fatal_CallsLogger_ForFormatAndArgs()
        {
            // Arrange
            // Act
            m_Sut.Fatal("Hello {0}",
                        "World");

            // Assert
            m_Logger.Received().FatalFormat("Hello {0}",
                                            Arg.Is <object[]>(x => ( string ) x [ 0 ] == "World"));
        }

        [Test]
        public void Fatal_CallsLogger_ForString()
        {
            // Arrange
            // Act
            m_Sut.Fatal("Hello");

            // Assert
            m_Logger.Received().Fatal("Hello");
        }

        [Test]
        public void Fatal_CallsLogger_ForStringAndException()
        {
            // Arrange
            var exception = new Exception();

            // Act
            m_Sut.Fatal("Hello",
                        exception);

            // Assert
            m_Logger.Received().Fatal("Hello",
                                      exception);
        }

        [Test]
        public void Info_CallsLogger_ForFormatAndArgs()
        {
            // Arrange
            // Act
            m_Sut.Info("Hello {0}",
                       "World");

            // Assert
            m_Logger.Received().InfoFormat("Hello {0}",
                                           Arg.Is <object[]>(x => ( string ) x [ 0 ] == "World"));
        }

        [Test]
        public void Info_CallsLogger_ForString()
        {
            // Arrange
            // Act
            m_Sut.Info("Hello");

            // Assert
            m_Logger.Received().Info("Hello");
        }

        [Test]
        public void Info_CallsLogger_ForStringAndException()
        {
            // Arrange
            var exception = new Exception();

            // Act
            m_Sut.Info("Hello",
                       exception);

            // Assert
            m_Logger.Received().Info("Hello",
                                     exception);
        }

        [SetUp]
        public void Setup()
        {
            m_Logger = Substitute.For <ILogger>();
            m_Sut = new SelkieLogger(m_Logger);
        }

        [Test]
        public void Warn_CallsLogger_ForFormatAndArgs()
        {
            // Arrange
            // Act
            m_Sut.Warn("Hello {0}",
                       "World");

            // Assert
            m_Logger.Received().WarnFormat("Hello {0}",
                                           Arg.Is <object[]>(x => ( string ) x [ 0 ] == "World"));
        }

        [Test]
        public void Warn_CallsLogger_ForString()
        {
            // Arrange
            // Act
            m_Sut.Warn("Hello");

            // Assert
            m_Logger.Received().Warn("Hello");
        }

        [Test]
        public void Warn_CallsLogger_ForStringAndException()
        {
            // Arrange
            var exception = new Exception();

            // Act
            m_Sut.Warn("Hello",
                       exception);

            // Assert
            m_Logger.Received().Warn("Hello",
                                     exception);
        }
    }
}