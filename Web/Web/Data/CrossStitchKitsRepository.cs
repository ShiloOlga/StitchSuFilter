using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Web.Domain;
using Web.Models;
using Kit = Web.Models.Kit;

namespace Web.Data
{
    public class CrossStitchKitsRepository : ICrossStitchKitsRepository
    {
        private readonly MariaDbContext _dbContext;

        public CrossStitchKitsRepository(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Kit>> All()
        {
            return await _dbContext.Patterns.Include(x => x.Author).Select(p => new Kit
                {
                    Item = p.Item,
                    Manufacturer = p.Author.Name,
                    KitType = KitType.DesignerPattern,
                    Title = p.Title,
                    ImageUrl = p.Image,
                    Size = new Size() {Width = p.Width, Height = p.Height},
                    Id = p.Id.ToString()
                })
                .ToArrayAsync();
        }

        public Task Clear()
        {
            return Task.CompletedTask;
        }

        public Task<Kit> GetByItem(string item)
        {
            var i = _dbContext.Patterns.First(p => p.Item == item);
            return Task.FromResult(new Kit());
        }

        public Task Add(Kit kit)
        {
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<Kit> kits)
        {
            return Task.CompletedTask;
        }

        public bool IsEmpty { get; }

        public Task Execute()
        {
            new DataSeed(_dbContext).Execute();
            return Task.CompletedTask;
        }
    }
}