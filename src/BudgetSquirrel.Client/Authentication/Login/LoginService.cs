using System;
using System.Threading.Tasks;
using BudgetSquirrel.Client.BackendClient;

namespace BudgetSquirrel.Client.Authentication.Login
{
  public class LoginService : ILoginService
  {
    private IBackendClient backend;

    private LoginContext loginContext;

    public LoginService(IBackendClient backend)
    {
      this.backend = backend;
    }

    public async Task Login(string username, string password)
    {
      this.loginContext = null;
      await this.backend.Authenticate(username, password);
      this.loginContext = await this.backend.Fetch<LoginContext>("auth/me");
    }
  }
}