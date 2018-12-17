using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Web.Models;

namespace Web.Data
{
    public class CrossStitchKitsRepository : ICrossStitchKitsRepository
    {
        private readonly CrossStitchSetsDbContext _dbContext;

        public bool IsEmpty => _dbContext.Sets.CountDocuments(FilterDefinition<Kit>.Empty) == 0;

        public CrossStitchKitsRepository(CrossStitchSetsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Kit>> All()
        {
            return  await _dbContext.Sets.Find(FilterDefinition<Kit>.Empty).ToListAsync();
        }

        public Task<Kit> GetById(string id)
        {
            return _dbContext.Sets.Find(kit => kit.Id == id).FirstOrDefaultAsync();
        }
        public Task Add(Kit kit)
        {
            return _dbContext.Sets.InsertOneAsync(kit);
        }
    }
}