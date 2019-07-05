using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.CrossStitch
{
    public class WishlistKitsViewModel
    {
        public IEnumerable<WishlistKitModel> Items { get; set; }
        public IEnumerable<SelectListItem> Manufacturers { get; set; }
        public string MethodName { get; set; }
        public string Manufacturer { get; set; }
    }
}
