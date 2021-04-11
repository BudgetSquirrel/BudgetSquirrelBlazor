namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public class LoginContextDto
  {
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public LoginContext ToLoginContext()
    {
      return new LoginContext(this.Email, this.FirstName, this.LastName, true);
    }
  }
}