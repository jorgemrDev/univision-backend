using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Models;
//dotnet ef migrations add Initial --project UnivisionServicesVideosApi
//dotnet ef database update --project UnivisionServicesVideosApi

namespace UnivisionServicesVideosApi.Context
{
    public class VideosContext : DbContext
    {
        public VideosContext(DbContextOptions<VideosContext> options) : base(options)
        {

        }

        public DbSet<Video> Video { get; set; }
    
    }

    //public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<VideosContext>
    //{
    //    public VideosContext CreateDbContext(string[] args)
    //    {
    //        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/appsettings.json").Build();
    //        var builder = new DbContextOptionsBuilder<VideosContext>();
    //        var connectionString = configuration.GetConnectionString("VideosConnection");
    //        builder.UseSqlServer(connectionString);
    //        return new VideosContext(builder.Options);
    //    }
    //}
}