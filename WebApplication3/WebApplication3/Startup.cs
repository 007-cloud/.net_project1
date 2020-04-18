using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication3.Data;

namespace WebApplication3
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddDbContext<MoviesContext>(options =>
            {
                options.UseSqlite("Filename=movies.db");
            });
            services.AddMvc();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "messages",
                    template: "say/{*message}",
                    defaults: new { controller = "Messages", action = "ShowMessage" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Hello}/{action=Index}/{id?}");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "calculator",
                    template: "Calculator/{action}/{number:int}",
                    defaults: new { Controller = "Calculator" });

                routes.MapRoute(
                    name: "messages",
                    template: "say/{**message}",
                    defaults: new { controller = "Messages", action = "ShowMessage" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Hello}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "movie",
                    template: "Movies/{action}/{number:int}",
                    defaults: new { Controller = "Movies" });
            });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World! Controller page = default");
                });
            });
        }
    }
}
