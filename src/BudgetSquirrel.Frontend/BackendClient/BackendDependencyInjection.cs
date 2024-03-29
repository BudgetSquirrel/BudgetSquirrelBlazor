using BudgetSquirrel.Frontend.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Frontend.BackendClient
{
  public static class BackendDependencyInjection
  {
    public static IServiceCollection AddBackendClient(IServiceCollection services)
    {
      services.AddSingleton<BackendConfiguration>(provider => {
        AppSettingsBase configuration = provider.GetRequiredService<AppSettingsBase>();
        return configuration.Backend;
      });

      services.AddSingleton<IBackendClient, BackendClient>();

      services.AddTransient<IBackendAuthenticator, BackendAuthenticator>();

      return services;
    }
  }
}