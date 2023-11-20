using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

public class UsersController : Controller
{
  private readonly UsersService _UsersService;

  public UsersController(UsersService UsersService)
  {
    _UsersService = UsersService;
  }

  public IActionResult Index()
  {
    var users = _UsersService.GetAllUsers();
    return View(users);
  }

  public IActionResult Details(int id)
  {
    var users = _UsersService.GetUserById(id);
    if (users == null)
    {
      return NotFound();
    }

    return View(users);
  }

  public IActionResult Edit(int id)
  {
    var users = _UsersService.GetUserById(id);
    if (users == null)
    {
      return NotFound();
    }

    return View(users);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult Edit(Users Users)
  {
    if (ModelState.IsValid)
    {
      _UsersService.UpdateUser(Users);
      return RedirectToAction("Index");
    }
    else
    {
      foreach (var modelStateKey in ModelState.Keys)
      {
        var modelStateVal = ModelState[modelStateKey];
        foreach (ModelError error in modelStateVal.Errors)
        {
          Console.WriteLine($"{modelStateKey}: {error.ErrorMessage}");
        }
      }
    }

    return View(Users);
  }


}