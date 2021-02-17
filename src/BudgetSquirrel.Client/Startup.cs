using BudgetSquirrel.Client.Authentication;
using BudgetSquirrel.Client.BackendClient;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Client
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      BackendDependencyInjection.AddBackendClient(services);
      AuthenticationDependencyInjection.AddAuthenticationServices(services);
    }

    public void Configure(IComponentsApplicationBuilder app)
    {
      app.AddComponent<App>("app");
    }
  }
}
