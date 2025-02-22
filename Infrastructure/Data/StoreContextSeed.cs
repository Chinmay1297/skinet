using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if(!context.Products.Any())  //if no products are available in DB, then seed them
        {
            var productsData = await File.ReadAllBytesAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>((productsData));

            if(products == null) return;
            foreach(var product in products)
            {
                product.CreatedDateTime = DateTime.Now;
                context.Products.Add(product);
            }
            
            await context.SaveChangesAsync();
        }    
    }
}
