using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private const string FileName = "backup.json";

        public CrossStitchKitsRepository(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<Kit>> All()
        {
            return Task.FromResult(Enumerable.Empty<Kit>());
        }

        public Task Clear()
        {
            return Task.CompletedTask;
        }

        public Task<Kit> GetByItem(string item)
        {
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
            if (System.IO.File.Exists(FileName))
            {
                var result = System.IO.File.ReadAllText(FileName);
                var kits = JsonConvert.DeserializeObject<IEnumerable<Kit>>(result);
                var manufacturers = kits
                    .Where(k => k.KitType == KitType.ManufacturerKit)
                    .Select(k => k.Manufacturer)
                    .Distinct()
                    .Select(a => new KitManufacturer { Name = a})
                    .ToArray();
                _dbContext.KitManufacturers.AddRange(manufacturers);
                _dbContext.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
    //public class CrossStitchKitsRepository : ICrossStitchKitsRepository
    //{
    //    private readonly CrossStitchSetsDbContext _dbContext;

    //    public bool IsEmpty => _dbContext.Sets.CountDocuments(FilterDefinition<Kit>.Empty) == 0;
    //    public async Task Execute()
    //    {
    //        var kits = await All();
    //        foreach (var kit in kits)
    //        {
    //            //var sizeparts = kit.Size.Split('x', 'X', '*', 'х', 'Х', '×');
    //            //var size = new Size()

    //            //{
    //            //    Width = decimal.Parse(sizeparts[0]),
    //            //    Height = decimal.Parse(sizeparts[1])
    //            //};
    //            //var update = new BsonDocument("$set", new BsonDocument("Size", size.ToBsonDocument()));
    //            //_dbContext.Sets.UpdateOne(k => k.Id == kit.Id, update);

    //        }
    //    }

    //    public CrossStitchKitsRepository(CrossStitchSetsDbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public async Task<IEnumerable<Kit>> All()
    //    {
    //        return  await _dbContext.Sets.Find(FilterDefinition<Kit>.Empty).ToListAsync();
    //    }

    //    public Task Clear()
    //    {
    //        return _dbContext.Sets.DeleteManyAsync(FilterDefinition<Kit>.Empty);
    //    }

    //    public Task<Kit> GetByItem(string item)
    //    {
    //        return _dbContext.Sets.Find(kit => kit.Item == item).FirstOrDefaultAsync();
    //    }

    //    public Task Add(Kit kit)
    //    {
    //        return _dbContext.Sets.InsertOneAsync(kit);
    //    }

    //    public Task AddRange(IEnumerable<Kit> kits)
    //    {
    //        return _dbContext.Sets.InsertManyAsync(kits);
    //    }
    //}
}