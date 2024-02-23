using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Product.Domain.Mappers;
using Product.Infra.CrossCutting.IOC;
using Product.Infra.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Product.Api
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
            var connectionString = Configuration.GetConnectionString("SqlServer");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddAutoMapper(typeof(ProductMapper));

            // Add services to the container.
            services.AddControllers();

            services.AddSwaggerGen(opt =>
            {
                opt.CustomSchemaIds(t => t.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault()?.DisplayName ?? t.Name);

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"ProductManager API",
                    Description = $"Product Manager é uma api de gestão de produtos",
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo Torres",
                        Email = "gustavo.tgn@gmail.com",
                    },
                });

                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => opt.IncludeXmlComments(xmlFile));
            });

            services.AddRepositories().AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.ApplyMigration();
        }
    }
}