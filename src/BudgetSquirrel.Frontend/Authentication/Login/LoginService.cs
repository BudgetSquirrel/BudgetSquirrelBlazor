using System;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BackendClient;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public class LoginService : ILoginService
  {
    private IBackendClient backend;
    private NavigationManager navigationManager;

    private LoginContext loginContext;

    public LoginService(IBackendClient backend, NavigationManager navManager)
    {
      this.backend = backend;
      this.navigationManager = navManager;
      this.loginContext = LoginContext.Unknown;
    }

    public LoginContext Context => this.loginContext;

    public bool IsAuthenticated => this.loginContext.IsAuthenticated;

    public async Task Login(string username, string password)
    {
      this.loginContext = null;
      await this.backend.Authenticate(username, password);
      LoginContextDto contextDto = await this.backend.Fetch<LoginContextDto>(BackendArea.AuthArea.ME);
      this.loginContext = contextDto.ToLoginContext();
    }

    public void PromptLoginIfNecessary()
    {
      if (!this.IsAuthenticated)
      {
        this.navigationManager.NavigateTo("login");
      }
    }
  }
}