using DesignPatternAsp.Configuration;
using DesignPatterns.Repository;
using DessignPatterns.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools.Earn;

namespace DesignPatternAsp
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
            services.AddControllersWithViews();

            //INYECCION DE DEPENDENCIAS - Se inyecta el objeto MyConfig obtenido de la configuracion para ser utilizado e cualquier controlador
            services.Configure<MyConfig>(Configuration.GetSection("MyConfig"));
            
            services.AddTransient((factory) =>
            {
                return new LocalEarnFactory(Configuration
                    .GetSection("MyConfig").GetValue<decimal>("LocalPercentage"));
            });

            services.AddTransient((factory) =>
            {
                return new ForeignEarnFactory(Configuration
                    .GetSection("MyConfig").GetValue<decimal>("ForeignPercentage"), 
                    Configuration.GetSection("MyConfig")
                    .GetValue<decimal>("Extra"));
            });
            //Inyecto el contexto
            services.AddDbContext<DesignPatternsContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Connection"));
            });

            //Inyecto la clase Repository para poder utilizarla en todos los controladores
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
