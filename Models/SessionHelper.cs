public static class SessionHelper
{
  private const string UserIdKey = "UserId";
  private const string LoggedInKey = "IsLoggedIn";
  private const string RoleKey = "UserRole";

  public static void SetUserId(HttpContext httpContext, int userId)
  {
    httpContext.Session.SetInt32(UserIdKey, userId);
  }

  public static int? GetUserId(HttpContext httpContext)
  {
    return httpContext.Session.GetInt32(UserIdKey);
  }

  public static void ClearUserId(HttpContext httpContext)
  {
    httpContext.Session.Remove(UserIdKey);
  }

  public static void SetRole(HttpContext context, string role)
  {
    context.Session.SetString(RoleKey, role);
  }

  public static string GetRole(HttpContext context)
  {
    return context.Session.GetString(RoleKey);
  }

  public static void SetLoggedIn(HttpContext httpContext, bool isLoggedIn)
  {
    httpContext.Session.SetBool(LoggedInKey, isLoggedIn);
  }

  public static bool GetLoggedIn(HttpContext httpContext)
  {
    return httpContext.Session.GetBool(LoggedInKey);
  }

  public static void ClearLoggedIn(HttpContext httpContext)
  {
    httpContext.Session.Remove(LoggedInKey);
  }
}

public static class SessionExtensions
{
  public static void SetBool(this ISession session, string key, bool value)
  {
    session.SetInt32(key, value ? 1 : 0);
  }

  public static bool GetBool(this ISession session, string key)
  {
    var value = session.GetInt32(key);
    return value.HasValue && value.Value != 0;
  }
}
