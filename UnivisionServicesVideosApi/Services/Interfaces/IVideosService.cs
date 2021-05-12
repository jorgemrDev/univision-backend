using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Models;

namespace UnivisionServicesVideosApi.Services.Interfaces
{
    public interface IVideosService
    {
        Task<Video> CreateVideo(Video video);
        Task<SearchResult> GetVideos(QueryParameters queryParameters);
    }
}
