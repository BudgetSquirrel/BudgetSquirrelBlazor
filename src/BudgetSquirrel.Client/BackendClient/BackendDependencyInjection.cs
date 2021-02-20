using BudgetSquirrel.Client.AppSettings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Client.BackendClient
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

      return services;
    }
  }
}