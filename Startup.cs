using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //var contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            //.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test")
            //.Options;
            var contextOptions = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(configuration.GetConnectionString("MvcMovieContext"))
            .Options;
            
            //using var context = new ApplicationDbContext(contextOptions);


            Configuration = configuration;
            using (ApplicationContext db = new ApplicationContext(contextOptions)) 
             //   options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))))
            {
             
            }



        } 

        public IConfiguration Configuration { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("MvcMovieContext")));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movies}/{action=Index}/{id?}");
                //pattern: "{}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
            name: "default2",
            pattern: "{controller=Producers}/{action=Index}/{id?}");
            });
        }
    }
}
