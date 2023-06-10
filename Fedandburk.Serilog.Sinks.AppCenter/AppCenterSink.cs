using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Serilog.Core;
using Serilog.Events;

namespace SByteDev.Serilog.Sinks.AppCenter
{
    /// <summary>
    /// Serilog sink that logs events to AppCenter.
    /// AppCenter should be configured before: https://docs.microsoft.com/en-us/appcenter/sdk/getting-started/xamarin
    /// If an Exception is specified, the log event will be logged using Crashes, otherwise Analytics will be used.
    /// </summary>
    public sealed class AppCenterSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public AppCenterSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null)
            {
                throw new ArgumentNullException(nameof(logEvent));
            }

            if (logEvent.Exception != null)
            {
                TrackError(logEvent);
            }
            else
            {
                TrackEvent(logEvent);
            }
        }

        private void TrackEvent(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(_formatProvider);
            var properties = GetProperties(logEvent);

            Analytics.TrackEvent(message, properties);
        }

        private void TrackError(LogEvent logEvent)
        {
            var exception = logEvent.Exception;
            var properties = GetProperties(logEvent);
            var message = logEvent.RenderMessage(_formatProvider);
            var errorAttachmentLog = ErrorAttachmentLog.AttachmentWithText(message, null);

            Crashes.TrackError(exception, properties, errorAttachmentLog);
        }

        private IDictionary<string, string> GetProperties(LogEvent logEvent)
        {
            return logEvent.Properties?.ToDictionary(
                item => item.Key,
                item => item.Value?.ToString(null, _formatProvider)
            );
        }
    }
}