using System;
using Castle.Core.Configuration;
using ECommerce.Application.Repositories;
using ECommerce.Application.Services;
using ECommerce.Persistence.DbContext;
using ECommerce.Persistence.Repositories;
using ECommerce.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Persistence;

public static class RegisterServices
{
    public static void AddPersistenceRegister(this IServiceCollection services)
    {
        services.AddDbContext<ECommerceDbContext>(options =>
        {
            ConfigurationBuilder configurationBuilder = new();
            var builder= configurationBuilder.AddJsonFile("/Users/fermayilhesenov/Projects/ECommerce/Presentation/ECommerce.WebApi/appsettings.json").Build();

            //options.UseLazyLoadingProxies()
            //       .UseSqlServer(builder.GetConnectionString("Default"));
            //options.UseLazyLoadingProxies()
            //           .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        //Register all repository in persistence

        //All Read Repository
        services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
        services.AddScoped<IReadCategoryRepository, ReadCategoryRepository>();
        services.AddScoped<IReadProductRepository, ReadProductRepository>();
        services.AddScoped<IReadAppUserRepository, ReadAppUserRepository>();
        


        //All Write Repository

        services.AddScoped<IWriteOrderRepository, WriteOrderRepository>();
        services.AddScoped<IWriteCategoryRepository, WriteCategoryRepository>();
        services.AddScoped<IWriteProductRepository, WriteProductRepository>();
        services.AddScoped<IWriteAppUserRepository, WriteAppUserRepository>();


        //All Services Register
        services.AddScoped<IProductService, ProductService>();
    }

}

//Microsoft.Extentions.Configurations
//Microsoft.Extentions.Configurations.Json

//dotnet add package Microsoft.Extensions.Configuration
//dotnet add package Microsoft.Extensions.Configuration.Json
