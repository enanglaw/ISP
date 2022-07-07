using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISPoliceAppApi.Data;
using ISPoliceAppApi.Extensions;
using ISPoliceAppApi.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NetTopologySuite.Geometries;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ISPoliceAppApi
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
            services.AddControllers().AddNewtonsoftJson(x =>
               x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<ISPoliceAppApiDbContext>(
              options => options.UseSqlServer(Configuration.GetConnectionString("connString"), x => x.CommandTimeout(600)));
            /*       services.AddDbContextPool<ISPoliceAppApiDbContext>(_ =>
                  {
                    _.UseSqlServer(Configuration.GetConnectionString("connString"), options => 
                    {
                        options.CommandTimeout(180); // 3 minutes
                    });
                  });
                   */
            services.AddCors(options =>
                  {
                      options.AddPolicy("CorsPolicy", policy =>
                {
                        var frontend_url = Configuration.GetValue<string>("frontend_url");
                        policy.WithOrigins(frontend_url).AllowAnyHeader().AllowAnyMethod();
                    });
                  });
            services.AddSingleton<IFileStorageService, InAppStorageService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton(provider => new AutoMapper.MapperConfiguration(config =>
            {
                var fileStorageService = provider.GetRequiredService<IFileStorageService>();
                config.AddProfile(new AutoMapperProfiles(fileStorageService));
            }).CreateMapper());
           
            /* var mappingConfig = new MapperConfiguration(mc =>
                {
                  mc.AddProfile(new AutoMapperProfiles());
                });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper); */
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IS Police App API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(pathFormat: "Logs/ISP-Log-{Date}.txt", retainedFileCountLimit: null);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ISPoliceAppApiDbContext>();
                context.Database.EnsureCreated();
                context.Database.Migrate();
            }
            // app.EnsureMigrationOfContext<ISPoliceAppApiDbContext>();

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IS Police App API v1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
