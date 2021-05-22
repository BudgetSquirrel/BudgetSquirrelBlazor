using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public interface ILoginService
  {
    LoginContext Context { get; }
    
    Task Login(string username, string password);

    Task PromptLoginIfNecessary();

    bool IsAuthenticated { get; }
  }
}