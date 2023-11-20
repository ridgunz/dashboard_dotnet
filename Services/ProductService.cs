// Services/ProductService.cs
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ProductService
{
  private readonly AppDbContext _context;
  private readonly ILogger<ProductService> _logger;


  public ProductService(AppDbContext context, ILogger<ProductService> logger)
  {
    _context = context;
    _logger = logger;
    _logger.LogInformation("ProductService instantiated.");
  }

  public async Task<List<Products>> GetAllProductsAsync()
  {
    // return await _context.Products.ToListAsync();
    var products = await _context.Products.ToListAsync();

    // Fetch category names separately
    var categoryIds = products.Select(p => p.CategoryId).ToList();
    var categories = await _context.Categories.Where(c => categoryIds.Contains(c.Id)).ToListAsync();

    // Assign category names to products
    foreach (var product in products)
    {
      product.Category = categories.FirstOrDefault(c => c.Id == product.CategoryId);
    }

    return products;
  }

  public async Task<Products> GetProductByIdAsync(int id)
  {
    return await _context.Products.FindAsync(id);
  }

  public async Task CreateProductAsync(Products product)
  {
    _context.Add(product);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateProductAsync(Products product)
  {
    _context.Update(product);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteProductAsync(int id)
  {
    var product = await _context.Products.FindAsync(id);
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();
  }

  public bool ProductExists(int id)
  {
    return _context.Products.Any(e => e.Id == id);
  }
}
