using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using tmetricstatistics.Data;
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
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<ITMetricRawDataServices, TMetricRawDataServices>();

            string BaseUri = Configuration.GetSection("AppCfg").GetSection("BaseUri").Value;

            services.AddHttpClient("tmetricrawdata", c =>
            {
                c.BaseAddress = new Uri(BaseUri);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Add("User-Agent", "tmetric-statistics");
                c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration["TMetricBearerToken"]);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                await next.Invoke();

                if (context.Response.StatusCode == 404 && !context.Request.Path.Value.Contains("/api/"))
                {
                    context.Request.Path = new PathString("/index.html");
                    await next.Invoke();
                }
            });


            if (env.IsDevelopment())
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
