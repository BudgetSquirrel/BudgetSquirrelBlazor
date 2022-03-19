using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.BackendClient
{
  public interface IBackendClient
  {
    Task ExecuteCommand(string endpoint, object request);
    Task ExecuteCommand(string endpoint);
    Task<T> Fetch<T>(string endpoint);
    Task<T> Fetch<T>(string endpoint, Dictionary<string, object> queryParams);
    Task<string> Authenticate(string username, string password);
    void RestoreAuthentication(string authToken);
  }
}