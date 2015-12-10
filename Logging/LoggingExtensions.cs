using System;
using static Utilities.Settings.SettingsHelper;

namespace Utilities.Logging
{
    /// <summary>
    /// The main usage methods
    /// Use Log(..) for a custom experience, or straight up Warn("text"); usage
    /// 
    /// Trace => Debug => Info => Warn => Error => Fatal
    /// </summary>
    public static class LoggingExtensions
    {
        private static readonly bool IsLogging = Get(SettingsType.System, "Logging:Enabled", true);

        public static void Log(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(Logger.DefaultLogLevel, message, null, null));
        }

        public static void Log(this ILogger logger, Exception exception)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(Logger.DefaultLogLevel, exception.Message, null, exception));
        }

        public static void Log(this ILogger logger, LoggingEventType severity, Exception exception)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(severity, exception.Message, null, exception));
        }
        
        public static void Log(this ILogger logger, LoggingEventType severity, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(severity, message, null, null));
        }

        public static void Log(this ILogger logger, LoggingEventType severity, Exception exception, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(severity, message, null, exception));
        }

        public static void Trace(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(LoggingEventType.Trace, message, null, null));
        }

        public static void Debug(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(LoggingEventType.Debug, message, null, null));
        }

        public static void Info(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(LoggingEventType.Info, message, null, null));
        }

        public static void Warn(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(LoggingEventType.Warn, message, null, null));
        }

        public static void Error(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(LoggingEventType.Error, message, null, null));
        }

        public static void Fatal(this ILogger logger, string message)
        {
            if (!IsLogging) return;
            logger.Log(new LogEntry(LoggingEventType.Fatal, message, null, null));
        }
    }
}
