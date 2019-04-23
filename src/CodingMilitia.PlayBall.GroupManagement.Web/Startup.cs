﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using CodingMilitia.PlayBall.GroupManagement.Business.Services;
using CodingMilitia.PlayBall.GroupManagement.Business.Imp.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CodingMilitia.PlayBall.GroupManagement.Web.IoC;
using CodingMilitia.PlayBall.GroupManagement.Web.Middlewares;
using CodingMilitia.PlayBall.GroupManagement.Web.Filters;
using CodingMilitia.PlayBall.GroupManagement.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CodingMilitia.PlayBall.GroupManagement.Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<GroupManagementDbContext>(options => {
                options.UseSqlServer(_config.GetConnectionString("GroupManagementDbContext"));
                options.EnableSensitiveDataLogging();
            });

            // Add Autofac
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            containerBuilder.Populate(services);
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);

            //uncomment if using default DI container
            //services.AddBusiness();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(".. no middlware could handle the request ..");
            });
        }
    }
}
