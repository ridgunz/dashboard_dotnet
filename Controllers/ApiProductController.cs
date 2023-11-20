// Controllers/ApiProductController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

[ApiController]
[Route("api/products")]
public class ApiProductController : Controller
{
  private readonly ProductService _productService;
  private readonly IWebHostEnvironment _environment;
  private readonly AppDbContext _dbContext;
  private readonly CategoriesService _categoryService;

  public ApiProductController(ProductService productService, IWebHostEnvironment environment, AppDbContext dbContext, CategoriesService CategoriesService)
  {
    _productService = productService;
    _environment = environment;
    _dbContext = dbContext;
    _categoryService = CategoriesService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllProducts()
  {
    var products = await _productService.GetAllProductsAsync();
    return Ok(products);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetProductById(int id)
  {
    var product = await _productService.GetProductByIdAsync(id);

    if (product == null)
    {
      return NotFound();
    }

    return Ok(product);
  }

  [HttpPost]
  public IActionResult Create()
  {
    try
    {
      // Access form data directly from the Request object
      var name = Request.Form["Name"];
      var price = Request.Form["Price"];
      var description = Request.Form["Description"];
      var categoryId = Request.Form["CategoryId"];
      var imageFile = Request.Form.Files["ImageFile"];

      // Example: Save file
      string imagePath = null;
      if (imageFile != null && imageFile.Length > 0)
      {
        imagePath = SaveFile(imageFile);
        // Handle imagePath as needed
      }


      var product = new Products
      {
        Name = name,
        Price = Convert.ToDecimal(price),
        Image = imagePath, // Replace with your logic
        CategoryId = Convert.ToInt32(categoryId),
        Description = description
      };

      _dbContext.Products.Add(product);
      _dbContext.SaveChanges();

      return Ok(product);
    }
    catch (Exception ex)
    {
      // Handle exceptions as needed
      return StatusCode(500, "Internal Server Error");
    }
  }

  [HttpPut("edit/{id}")]
  public IActionResult Edit(int id)
  {
    try
    {
      // Access form data directly from the Request object
      var name = Request.Form["Name"];
      var price = Request.Form["Price"];
      var description = Request.Form["Description"];
      var categoryId = Request.Form["CategoryId"];
      var imageFile = Request.Form.Files["ImageFile"];

      // Example: Find the existing product
      var product = _dbContext.Products.Find(id);

      if (product == null)
      {
        return NotFound();
      }

      product.Name = name;
      product.Price = Convert.ToDecimal(price);
      product.Description = description;
      product.CategoryId = Convert.ToInt32(categoryId);


      if (imageFile != null && imageFile.Length > 0)
      {

        if (!string.IsNullOrEmpty(product.Image))
        {
          DeleteImage(product.Image);
        }

        // Save the new image
        product.Image = SaveFile(imageFile);
      }

      _dbContext.Products.Update(product);
      _dbContext.SaveChanges();

      return Ok(product);
    }
    catch (Exception ex)
    {
      // Handle exceptions as needed
      return StatusCode(500, "Internal Server Error");
    }
  }

  [HttpDelete("delete/{id}")]
  public IActionResult Delete(int id)
  {
    try
    {

      var product = _dbContext.Products.Find(id);

      if (product == null)
      {
        return NotFound();
      }

      if (!string.IsNullOrEmpty(product.Image))
      {
        DeleteImage(product.Image);
      }

      _dbContext.Products.Remove(product);
      _dbContext.SaveChanges();

      return Ok(new { message = "Product deleted successfully" });
    }
    catch (Exception ex)
    {
      // Handle exceptions as needed
      return StatusCode(500, "Internal Server Error");
    }
  }


  private string SaveFile(IFormFile file)
  {
    var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

    using (var fileStream = new FileStream(filePath, FileMode.Create))
    {
      file.CopyTo(fileStream);
    }

    return "/images/" + uniqueFileName;
  }

  private void DeleteImage(string imagePath)
  {
    // Delete the image file
    var filePath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/'));

    if (System.IO.File.Exists(filePath))
    {
      System.IO.File.Delete(filePath);
    }
  }


}
