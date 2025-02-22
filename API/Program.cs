using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductsRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

#region Seed Data
try
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<StoreContext>();

    //apply pending migrations/ create db if doesnt exist
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await context.SaveChangesAsync();
}
catch(Exception ex){
    Console.WriteLine(ex);
    throw;
}
#endregion

app.Run();
