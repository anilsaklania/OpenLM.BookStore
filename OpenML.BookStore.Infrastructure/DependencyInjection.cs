using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenML.BookStore.Application.Interfaces;
using OpenML.BookStore.Infrastructure.Data;
using OpenML.BookStore.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenML.BookStore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("BookStoreDB")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
