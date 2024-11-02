using CardsServer.BLL.Abstractions;

namespace CardsServer.DAL.Repository
{
    public class LearningRepository : ILearningRepository
    {
        private ApplicationContext _context;

        public LearningRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}
