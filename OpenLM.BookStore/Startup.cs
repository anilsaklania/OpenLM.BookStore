using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoWrapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using OpenML.BookStore.Application;
using OpenML.BookStore.Application.Authors.ViewModel;
using OpenML.BookStore.Application.Books.ViewModel;
using OpenML.BookStore.Application.Customers.ViewModel;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Application.Publishers.ViewModel;
using OpenML.BookStore.Application.Warehouses.ViewModel;
using OpenML.BookStore.Domain.Entities;
using OpenML.BookStore.Infrastructure;
using OpenML.BookStore.Infrastructure.Data;

namespace OpenLM.BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Environment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddLogging(config =>
            {
                config.AddConfiguration(this.Configuration.GetSection("Logging"));
                config.AddDebug();
                config.AddEventSourceLogger();
            });
            services.AddControllers()
               .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
               .AddFluentValidation(fv =>
               {
                   fv.RegisterValidatorsFromAssemblyContaining<AuthorValidator>();
                   fv.RegisterValidatorsFromAssemblyContaining<BookValidator>();
                   fv.RegisterValidatorsFromAssemblyContaining<CustomerValidator>();
                   fv.RegisterValidatorsFromAssemblyContaining<PublisherValidator>();
                   fv.RegisterValidatorsFromAssemblyContaining<WareHouseValidator>();
                   fv.ImplicitlyValidateRootCollectionElements = true;
               });
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "OpenLM Book Store API",
                    Description = "OPENLM BookStore Web API",
                    TermsOfService = new Uri("https://openlm.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Anil Kumar",
                        Email = "anilsaklania@gmail.com",
                        Url = new Uri("https://openlm.com/terms"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "",
                        Url = new Uri("https://openlm.com/terms"),
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(string.Format("Logs/mylog-{0}.txt", DateTime.Now.ToShortDateString()));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenLM Book Store API V1"); });
            app.UseHttpsRedirection();
            //app.RegisterCustomExceptionHandler();
            app.UseApiResponseAndExceptionWrapper();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
