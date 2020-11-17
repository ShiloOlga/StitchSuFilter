using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data.Repositories
{
    public interface ICrossStitchKitsRepository
    {
        Task<IEnumerable<KitModel>> AllPatterns();
        Task<IEnumerable<KitModel>> AllKits();
        Task<IEnumerable<KitModel>> AllKitPatterns();
        Task Clear();
        Task<PatternModel> GetByItem(string item);
        Task Add(KitModel kit);
        Task AddRange(IEnumerable<KitModel> kits);
        Task<IEnumerable<FabricItemModel>> GetFabricItems();
        bool IsEmpty { get; }
        Task Execute();
        Task<IEnumerable<ThreadColorReportModel>> GetColorReport();
    }
}
