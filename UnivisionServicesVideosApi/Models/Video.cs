using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnivisionServicesVideosApi.Models
{
    public class Video
    {
        public Guid VideoId { get; set; }
        public string Title { get; set; }
        public string VideoPath { get; set; }
        public string ThumbnailPath { get; set; }
    }
}

