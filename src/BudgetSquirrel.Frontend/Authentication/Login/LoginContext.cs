namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public class LoginContext
  {
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public bool IsAuthenticated { get; private set; }

    public LoginContext(
      string email,
      string firstName,
      string lastName,
      bool isAuthenticated)
    {
      this.Email = email;
      this.FirstName = firstName;
      this.LastName = lastName;
      this.IsAuthenticated = isAuthenticated;
    }

    public static readonly LoginContext Unknown = new LoginContext("", "Unknown", "User", false);
  }
}