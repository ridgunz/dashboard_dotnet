// Controllers/CategoriesController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class CategoriesController : Controller
{
  private readonly CategoriesService _CategoriesService;

  public CategoriesController(CategoriesService CategoriesService)
  {
    _CategoriesService = CategoriesService;
  }

  public IActionResult Index()
  {
    var categories = _CategoriesService.GetAllCategories();
    return View(categories);
  }

  public IActionResult Details(int id)
  {
    var Categories = _CategoriesService.GetCategoriesById(id);
    if (Categories == null)
    {
      return NotFound();
    }

    return View(Categories);
  }

  public IActionResult Create()
  {
    return View();
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Create(Categories Categories)
  {
    if (ModelState.IsValid)
    {
      _CategoriesService.AddCategories(Categories);
      return RedirectToAction("Index");
    }

    return View(Categories);
  }

  public IActionResult Edit(int id)
  {
    var Categories = _CategoriesService.GetCategoriesById(id);
    if (Categories == null)
    {
      return NotFound();
    }

    return View(Categories);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Edit(Categories Categories)
  {
    if (ModelState.IsValid)
    {
      _CategoriesService.UpdateCategories(Categories);
      return RedirectToAction("Index");
    }

    return View(Categories);
  }

  public IActionResult Delete(int id)
  {
    var Categories = _CategoriesService.GetCategoriesById(id);
    if (Categories == null)
    {
      return NotFound();
    }

    return View(Categories);
  }

  [HttpPost, ActionName("DeleteConfirmed")]
  [ValidateAntiForgeryToken]
  public IActionResult DeleteConfirmed(int id)
  {
    _CategoriesService.DeleteCategories(id);
    return RedirectToAction("Index");
  }
}
