using System;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepository repository;

    public ProductsController(IProductsRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
    {
        return Ok(await repository.GetProductsAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetProductByIdAsync(id);
        if(product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.AddProduct(product);
        if(await repository.SaveChangesAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        return BadRequest("Failed to create product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if(repository.ProductExists(id))
        {
            return NotFound();
        }

        repository.UpdateProduct(product);
        if(await repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to update product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repository.GetProductByIdAsync(id);
        if(product==null) return BadRequest("Product not found");

        repository.DeleteProduct(product);
        if(await repository.SaveChangesAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to delete product");
    }
}
