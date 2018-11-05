﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarService.Domain.Database;
using CarService.Domain.Repositories;
using CarService.Web.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CarService.Web
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
            var connectionString = Configuration["ConnectionStrings:localDb"];
            services.AddDbContext<CarServiceDbContext>(options => options.UseSqlServer(connectionString));

            services.AddMvc();

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddSingleton<IEntrySessionService, EntrySessionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            DbInitializer.Seed(app);
            app.UseMvc();
        }
    }
}