using CardsServer.API.Extension;
using CardsServer.DAL.Repository;
using Microsoft.Extensions.Logging;

namespace CardsServer.DAL
{
    public class DbLoggerProvider : ILoggerProvider
    {
        private readonly ILogRepository _repository;

        public DbLoggerProvider(ILogRepository repository)
        {
            _repository = repository;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(_repository);
        }

        public void Dispose()
        {
        }
    }
}
