using System;
using Core.Entities;
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
        modelBuilder.Entity<Product>().Property(x=>x.Price).HasColumnType("decimal(18,2)");
        base.OnModelCreating(modelBuilder);
    }
}
