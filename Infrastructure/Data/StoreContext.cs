using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public StoreContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Commenting below code, because what if we have multiple entities and we want to apply configuration for all entities
        //modelBuilder.Entity<Product>().Property(x=>x.Price).HasColumnType("decimal(18,2)");
        base.OnModelCreating(modelBuilder);

        //Applying custom configurations for Entities
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}
