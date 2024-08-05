using System;
namespace ECommerce.Persistence.DbContext;

using ECommerce.Domain.Entities.Concretes;
using Microsoft.EntityFrameworkCore;


public class ECommerceDbContext : DbContext
{
    public ECommerceDbContext() { }

    public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=ECompanyDb;User ID=SA;Password=reallyStrongPwd123;TrustServerCertificate=True");
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

}

