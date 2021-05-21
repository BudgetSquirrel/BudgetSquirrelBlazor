using System;
using System.Threading.Tasks;
using BudgetSquirrel.Frontend.BackendClient;
using BudgetSquirrel.Frontend.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public class LoginService : ILoginService
  {
    private const string AuthBearerTokenCookieName = "auth_token";

    private IBackendClient backend;
    private NavigationManager navigationManager;
    private ICookieService cookieService;

    private LoginContext loginContext;

    public LoginService(
      IBackendClient backend,
      NavigationManager navManager,
      ICookieService cookieService)
    {
      this.backend = backend;
      this.navigationManager = navManager;
      this.cookieService = cookieService;
      this.loginContext = LoginContext.Unknown;
    }

    public LoginContext Context => this.loginContext;

    public bool IsAuthenticated => this.loginContext.IsAuthenticated;

    public async Task Login(string username, string password)
    {
      this.loginContext = null;
      string authToken = await this.backend.Authenticate(username, password);
      await this.cookieService.SetCookie(AuthBearerTokenCookieName, authToken, 1);

      LoginContextDto contextDto = await this.backend.Fetch<LoginContextDto>(BackendArea.AuthArea.ME);
      this.loginContext = contextDto.ToLoginContext();
    }

    public async Task PromptLoginIfNecessary()
    {
      await this.InitializeAuthentication();
      if (!this.IsAuthenticated)
      {
        this.navigationManager.NavigateTo("login");
      }
    }

    private async Task InitializeAuthentication()
    {
      if (this.IsAuthenticated)
      {
        return;
      }

      string authToken = await this.cookieService.GetCookie(AuthBearerTokenCookieName);
      if (authToken != null)
      {
        this.backend.RestoreAuthentication(authToken);
        await this.InitializeLoginContext();
      }
    }

    private async Task InitializeLoginContext()
    {
      LoginContextDto contextDto = await this.backend.Fetch<LoginContextDto>(BackendArea.AuthArea.ME);
      this.loginContext = contextDto.ToLoginContext();
    }
  }
}