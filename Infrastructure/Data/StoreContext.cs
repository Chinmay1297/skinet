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
}
