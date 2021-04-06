namespace BudgetSquirrel.Backend.Auth
{
  public interface IJwtTokenAuthenticator
  {
    string GenerateToken(string username);
  }
}