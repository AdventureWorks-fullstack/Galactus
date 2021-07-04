using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AdventureWorks.Domain;
using Microsoft.EntityFrameworkCore;
using Galactus.Schema;
using Microsoft.Extensions.Configuration;
using Galactus.Schema.Mutations;

namespace Galactus
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        readonly IConfiguration _config;
        readonly string _corsPolicy = "YOLO";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(_corsPolicy, builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<AdventureWorksContext>(options => options.UseSqlServer(_config.GetConnectionString("AdventureWorksDb")));

            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
            }

            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseCors(_corsPolicy);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL("/");
            });
        }
    }
}
