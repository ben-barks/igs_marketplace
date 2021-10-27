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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using marketplace.Models;

namespace marketplace
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MarketContext>(opt =>
                opt.UseInMemoryDatabase("products"));

            services.AddApiVersioning(config => {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "marketplace");
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MarketContext>();
            SeedData(context);
        }

        public static void SeedData(MarketContext context)
        {
            product product1 = new product
            {
                Id = 1,
                Name = "Lavender heart",
                Price = "9.25"
            };
            product product2 = new product
            {
                Id = 2,
                Name = "Personalised cufflinks",
                Price = "45.00"
            };
            product product3 = new product
            {
                Id = 3,
                Name = "Kids T-shirt",
                Price = "19.95"
            };

            context.products.Add(product1);
            context.products.Add(product2);
            context.products.Add(product3);
            context.SaveChanges();
        }
    }
}
