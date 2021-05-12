using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnivisionServicesVideosApi.Models
{
    public class QueryParameters
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public string SearchKeyWord { get; set; }
        
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
