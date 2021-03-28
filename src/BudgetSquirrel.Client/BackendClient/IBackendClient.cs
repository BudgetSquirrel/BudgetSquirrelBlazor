using System.Threading.Tasks;

namespace BudgetSquirrel.Client.BackendClient
{
  public interface IBackendClient
  {
    Task ExecuteCommand(string endpoint, object request);
    Task<T> Fetch<T>(string endpoint);
    Task Authenticate(string username, string password);
  }
}