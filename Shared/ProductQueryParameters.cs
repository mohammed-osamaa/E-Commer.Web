using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductQueryParameters
    {
        private const int defaultPageSize = 5;
        private const int maxPageSize = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortings ProductSorting { get; set; }
        public string? Search { get; set; }
        public int PageIndex { get; set; } = 1;

        private int pageSize = defaultPageSize;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

    }
}
