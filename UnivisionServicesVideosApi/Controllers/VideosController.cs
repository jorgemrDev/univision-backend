using GleamTech.VideoUltimate;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Models;
using UnivisionServicesVideosApi.Services.Interfaces;

namespace UnivisionServicesVideosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : Controller
    {
        private readonly IVideosService _videosService;

        public VideosController(IVideosService videosService)
        {
            _videosService = videosService;
        }

        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> Post(IFormFile file)
        {
           
            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            // Generate Random name.
            string name = Guid.NewGuid().ToString() + extension;

        
            // Basic validation on mime types and file extension
            string[] videoMimetypes = { "video/mp4", "video/webm", "video/ogg" };
            string[] videoExt = { ".mp4", ".webm", ".ogg" };

            try
            {
                if (Array.IndexOf(videoMimetypes, mimeType) >= 0 && (Array.IndexOf(videoExt, extension) >= 0))
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "videos", name);

                    using (var bits = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(bits);
                    }

                    string thumbnailName = string.Format("{0}.{1}", Guid.NewGuid().ToString(), ImageFormat.Jpeg);
                    var thumbnailPath = Path.Combine(Directory.GetCurrentDirectory(), "videos", thumbnailName);
                    using (var videoThumbnailer = new VideoThumbnailer(path))
                    using (var thumbnail = videoThumbnailer.GenerateThumbnail(1000))
                        thumbnail.Save(thumbnailPath, ImageFormat.Jpeg);


                    Video video = new Video()
                    {
                        ThumbnailPath = thumbnailName,
                        Title = file.FileName,
                        VideoPath = name
                    };
                    await _videosService.CreateVideo(video);

                    return Json(name);
                }
                throw new ArgumentException("The video did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
        }




        [EnableCors("CorsPolicy")]
        [HttpGet]
        public async Task<ActionResult<SearchResult>> GetVideos([FromQuery] QueryParameters parameters)
        {
            return await _videosService.GetVideos(parameters);
        }





    }
}
