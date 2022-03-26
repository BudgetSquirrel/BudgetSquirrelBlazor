namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public class LoginContextDto
  {
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public LoginContext ToLoginContext()
    {
      return new LoginContext(this.Email, this.FirstName, this.LastName, true);
    }
  }
}