using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Web.Models.CrossStitch
{
    public class CrossStitchViewModel
    {
        public IEnumerable<CrossStitchPatternModel> Items { get; set; }
        public SelectList Authors { get; set; }
        public SelectList Statuses { get; set; }
        public string MethodName { get; set; }
    }
}
