using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Data
{
    public interface ICrossStitchKitsRepository
    {
        Task<IEnumerable<Kit>> All();
        Task Clear();
        Task<Kit> GetByItem(string item);
        Task Add(Kit kit);
        Task AddRange(IEnumerable<Kit> kits);
        bool IsEmpty { get; }
        Task Execute();
    }
}
