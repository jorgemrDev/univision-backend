using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Context;
using UnivisionServicesVideosApi.Models;
using UnivisionServicesVideosApi.Repository.Interfaces;

namespace UnivisionServicesVideosApi.Repository.Implementations
{
    public class VideosRepository : IVideosRepository
    {
        private readonly VideosContext _videosContext;
        public VideosRepository(VideosContext videosContext)
        {
            _videosContext = videosContext;
        }
        public async Task<Video> CreateVideo(Video video)
        {
            _videosContext.Video.Add(video);
            var rows = await _videosContext.SaveChangesAsync();

            if (rows > 0)
                return video;

            throw new Exception("There was an error creating video");
        }

        public async Task<SearchResult> GetVideos(QueryParameters queryParameters)
        {
            var results = await _videosContext.Video.Skip((queryParameters.PageNumber - 1)
                * queryParameters.PageSize).Take(queryParameters.PageSize).ToListAsync();

            queryParameters.TotalCount = results.Select(e => e.VideoId).Distinct().Count();
            queryParameters.TotalPages = (int)Math.Ceiling((decimal)((double)queryParameters.TotalCount / ((double)queryParameters.PageSize)));

            var result = new SearchResult()
            {
                Videos = results,
                QueryParameters = queryParameters
            };
            return result;
        }
    }
}
