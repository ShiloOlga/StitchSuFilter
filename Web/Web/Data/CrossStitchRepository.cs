using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.CrossStitch;
using Web.Utils.CrossStitch;

namespace Web.Data
{
    public interface ICrossStitchRepository
    {
        Task<IEnumerable<CrossStitchPatternModel>> GetWishlist();
        Task<IEnumerable<CrossStitchPatternModel>> GetUpdates();
    }

    public class CrossStitchRepository : ICrossStitchRepository
    {
        private Task<CrossStitchPageContent> _wishListTask;
        private Task<CrossStitchPageContent> _lastUpdatesTask;
        private IEnumerable<CrossStitchPatternModel> _wishlistItems;
        private IEnumerable<CrossStitchPatternModel> _updateItems;

        public CrossStitchRepository()
        {
            LoadWishList();
            LoadLastUpdates();
        }

        private async void LoadWishList()
        {
            var wishlistDownloader = new WishlistDownloader();
            _wishListTask = wishlistDownloader.Parse();
            await _wishListTask;
            _wishlistItems = _wishListTask.Result.Patterns.ToArray();
        }

        private async void LoadLastUpdates()
        {
            var updatesDownloader = new UpdatesDownloader();
            _lastUpdatesTask = updatesDownloader.Parse(null);
            await _lastUpdatesTask;
            _updateItems = _lastUpdatesTask.Result.Patterns.ToArray();
        }

        public async Task<IEnumerable<CrossStitchPatternModel>> GetWishlist()
        {
            await _wishListTask;
            return _wishlistItems;
        }

        public async Task<IEnumerable<CrossStitchPatternModel>> GetUpdates()
        {
            await _lastUpdatesTask;
            return _updateItems;
        }
    }
}
