using System;

namespace Web.Models
{
    public class PagingModel
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalCount { get; set; }
        public int PageSize { get; set; } = 25;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(TotalCount, PageSize));

        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
    }
}
