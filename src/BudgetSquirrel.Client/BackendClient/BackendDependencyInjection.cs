using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Client.BackendClient
{
  public static class BackendDependencyInjection
  {
    public static IServiceCollection AddBackendClient(IServiceCollection services)
    {
      services.AddSingleton<BackendConfiguration>(provider => {

        IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
        BackendConfiguration backendConfig = configuration.GetSection("Backend").Get<BackendConfiguration>();
        return backendConfig;
      });

      services.AddSingleton<IBackendClient, BackendClient>();

      return services;
    }
  }
}