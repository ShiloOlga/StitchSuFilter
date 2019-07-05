using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.CrossStitch;
using Web.Utils.CrossStitch;

namespace Web.Data.Repositories
{
    public interface ICrossStitchRepository
    {
        Task<IEnumerable<CrossStitchPatternModel>> GetWishlistPatterns();
        Task<IEnumerable<WishlistKitModel>> GetWishlistKits();
        Task<IEnumerable<CrossStitchPatternModel>> GetUpdates(string lastSavedId);
    }

    public class CrossStitchRepository : ICrossStitchRepository
    {
        private Task<CrossStitchPageContent> _wishListTask;
        private Task<CrossStitchPageContent> _lastUpdatesTask;
        private IEnumerable<CrossStitchPatternModel> _wishlistPatterns;
        private List<CrossStitchPatternModel> _updateItems;
        private IEnumerable<WishlistKitModel> _wishlistKits;

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
            _wishlistPatterns = _wishListTask.Result.Patterns.ToArray();
            _wishlistKits = _wishListTask.Result.Kits.ToArray();
        }

        private async void LoadLastUpdates()
        {
            var updatesDownloader = new UpdatesDownloader();
            _lastUpdatesTask = updatesDownloader.Parse(null);
            await _lastUpdatesTask;
            _updateItems = _lastUpdatesTask.Result.Patterns.ToList();
        }

        public async Task<IEnumerable<CrossStitchPatternModel>> GetWishlistPatterns()
        {
            await _wishListTask;
            return _wishlistPatterns;
        }

        public async Task<IEnumerable<WishlistKitModel>> GetWishlistKits()
        {
            await _wishListTask;
            return _wishlistKits;
        }

        public async Task<IEnumerable<CrossStitchPatternModel>> GetUpdates(string lastSavedId)
        {
            await _lastUpdatesTask;
            var hasSavedId = int.TryParse(lastSavedId, out var lastId);
            var pageId = 1;
            while (hasSavedId && !_updateItems.Any(i => i.PatternId.Id == lastId))
            {
                var updatesDownloader = new UpdatesDownloader();
                var pageContent = await updatesDownloader.Parse(++pageId);
                _updateItems.AddRange(pageContent.Patterns);
            }
            return _updateItems;
        }
    }
}
