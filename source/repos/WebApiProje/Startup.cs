using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApiProje.DataAccess;
using WebApiProje.Formatters;
using WebApiProje.CustomMiddlewares;

namespace WebApiProje
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
            services.AddScoped<IProductDal,EfProductDal>(); //IproductDal constractor da lazım olursa ona efproductdal new le ve o nesneyi ver
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(options=> {
                options.OutputFormatters.Add(new VCardOutputFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //
        [Obsolete]
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)//, ILoggerFactory loggerFactory)
        {
            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();*/

            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            app.UseMiddleware<AuthenticationMiddleware>();
            
            //otomatik olarak sayfa yönlendirmelerini tabır localhost/api/products/get 
            //dendiğinde product controller ı içerisindeki GET metodunu görür
            app.UseMvc(config=> {
                //config.MapRoute("DefaultRoute", "api/{controller}/{action}");
            });
        }
    }
}
