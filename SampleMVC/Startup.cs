using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleMVC.Services;

namespace SampleMVC
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
            services.AddTransient<IMyCustomService, MyCustomService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder appBuilder, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                appBuilder.UseDeveloperExceptionPage();
            }

            //default: appBuilder.UseMvc();
            
            appBuilder.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(name: "ProductCategories",
                    template: "{controller}/{action}/{category}",
                    defaults: new
                    {
                        controller = "Ikea",
                        action = "Index",
                        category = "Furnitures"
                    },
                    constraints: new { id = @"^\d+$", category = @"(Furnitures|Lights|Meatballs)" }
                );
                
            });

            appBuilder.UseMyMiddleware();
        }
    }


    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Write to Response
            await _next.Invoke(context);
            // Clean up.
        }
    }

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseMiddleware<MyMiddleware>();
        }
    }
}
