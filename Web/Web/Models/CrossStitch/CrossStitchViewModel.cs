using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.CrossStitch
{
    public class CrossStitchViewModel
    {
        public IEnumerable<CrossStitchPatternModel> Items { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }
        public string MethodName { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
    }
}
