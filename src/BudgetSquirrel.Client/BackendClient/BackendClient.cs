using System;
using System.Threading.Tasks;

namespace BudgetSquirrel.Client.BackendClient
{
  public class BackendClient : IBackendClient
  {
    private BackendConfiguration backendConfiguration;

    public BackendClient(BackendConfiguration backendConfiguration)
    {
      this.backendConfiguration = backendConfiguration;
    }
    
    public Task ExecuteCommand(string endpoint, object request)
    {
      Console.WriteLine(this.backendConfiguration.RootUrl);
      throw new System.NotImplementedException();
    }
  }
}