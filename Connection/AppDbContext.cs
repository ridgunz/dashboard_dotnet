// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<Products> Products { get; set; }
  public DbSet<Categories> Categories { get; set; }
  public DbSet<Users> Users { get; set; }
}
