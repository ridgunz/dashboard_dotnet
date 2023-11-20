using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class CategoriesService
{
  private readonly AppDbContext _context;

  public CategoriesService(AppDbContext context)
  {
    _context = context;
  }

  public List<Categories> GetAllCategories()
  {
    return _context.Categories.ToList();
  }

  public Categories GetCategoriesById(int CategoriesId)
  {
    return _context.Categories.FirstOrDefault(c => c.Id == CategoriesId);
  }

  public void AddCategories(Categories Categories)
  {
    _context.Categories.Add(Categories);
    _context.SaveChanges();
  }

  public void UpdateCategories(Categories Categories)
  {
    var existingCategories = _context.Categories.FirstOrDefault(c => c.Id == Categories.Id);
    if (existingCategories != null)
    {
      existingCategories.CategoryName = Categories.CategoryName;
      // Update other properties as needed
      _context.SaveChanges();
    }
  }

  public void DeleteCategories(int CategoriesId)
  {
    var CategoriesToRemove = _context.Categories.FirstOrDefault(c => c.Id == CategoriesId);
    if (CategoriesToRemove != null)
    {
      _context.Categories.Remove(CategoriesToRemove);
      _context.SaveChanges();
    }
  }
}