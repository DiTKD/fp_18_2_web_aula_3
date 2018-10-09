using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fp_stack.core.Models;
using fp_stack.core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace fp_stack.api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services = ConfigureServicesInject(services);

            var connection = @"Server=(localdb)\mssqllocaldb;Database=StackDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));


            services.AddCors(x =>
            {
                x.AddPolicy("Default", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            services.AddMvc(
                options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvcWithDefaultRoute();
            app.UseCors("Default");
        }

        private IServiceCollection ConfigureServicesInject(IServiceCollection services)
        {
            services.AddTransient<NotciaService>();

            return services;
        }
    }
}
