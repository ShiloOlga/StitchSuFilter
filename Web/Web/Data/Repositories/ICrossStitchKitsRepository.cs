using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data.Repositories
{
    public interface ICrossStitchKitsRepository
    {
        Task<IEnumerable<KitModel>> All();
        Task Clear();
        Task<KitModel> GetByItem(string item);
        Task Add(KitModel kit);
        Task AddRange(IEnumerable<KitModel> kits);
        bool IsEmpty { get; }
        Task Execute();
    }
}
