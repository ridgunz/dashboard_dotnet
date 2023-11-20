using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class UsersService
{
  private readonly AppDbContext _dbContext;

  public UsersService(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public List<Users> GetAllUsers()
  {
    return _dbContext.Users.ToList();
  }

  public Users GetUserById(int userId)
  {
    return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
  }

  public void AddUser(Users user)
  {
    _dbContext.Users.Add(user);
    _dbContext.SaveChanges();
  }

  public void UpdateUser(Users user)
  {
    var existingUser = _dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
    if (existingUser != null)
    {
      existingUser.Role = user.Role;
      _dbContext.SaveChanges();
    }
  }

  public void DeleteUser(int userId)
  {
    var userToRemove = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    if (userToRemove != null)
    {
      _dbContext.Users.Remove(userToRemove);
      _dbContext.SaveChanges();
    }
  }
}