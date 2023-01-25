using Detailr.Application.Contracts.Persistence;
using Detailr.Domain;
using Microsoft.EntityFrameworkCore;

namespace Detailr.Persistence.Repositories
{
    public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
    {
        private readonly DetailrDbContext _dbContext;

        public BusinessRepository(DetailrDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DoesBusinessNameExist(string name)
        {
            var business = await _dbContext.Businesses
                .Where(b => b.Name == name).FirstOrDefaultAsync();

            return business != null;
        }
    }
}
