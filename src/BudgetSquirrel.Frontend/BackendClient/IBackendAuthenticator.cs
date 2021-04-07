using System.Net.Http;
using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.BackendClient
{
  public interface IBackendAuthenticator
  {
    Task<string> GetAuthToken(HttpClient client, string username, string password);
  }
}