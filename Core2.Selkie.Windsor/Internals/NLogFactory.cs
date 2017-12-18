// Copyright 2004-2012 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// (source code was slightly modified to adjust to coding guidelines)

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Castle.Core.Logging;
using JetBrains.Annotations;
using NLog;
using NLog.Config;
using ILogger = Castle.Core.Logging.ILogger;

namespace Core2.Selkie.Windsor.Internals
{
    /// <summary>
    ///     Implementation of <see cref="ILoggerFactory" /> for NLog.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class NLogFactory : AbstractLoggerFactory
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NLogFactory" /> class.
        /// </summary>
        public NLogFactory()
            : this(DefaultConfigFileName)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NLogFactory" /> class.
        /// </summary>
        /// <param name="configuredExternally">
        ///     If <c>true</c>. Skips the initialization of log4net assuming it will happen
        ///     externally. Useful if you're using another framework that wants to take over configuration of NLog.
        /// </param>
        [UsedImplicitly]
        public NLogFactory(bool configuredExternally)
        {
            if ( configuredExternally )
            {
                return;
            }

            FileInfo file = GetConfigFile(DefaultConfigFileName);
            LogManager.Configuration = new XmlLoggingConfiguration(file.FullName);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NLogFactory" /> class.
        /// </summary>
        /// <param name="configFile"> The config file. </param>
        [UsedImplicitly]
        public NLogFactory(string configFile)
        {
            FileInfo file = GetConfigFile(configFile);
            LogManager.Configuration = new XmlLoggingConfiguration(file.FullName);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NLogFactory" /> class.
        /// </summary>
        /// <param name="loggingConfiguration"> The NLog Configuration </param>
        public NLogFactory(LoggingConfiguration loggingConfiguration)
        {
            LogManager.Configuration = loggingConfiguration;
        }

        private const string DefaultConfigFileName = "nlog.config";

        /// <summary>
        ///     Creates a logger with specified <paramref name="name" />.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <returns> </returns>
        public override ILogger Create(string name)
        {
            Logger log = LogManager.GetLogger(name);
            return new NLogLogger(log,
                                  this);
        }

        /// <summary>
        ///     Not implemented, NLog logger levels cannot be set at runtime.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <param name="level"> The level. </param>
        /// <returns> </returns>
        /// <exception cref="NotImplementedException" />
        public override ILogger Create(string name,
                                       LoggerLevel level)
        {
            throw new
                NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}