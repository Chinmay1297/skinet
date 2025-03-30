using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if (!context.Products.Any())  //if no products are available in DB, then seed them
        {
            var productsData = await File.ReadAllBytesAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>((productsData));

            if (products == null) return;
            foreach (var product in products)
            {
                product.CreatedDateTime = DateTime.Now;
                context.Products.Add(product);
            }

            await context.SaveChangesAsync();
        }

        if (!context.DeliveryMethods.Any())  //if no deliveryMethods are available in DB, then seed them
        {
            var deliveryMethodData = await File.ReadAllBytesAsync("../Infrastructure/Data/SeedData/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>((deliveryMethodData));

            if (deliveryMethods == null) return;
            foreach (var deliveryMethod in deliveryMethods)
            {
                deliveryMethod.CreatedDateTime = DateTime.Now;
                context.DeliveryMethods.Add(deliveryMethod);
            }

            await context.SaveChangesAsync();
        }
    }
}
