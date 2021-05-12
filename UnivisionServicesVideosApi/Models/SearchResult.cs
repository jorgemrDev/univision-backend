using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnivisionServicesVideosApi.Models
{
    public class SearchResult
    {
        public List<Video> Videos { get; set; }
        public QueryParameters QueryParameters { get; set; }
    }
}
