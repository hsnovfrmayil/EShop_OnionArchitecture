using ECommerce.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;
using ECommerce.Persistence;
using ECommerce.Application.Repositories;
using ECommerce.Persistence.Repositories;
using System;
using ECommerce.Application;
using ECommerce.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddAuthentication

builder.AddIntrastructureRegister();
builder.Services.AddPersistenceRegister();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("auth",new OpenApiSecurityScheme()
    {
        Description="JWT auth...",
        In=ParameterLocation.Header,
        Name="Authorization",
        Type=SecuritySchemeType.ApiKey
    });
});

//builder.Services.AddDbContext<ECommerceDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
//});
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddDbContext<ECommerceDbContext>(option =>
//{
//    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
//});

builder.Services.AddPersistenceRegister();
//builder.Services.AddApplicationRegister();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

