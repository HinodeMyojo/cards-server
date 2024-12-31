using CardsServer.BLL.Entity;

namespace CardsServer.DAL.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationContext _context;

        public LogRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(LogsEntity entity, CancellationToken cancellationToken)
        {
            _context.Logs.Add(entity);
            _context.SaveChanges();
        }
    }
}
