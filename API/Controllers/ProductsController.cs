using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> genericRepository;

    public ProductsController(IGenericRepository<Product> repository)
    {
        this.genericRepository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await genericRepository.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await genericRepository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        genericRepository.Add(product);
        if (await genericRepository.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
        return BadRequest("Failed to create product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (!genericRepository.Exists(id))
        {
            return NotFound();
        }

        genericRepository.Update(product);
        if (await genericRepository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to update product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await genericRepository.GetByIdAsync(id);
        if (product == null) return BadRequest("Product not found");

        genericRepository.Delete(product);
        if (await genericRepository.SaveAllAsync())
        {
            return NoContent();
        }

        return BadRequest("Failed to delete product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        //TODO: Implement logic
        return Ok();
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    {
        //TODO: Implement logic
        return Ok();
    }
}
