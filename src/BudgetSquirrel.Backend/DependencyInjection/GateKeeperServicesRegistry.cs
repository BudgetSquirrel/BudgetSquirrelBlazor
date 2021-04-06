using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetSquirrel.Backend.DependencyInjection
{
  public static class GateKeeperServicesRegistry
  {
    public static void AddGateKeeper(IServiceCollection services, IConfiguration config)
    {
      GateKeeperConfig gateKeeperConfig = ConfigurationReader.FromAppConfiguration(config);
      services.AddSingleton<GateKeeperConfig>(gateKeeperConfig);

      services.AddTransient<ICryptor, Rfc2898Encryptor>();
    }
  }
}