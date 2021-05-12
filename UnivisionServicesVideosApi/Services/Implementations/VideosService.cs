using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Models;
using UnivisionServicesVideosApi.Repository.Interfaces;
using UnivisionServicesVideosApi.Services.Interfaces;

namespace UnivisionServicesVideosApi.Services.Implementations
{
    public class VideosService : IVideosService
    {
        private readonly IVideosRepository _videosRepository;

        public VideosService(IVideosRepository videosRepository)
        {
            _videosRepository = videosRepository;
        }
        public async Task<Video> CreateVideo(Video video)
        {
            return await _videosRepository.CreateVideo(video);
        }

        public async Task<SearchResult> GetVideos(QueryParameters queryParameters)
        {
            return await _videosRepository.GetVideos(queryParameters);
        }
    }
}
