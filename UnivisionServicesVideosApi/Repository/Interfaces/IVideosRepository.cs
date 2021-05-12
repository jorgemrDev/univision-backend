using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Models;

namespace UnivisionServicesVideosApi.Repository.Interfaces
{

    public interface IVideosRepository
    {
        /// <summary>
        /// Create a Video on univision database
        /// </summary>
        /// <param name="video">video Object</param>
        /// <returns>Created Entity</returns>
        Task<Video> CreateVideo(Video video);


        Task<SearchResult> GetVideos(QueryParameters queryParameters);
    }
}

