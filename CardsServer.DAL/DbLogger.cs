
using CardsServer.BLL.Entity;
using CardsServer.DAL.Repository;
using Microsoft.Extensions.Logging;

namespace CardsServer.API.Extension
{
    public class DbLogger : ILogger, IDisposable
    {
        private ILogRepository _repository;
        static object _lock = new object();

        public DbLogger(ILogRepository repository)
        {
            _repository = repository;            
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return this;
        }

        public void Dispose(){ }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (logLevel == LogLevel.Error || logLevel == LogLevel.Critical)
            {

                var logEntity = new LogsEntity
                {
                    CreateAt = DateTime.UtcNow,
                    State = formatter(state, exception),
                    Exception = exception?.ToString() ?? string.Empty
                };

                _repository.Add(logEntity, default);
            }
        } 
    }
}
