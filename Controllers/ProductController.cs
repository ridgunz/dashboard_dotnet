// Controllers/ProductController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductController : Controller
{
  private readonly ProductService _productService;
  private readonly IWebHostEnvironment _environment;
  private readonly AppDbContext _dbContext;
  private readonly CategoriesService _categoryService;

  public ProductController(ProductService productService, IWebHostEnvironment environment, AppDbContext dbContext, CategoriesService CategoriesService)
  {
    _productService = productService;
    _environment = environment;
    _dbContext = dbContext;
    _categoryService = CategoriesService;
  }

  public async Task<IActionResult> Index()
  {
    var products = await _productService.GetAllProductsAsync();

    return View(products);
  }

  [HttpGet]
  public IActionResult Create()
  {
    var categories = _categoryService.GetAllCategories();

    var productViewModel = new ProductViewModel
    {
      Categories = categories.Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.CategoryName
      })
    };

    return View(productViewModel);
  }


  [HttpPost]
  public IActionResult Create(ProductViewModel model)
  {
    if (ModelState.IsValid)
    {
      if (model.ImageFile != null)
      {
        var imagePath = SaveFile(model.ImageFile);
        model.ImagePath = imagePath;
      }

      var product = new Products
      {
        Name = model.Name,
        Price = model.Price,
        Image = model.ImagePath,
        CategoryId = model.CategoryId,
        Description = model.Description
      };

      _dbContext.Products.Add(product);
      _dbContext.SaveChanges();

      return RedirectToAction("Index");
    }

    return View(model);
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


  public async Task<IActionResult> Edit(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var product = _dbContext.Products.Find(id);
    var categories = _categoryService.GetAllCategories();

    // Map categories to SelectListItem
    var categoryList = categories.Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.CategoryName
    }).ToList();

    var productViewModel = new ProductViewModel
    {
      Id = product.Id,
      Name = product.Name,
      Price = product.Price,
      Description = product.Description,
      CategoryId = product.CategoryId,
      UpdatedAt = DateTime.Now,
      Categories = categoryList
    };

    return View(productViewModel);
  }


  [HttpPost]
  public IActionResult Edit(ProductViewModel model)
  {
    Console.WriteLine("Category " + model.CategoryId);

    if (ModelState.IsValid)
    {
      var product = _dbContext.Products.Find(model.Id);

      if (product != null)
      {
        // Update the properties of the existing product
        product.Name = model.Name;
        product.Price = model.Price;
        product.Description = model.Description;
        product.CategoryId = model.CategoryId;

        // Handle file upload
        if (model.ImageFile != null)
        {
          // Delete the old image if it exists
          if (!string.IsNullOrEmpty(product.Image))
          {
            DeleteImage(product.Image);
          }

          // Save the new image
          product.Image = SaveFile(model.ImageFile);
        }

        // Update the product in the database
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
      }
      else
      {
        // Product not found
        return NotFound();
      }
    }

    // ModelState is not valid, retrieve categories again
    var categories = _categoryService.GetAllCategories();

    // Map categories to SelectListItem
    var categoryList = categories.Select(c => new SelectListItem
    {
      Value = c.Id.ToString(),
      Text = c.CategoryName
    }).ToList();

    // Set the Categories property again
    model.Categories = categoryList;

    // Check ModelState errors
    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
      Console.WriteLine(error.ErrorMessage);
    }

    return View(model);
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

  public async Task<IActionResult> Delete(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var product = await _productService.GetProductByIdAsync(id.Value);
    if (product == null)
    {
      return NotFound();
    }

    return View(product);
  }

  [HttpPost, ActionName("DeleteConfirmed")]
  public async Task<IActionResult> DeleteConfirmed(int id)
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
    await _productService.DeleteProductAsync(id);
    return RedirectToAction(nameof(Index));
  }

}
