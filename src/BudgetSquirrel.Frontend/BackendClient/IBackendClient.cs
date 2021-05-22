using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.BackendClient
{
  public interface IBackendClient
  {
    Task ExecuteCommand(string endpoint, object request);
    Task<T> Fetch<T>(string endpoint);
    Task<string> Authenticate(string username, string password);
    void RestoreAuthentication(string authToken);
  }
}