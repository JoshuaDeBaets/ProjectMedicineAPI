using System.Collections.Generic;
using BL_Medicine.Managers;
using BL_Medicine.Repositories;
using DL_Medicine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            // Add Swagger/OpenAPI support for documenting your API (optional)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medicine API", Version = "v1", Description = "The MediApp API" });
            });

            string connectionString = @"Data Source=(local)\SQLEXPRESS;Initial Catalog=MobileDB;Integrated Security=True";
            services.AddControllers();
            services.AddSingleton<IUserRepository>(r => new UserRepository(connectionString));
            services.AddSingleton<UserManager>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var basePath = "/"; // Use a forward slash to specify the root path

            // Enable Swagger UI in development (optional)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/" } };
                });
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"{basePath}swagger/v1/swagger.json", "Medicine API");
                options.RoutePrefix = "swagger";
            });

            app.UseCors("CorsPolicy"); // Add CORS middleware before routing

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
