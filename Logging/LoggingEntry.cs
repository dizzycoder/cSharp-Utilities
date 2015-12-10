using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Logging
{
    public class LogEntry
    {
        public readonly LoggingEventType Severity;
        public readonly string Message;
        public readonly Type Source;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType severity, string message, Type source, Exception exception)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentException(nameof(message));
            
            if (severity < LoggingEventType.Trace || severity > LoggingEventType.Fatal)
                throw new ArgumentOutOfRangeException(nameof(severity));

            this.Severity = severity;
            this.Message = message;
            this.Source = source;
            this.Exception = exception;
        }
    }
}
