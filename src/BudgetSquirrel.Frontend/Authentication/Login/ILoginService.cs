using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public interface ILoginService
  {
    LoginContext Context { get; }
    
    Task Login(string username, string password);

    void PromptLoginIfNecessary();

    bool IsAuthenticated { get; }
  }
}