using System.Threading.Tasks;
using BudgetSquirrel.Client.BackendClient;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Client.Authentication.Login
{
  public class LoginService : ILoginService
  {
    private IBackendClient backend;

    public LoginService(IBackendClient backend)
    {
      this.backend = backend;
    }

    public Task Login(string username, string password)
    {
      LoginRequest loginRequest = new LoginRequest()
      {
        Username = username,
        Password = password
      };
      return this.backend.ExecuteCommand("auth/login", loginRequest);
    }
  }
}