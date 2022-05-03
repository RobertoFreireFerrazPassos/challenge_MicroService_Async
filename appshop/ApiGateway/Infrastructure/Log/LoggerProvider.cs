using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ApiGateway.Infrastructure.Log
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly LoggerConfiguration _config;

        private readonly ConcurrentDictionary<string,Logger> _loggers = new ConcurrentDictionary<string,Logger>();

        public LoggerProvider (LoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new Logger(name, _config));
        }
        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
