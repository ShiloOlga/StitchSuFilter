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

        public Task Clear()
        {
            return _dbContext.Sets.DeleteManyAsync(FilterDefinition<Kit>.Empty);
        }

        public Task<Kit> GetByItem(string item)
        {
            return _dbContext.Sets.Find(kit => kit.Item == item).FirstOrDefaultAsync();
        }

        public Task Add(Kit kit)
        {
            return _dbContext.Sets.InsertOneAsync(kit);
        }

        public Task AddRange(IEnumerable<Kit> kits)
        {
            return _dbContext.Sets.InsertManyAsync(kits);
        }
    }
}