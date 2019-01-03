using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web.Data.Context;
using Web.Models;

namespace Web.Data.Repositories
{
    public class CrossStitchKitsRepository : ICrossStitchKitsRepository
    {
        private readonly MariaDbContext _dbContext;
        private readonly IMapper _mapper;

        public CrossStitchKitsRepository(MariaDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KitModel>> All()
        {
            return await _dbContext.Patterns.Include(x => x.Author).Select(p => new KitModel
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

        public async Task<IEnumerable<ThreadColorReportModel>> GetColorReport()
        {
            var patternGroups = _dbContext.ThreadColorOptions.GroupBy(g => g.PatternId);

            return await _dbContext.ThreadColorOptions.Where(o => o.ThreadColor.Manufacturer.Id == 3)
                .GroupBy(p => p.ThreadColor)
                .Select(p => new ThreadColorReportModel
                {
                    Color = p.Key,
                    TotalLength = p.Sum(x => x.RequiredLength ?? 0)
                })
                .ToArrayAsync();
        }

        public Task Clear()
        {
            return Task.CompletedTask;
        }

        public Task<PatternModel> GetByItem(string item)
        {
            var dto = _dbContext.Patterns.First(p => p.Item == item);
            return Task.FromResult(_mapper.Map<PatternModel>(dto));
        }

        public Task Add(KitModel kit)
        {
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<KitModel> kits)
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