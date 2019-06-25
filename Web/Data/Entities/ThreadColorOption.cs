using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class ThreadColorOption
    {
        private readonly ILazyLoader _lazyLoader;
        private Pattern _pattern;
        private ThreadColor _threadColor;

        public ThreadColorOption(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int PatternId { get; set; }
        public int ThreadColorId { get; set; }
        public decimal? RequiredLength { get; set; }

        public Pattern Pattern
        {
            get => _lazyLoader.Load(this, ref _pattern);
            set => _pattern = value;
        }

        public ThreadColor ThreadColor
        {
            get => _lazyLoader.Load(this, ref _threadColor);
            set => _threadColor = value;
        }
    }

}
