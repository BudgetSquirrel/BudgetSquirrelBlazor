using System;
using BudgetSquirrel.Client.AppSettings;
using BudgetSquirrel.Client.Authentication;
using BudgetSquirrel.Client.BackendClient;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Client
{
  public class Startup
  {
    public const string ENVIRONMENT_LOCAL = "Local";

    public void ConfigureServices(IServiceCollection services)
    {
      string currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ENVIRONMENT_LOCAL;

      AppSettingsBase appSettings = this.GetAppSettings(currentEnv);
      services.AddSingleton<AppSettingsBase>(appSettings);

      BackendDependencyInjection.AddBackendClient(services);
      AuthenticationDependencyInjection.AddAuthenticationServices(services);
    }

    private AppSettingsBase GetAppSettings(string currentEnv)
    {
      AppSettingsBase appSettings;
      if (currentEnv == ENVIRONMENT_LOCAL)
      {
        appSettings = new AppSettingsLocal();
      }
      else
      {
        throw new InvalidOperationException($"Environment {currentEnv} not supported");
      }
      return appSettings;
    }

    public void Configure(IComponentsApplicationBuilder app)
    {
      app.AddComponent<App>("app");
    }
  }
}
