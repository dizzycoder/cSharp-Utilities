using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using Utilities.Settings;

namespace Utilities.Logging
{
    /// <summary>
    /// Logger wrapper, usage:
    ///     private static readonly ILogger Logger = new Logger(typeof(SettingsHelper));
    ///     Logger.Trace("message");
    /// </summary>
    public class Logger : ILogger
    {
        public static LoggingEventType DefaultLogLevel;

        private NLog.ILogger _logger;
        private Type _callerType;
        private readonly string _loggerName;

        /// <summary>
        /// Bootstrap
        /// </summary>
        static Logger()
        {
            DefaultLogLevel = SettingsHelper.Get(SettingsHelper.SettingsType.System, "Logging:DefaultLevel", LoggingEventType.Info);
        }

        /// <summary>
        /// Preferred type as logger name, pass the type of the caller
        /// </summary>
        /// <param name="type"></param>
        public Logger(Type type = null)
        {
            this._callerType = type;
        }

        /// <summary>
        /// Manual string override for the name of the logger instead of the name of the calling type
        /// </summary>
        /// <param name="loggerName"></param>
        public Logger(string loggerName)
        {
            _loggerName = loggerName;
        }

        /// <summary>
        /// Don't use this silly, use the extension methods for Logger.Log(string message) etc
        /// This method is the central wire-up for the logger package you're using; here we use NLog
        /// You could change JUST this method to use Log4NET or some other
        /// </summary>
        /// <param name="entry"></param>
        public void Log(LogEntry entry)
        {
            if (_logger == null)
            {                
                _logger = LogManager.GetLogger(LoggerName); 
            }

            var logEvent = new LogEventInfo()
            {
                LoggerName = LoggerName,
                Message = entry.Message,
                Exception = entry.Exception,
            };
            
            // for use with ${event-context:Something}
            //logEvent.Properties["Something"] = "stuff";

            switch (entry.Severity)
            {
                case LoggingEventType.Trace: logEvent.Level = LogLevel.Trace; break;
                case LoggingEventType.Debug: logEvent.Level = LogLevel.Debug; break;
                case LoggingEventType.Info:  logEvent.Level = LogLevel.Info;  break;
                case LoggingEventType.Warn:  logEvent.Level = LogLevel.Warn;  break;
                case LoggingEventType.Error: logEvent.Level = LogLevel.Error; break;
                case LoggingEventType.Fatal: logEvent.Level = LogLevel.Fatal; break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Debug.Print("Logging a {0} event with message {1}", logEvent.Level, logEvent.Message);
            _logger.Log(logEvent, typeof(Logger), typeof(LoggingExtensions));
        }

        /// <summary>
        /// The Logger instance that is used, set by constructor only
        /// </summary>
        public string LoggerName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_loggerName)) return _loggerName; // override
                
                if (_callerType != null) return _callerType.FullName;

                // use reflection to get the caller type if not passed
                var frame = new StackFrame(3); // b/c of the extension method wrapping
                var method = frame.GetMethod();
                _callerType = method.DeclaringType;

                return _callerType != null ? _callerType.FullName : "UNKNOWN";
            }
        }
    }
}
