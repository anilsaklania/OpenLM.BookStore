using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenML.BookStore.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("BookStoreDB")));


            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddTransient<IEmailSender, NotificationService>();
            //services.AddTransient<IDocuSignService, DocuSignService>();
            //services.AddTransient<IRegisterHangfireService, RegisterHangfireService>();
            //services.AddTransient<IClearSearchService, ClearSearchService>();
            //services.AddTransient<ISpreadSheetService, SpreadSheetService>();
            //services.AddTransient<ILdapService, LdapService>();
            //services.AddTransient<IWordDocumentUtility, WordDocumentUtility>();

            //services.AddTransient<IAdo, AdoRepository>();

            return services;
        }
    }
}
