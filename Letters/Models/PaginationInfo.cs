using System;
using System.Collections.Generic;

namespace Letters.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                if (TotalItems % PageSize == 0)
                {
                    return TotalItems / PageSize;
                }

                return TotalItems / PageSize + 1;
            }
        }
    }

    public class IndexViewModel
    {
        public IEnumerable<Letter> Letters { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
