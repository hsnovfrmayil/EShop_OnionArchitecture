using System;
using ECommerce.Application.Repositories;
using ECommerce.Persistence.DbContext;
using ECommerce.Persistence.Repositories;
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

            options.UseLazyLoadingProxies()
                   .UseSqlServer(builder.GetConnectionString("Default"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

    }
}

//Microsoft.Extentions.Configurations
//Microsoft.Extentions.Configurations.Json

//dotnet add package Microsoft.Extensions.Configuration
//dotnet add package Microsoft.Extensions.Configuration.Json
