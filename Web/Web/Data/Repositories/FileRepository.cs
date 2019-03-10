using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data.Context;
using Web.Models;

namespace Web.Data.Repositories
{
    public class FileRepository : ICrossStitchKitsRepository
    {
        public bool IsEmpty => false;

        public Task Add(KitModel kit)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<KitModel> kits)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<KitModel>> All()
        {
            return Task.FromResult(Enumerable.Empty<KitModel>());
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

        public Task<IEnumerable<FabricItemModel>> GetFabricItems()
        {
            throw new NotImplementedException();
        }
    }
}
