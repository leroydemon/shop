
using Microsoft.Extensions.Logging;

namespace Infrastucture.Logger
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath;
        private static readonly object _lock = new object();

        public FileLogger(string filePath)
        {
            _filePath = filePath;

            var logDirectory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

        public void Log<TState>(LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var logRecord = string.Format("{0}: [{1}] {2} {3}", DateTime.Now, logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

            lock (_lock)
            {
                File.AppendAllText(_filePath, logRecord + Environment.NewLine);
            }
        }
    }
}