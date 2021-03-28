namespace BudgetSquirrel.Server.Auth
{
  public interface IJwtTokenAuthenticator
  {
    string GenerateToken(string username);
  }
}