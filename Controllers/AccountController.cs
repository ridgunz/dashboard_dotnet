using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
  private readonly AppDbContext _dbContext;

  public AccountController(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public IActionResult Login()
  {
    return View();
  }

  [HttpPost]
  public IActionResult Login(string username, string password)
  {
    var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);

    if (user != null && VerifyPassword(password, user.Password))
    {
      SessionHelper.SetUserId(HttpContext, user.Id);
      SessionHelper.SetLoggedIn(HttpContext, true);
      SessionHelper.SetRole(HttpContext, user.Role);

      return RedirectToAction("Index", "Home");
    }

    // Failed login
    ModelState.AddModelError(string.Empty, "Invalid username or password");
    return View("Login");
  }

  private bool VerifyPassword(string enteredPassword, string hashedPassword)
  {
    // Implement password verification (e.g., using BCrypt)
    return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
  }

  public IActionResult Logout()
  {
    SessionHelper.ClearUserId(HttpContext);
    SessionHelper.SetLoggedIn(HttpContext, false);
    SessionHelper.SetRole(HttpContext, "");  // Clear the user's role

    return RedirectToAction("Index", "Home");
  }


  private Users AuthenticateUser(string username, string password)
  {
    // Replace this with actual user authentication logic using DbContext
    return _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
  }
}
