using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Service.Service
{
   public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
             
            services.AddScoped<ProductService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("StaffOnly", policy => policy.RequireRole("Staff"));
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             
            app.UseAuthentication();
            
        }
    }
}
