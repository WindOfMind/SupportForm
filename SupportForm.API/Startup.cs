using System;
using System.IO;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SupportForm.API.Middleware;
using SupportForm.API.Models.SwaggerExamples;
using SupportForm.Domain;
using SupportForm.Infrastructure;
using Swashbuckle.AspNetCore.Filters;

namespace SupportForm.API
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
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddSingleton<ISupportMessageRepository, SupportMessageRepository>();

            services.AddCors(options =>
                options.AddPolicy("LocalDevCorsPolicy", builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .SetIsOriginAllowed((host) => true))
                );

            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<RequestLoggingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("LocalDevCorsPolicy");
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Support API V1");
                c.RoutePrefix = "api/documentation";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Support API",
                        Version = "v1",
                        Description = "HTTP service for support messages"
                    });

                var dir = new DirectoryInfo(AppContext.BaseDirectory);
                foreach (var fi in dir.EnumerateFiles("*.xml"))
                {
                    options.IncludeXmlComments(fi.FullName);
                }

                options.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblyOf<SupportMessageRequestExample>();
        }
    }
}
