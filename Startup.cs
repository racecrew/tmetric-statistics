using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tmetricstatistics.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Threading.Tasks;

namespace tmetricstatistics
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration _cfg { get; }
        public Startup(IWebHostEnvironment env, IConfiguration cfg)
        {
            _env = env;
            _cfg = cfg;
            var builder = new ConfigurationBuilder();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddSingleton<ITMetricRawDataServices, TMetricRawDataServices>();

            string BaseUri = _cfg["TMetric:BaseUri"];

            services.AddHttpClient("tmetricrawdata", c =>
            {
                c.BaseAddress = new Uri(BaseUri);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Add("User-Agent", "tmetric-statistics");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _cfg["TMetric:BearerToken"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                await next.Invoke();

                if (context.Response.StatusCode == 404 && !context.Request.Path.Value.Contains("/api/"))
                {
                    context.Request.Path = new PathString("/index.html");
                    await next.Invoke();
                }
            });


            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
