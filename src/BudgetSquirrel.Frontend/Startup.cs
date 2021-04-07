using System;
using BudgetSquirrel.Frontend.AppSettings;
using BudgetSquirrel.Frontend.Authentication;
using BudgetSquirrel.Frontend.BackendClient;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Frontend
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

    public void Configure(WebAssemblyHostBuilder builder)
    {
      builder.RootComponents.Add<App>("app");
    }
  }
}
