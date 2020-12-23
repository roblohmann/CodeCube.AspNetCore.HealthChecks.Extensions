using System;
using CodeCube.AspNetCore.HealthChecks.Extensions.Uris.Versioning.DependencyInjection;
using CodeCube.AspNetCore.HealthChecks.Extensions.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Samples.NETCore31
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
            services.AddHealthChecks()
                .AddUrlGroupWithVersioning(options =>
                {
                    options.AddUri(new Uri("https://www.google.com"), setup =>
                    {
                        setup.WithVersioningHeader("server");
                    });

                })
                .AddApplicationInsightsPublisher();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseHealthChecksWithVersioning("/healthz", "Samples.NETCore31", true);
            //app.UseHealthChecks("/healthz");
        }
    }
}
