using System;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace SByteDev.Serilog.Sinks.AppCenter
{
    public static class AppCenterLoggerConfigurationExtensions
    {
        /// <summary>
        /// Adds <c>AppCenterSink</c> to given <c>LoggerSinkConfiguration</c>.
        /// AppCenter should be configured before: https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin
        /// If an Exception is specified, the log event will be logged using Crashes, otherwise Analytics will be used.
        /// </summary>
        /// <param name="sinkConfiguration">The target sink configuration.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level for this sink.</param>
        /// <param name="levelSwitch">The log level switch for this sink.</param>
        /// <param name="formatProvider">Adds formatting to log events.</param>
        /// <returns>Existing configuration with AppCenter sink.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static LoggerConfiguration AppCenter(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            LoggingLevelSwitch levelSwitch = null,
            IFormatProvider formatProvider = null
        )
        {
            if (sinkConfiguration == null)
            {
                throw new ArgumentNullException(nameof(sinkConfiguration));
            }

            return sinkConfiguration.Sink(new AppCenterSink(formatProvider), restrictedToMinimumLevel, levelSwitch);
        }
    }
}