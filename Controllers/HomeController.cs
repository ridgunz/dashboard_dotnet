using Microsoft.AspNetCore.Mvc;

namespace test_dotnet.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly AppDbContext _dbContext;

  private readonly ProductService _productService;

  public HomeController(ILogger<HomeController> logger, AppDbContext dbContext, ProductService productService)
  {
    _logger = logger;
    _dbContext = dbContext;
    _productService = productService;
  }

  public async Task<IActionResult> IndexAsync()
  {
    var products = await _productService.GetAllProductsAsync();

    return View(products);
  }

  public IActionResult Privacy()
  {
    return View();
  }

}
