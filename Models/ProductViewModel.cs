using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductViewModel
{
  public int Id { get; set; }
  public int? CategoryId { get; set; }
  public string? Name { get; set; }
  public decimal? Price { get; set; }
  public string? Image { get; set; }
  public string? Description { get; set; }

  [Display(Name = "Product Image")]
  [DataType(DataType.Upload)]
  public IFormFile? ImageFile { get; set; }

  public string? ImagePath { get; set; }

  public DateTime? UpdatedAt { get; set; }

  [Display(Name = "Category")]
  public IEnumerable<SelectListItem>? Categories { get; set; }


}