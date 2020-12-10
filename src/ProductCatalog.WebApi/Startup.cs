using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductCatalog.WebApi.Data;
using ProductCatalog.WebApi.Mapping;
using ProductCatalog.WebApi.Services.Concretes;
using ProductCatalog.WebApi.Services.Interfaces;
using System;

namespace ProductCatalog.WebApi
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

            services.AddDbContext<ProductCatalogDbContext>(options =>
               options.UseMySQL(
                   Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ProductCatalogDbContext).Assembly.FullName)));

            services.AddScoped<IProductCatalogDbContext>(provider => provider.GetService<ProductCatalogDbContext>());


            services.AddSwaggerGen();

            services.AddAutoMapper(new Type[] { typeof(OrganizationProfile) });

            services.AddSingleton<IExcelService, ExcelService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
