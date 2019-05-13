using System.Collections.Generic;

namespace Web.Models.ViewModels
{
    public class KitSummaryViewModel
    {
        public IEnumerable<KitModel> KitItems { get; set; }
        public PagingModel PagingInfo { get; set; }
    }
}
