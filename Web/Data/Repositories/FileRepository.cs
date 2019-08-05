using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.Data.Context;
using Web.Models;
using Web.Models.V2;
using FabricModel = Web.Models.V2.FabricModel;
using KitModel = Web.Models.KitModel;
using PatternModel = Web.Models.PatternModel;

namespace Web.Data.Repositories
{
    public class FileRepository : ICrossStitchKitsRepository, ICrossStitchPatternsRepository
    {
        private class Temp
        {
            public ICollection<Models.V2.KitModel> Kits { get; set; }
            public ICollection<string> KitManufacturers { get; set; }
            public ICollection<string> PatternAuthors { get; set; }
            public ICollection<Models.V2.PatternModel> Patterns { get; set; }
            public ICollection<string> FabricTypes { get; set; }
            public ICollection<string> FabricContentTypes { get; set; }
            public ICollection<string> ThreadManufacturers { get; set; }
            public ICollection<FabricModel> Fabrics { get; set; }
            public ICollection<ThreadColorModel> Threads { get; set; }
        }

        public ICollection<Models.V2.KitModel> Kits { get; }
        public ICollection<string> KitManufacturers { get; }
        public ICollection<string> PatternAuthors { get; }
        public ICollection<Models.V2.PatternModel> Patterns { get; }
        public ICollection<string> FabricTypes { get; }
        public ICollection<string> FabricContentTypes { get; }
        public ICollection<string> ThreadManufacturers { get; }
        public ICollection<FabricModel> Fabrics { get; }
        public ICollection<ThreadColorModel> Threads { get; }
        [JsonIgnore]
        public bool IsEmpty => false;
        private IEnumerable<KitModel> _patterns;
        private IEnumerable<KitModel> _kits;
        private readonly Random _random = new Random();

        public FileRepository()
        {
            var text = File.ReadAllText("Data.json");
            var data = JsonConvert.DeserializeObject<Temp>(text);
            Kits = data.Kits;
            KitManufacturers = data.KitManufacturers;
            PatternAuthors = data.PatternAuthors;
            Patterns = data.Patterns;
            FabricTypes = data.FabricTypes;
            FabricContentTypes = data.FabricContentTypes;
            ThreadManufacturers = data.ThreadManufacturers;
            Fabrics = data.Fabrics;
            Threads = data.Threads;
        }

        public Task Add(KitModel kit)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<KitModel> kits)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KitModel>> AllPatterns()
        {
            if (_patterns == null)
            {
                _patterns = Patterns
                    .Select(kit => new KitModel
                    {
                        Title = kit.Title,
                        Manufacturer = kit.Author,
                        Item = kit.Item,
                        Size = kit.Size,
                        ImageUrl = kit.ImageUrl
                    })
                    .OrderBy(x => _random.Next())
                    .ToArray();
            }
            return Task.FromResult(_patterns);
        }

        public Task<IEnumerable<KitModel>> AllKits()
        {
            if (_kits == null)
            {
                _kits = Kits
                    .Select(kit => new KitModel
                    {
                        Title = kit.Title,
                        Manufacturer = kit.Manufacturer,
                        Item = kit.Item,
                        Size = kit.Size,
                        ImageUrl = kit.ImageUrl
                    })
                    .OrderBy(x => _random.Next())
                    .ToArray();
            }

            return Task.FromResult(_kits);
        }

        public Task Clear()
        {
            throw new NotImplementedException();
        }

        public Task Execute()
        {
            new FileDataSeed().Execute();
            return Task.CompletedTask;
        }

        public Task<PatternModel> GetByItem(string item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ThreadColorReportModel>> GetColorReport()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.FabricItemModel>> GetFabricItems()
        {
            throw new NotImplementedException();
        }
    }
}
