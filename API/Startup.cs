  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.UnitOfMeasureContracts;
using Data;
using EngineeringUnitsCore.Converter;
using EngineeringUnitscore.Wrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace UomSystem
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
            
            //sqlite connection for testing
            //services.AddDbContext<RepositoryContext>(options => options.UseSqlite("Filename=units.db")); //../Data/
            var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
            var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            var connectionUrl = $"host={host};port={port};database={db};username={user};password={pass};";
            Console.WriteLine(connectionUrl);
            //postgres connection with connection string defined in appsettings.json 
            services.AddDbContext<RepositoryContext>(options =>
                options.UseNpgsql(connectionUrl));
            services.AddControllers();
            services.AddScoped<IEngineeringUnitsWrapper, EngineeringUnitsWrapper>();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}