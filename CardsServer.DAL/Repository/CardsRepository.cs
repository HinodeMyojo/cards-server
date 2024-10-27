using CardsServer.BLL.Abstractions;
using CardsServer.BLL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CardsServer.DAL.Repository
{
    public class CardsRepository : ICardsRepository
    {
        private readonly ApplicationContext _context;

        public CardsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ElementStatisticEntity> CreateElementStatistic(UserEntity user, ElementEntity element, CancellationToken cancellationToken)
        {
            ElementStatisticEntity result = new()
            {
                Element = element,
                User = user,
            };

            await _context.ElementStatistics.AddAsync(result);
            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task EditElementStatistic(ElementStatisticEntity elementStatistic, CancellationToken cancellationToken)
        {
            _context.ElementStatistics.Update(elementStatistic);
            await _context.SaveChangesAsync();
        }

        public async Task<ElementStatisticEntity?> GetElementStatistic(
            Expression<Func<ElementStatisticEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.ElementStatistics.FirstOrDefaultAsync(predicate, cancellationToken);
        }
    }
}
