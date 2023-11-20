public class Products
{
  public int Id { get; set; }
  public int? CategoryId { get; set; }
  public string? Name { get; set; }
  public decimal? Price { get; set; }
  public string? Image { get; set; }
  public string? Description { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public Categories? Category { get; set; }

}