using GleamTech.AspNet.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnivisionServicesVideosApi.Context;
using UnivisionServicesVideosApi.Repository.Implementations;
using UnivisionServicesVideosApi.Repository.Interfaces;
using UnivisionServicesVideosApi.Services.Implementations;
using UnivisionServicesVideosApi.Services.Interfaces;

namespace UnivisionServicesVideosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddControllers();
            services.AddDbContext<VideosContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("VideosConnection")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UnivisionServicesVideosApi", Version = "v1" });
            });
            services.AddGleamTech();
            services.AddScoped<IVideosService, VideosService>();
            services.AddScoped<IVideosRepository, VideosRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UnivisionServicesVideosApi v1"));
            }
            app.UseGleamTech();
            app.UseRouting();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "videos")),
                RequestPath = "/StaticFiles"
            });

            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
