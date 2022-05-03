using Microsoft.Extensions.Logging;
using System;

namespace ApiGateway.Infrastructure.Log
{
    public class Logger : ILogger
    {
        private static object _lock = new Object();

        private readonly string _name;

        private readonly LoggerConfiguration _config;

        public Logger(string name, LoggerConfiguration config)
        {
            _name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == _config.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            lock (_lock)
            {

                if (_config.EventId == 0 || _config.EventId == eventId.Id)
                {
                    var color = Console.ForegroundColor;

                    Console.ForegroundColor = _config.Color;

                    Console.Write($"Customlogger: {logLevel} - {eventId.Id} - {_name} - {formatter(state, exception)}\n");

                    Console.ForegroundColor = color;
                }
            }
        }
    }
}
