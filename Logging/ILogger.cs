using System;

namespace Utilities.Logging
{
    public interface ILogger
    {
        void Log(LogEntry entry);
        string LoggerName { get; }
    }
}