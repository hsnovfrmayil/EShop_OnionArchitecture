using System;
namespace ECommerce.Persistence.DbContext;

using System.Threading;
using System.Threading.Tasks;
using ECommerce.Domain.Entities.Abstracts;
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
        optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ECommerceDb;User Id=postgres;Password=12345");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var datas = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);

        foreach(var item in datas)
        {
            if(item.Entity is IBaseEntity entity)
            {
                if (item.State == EntityState.Added)
                    entity.CreatedAt = DateTime.UtcNow;

                entity.UpdatedAt= DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

}

