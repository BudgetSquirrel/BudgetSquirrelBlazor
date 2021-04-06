using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BudgetSquirrel.Web.Common.Messages.Auth;
using Newtonsoft.Json;

namespace BudgetSquirrel.Frontend.BackendClient
{
  public class BackendAuthenticator : IBackendAuthenticator
  {
    private BackendConfiguration backendConfiguration;

    public BackendAuthenticator(BackendConfiguration backendConfiguration)
    {
      this.backendConfiguration = backendConfiguration;
    }

    public async Task<string> GetAuthToken(HttpClient client, string username, string password)
    {
      LoginRequest loginRequest = new LoginRequest()
      {
        Username = username,
        Password = password
      };
      string requestJson = JsonConvert.SerializeObject(loginRequest);
      string url = $"{this.backendConfiguration.RootUrl}/backend/auth/authenticate";
      HttpContent data = new StringContent(requestJson, Encoding.UTF8, "application/json");

      HttpResponseMessage response = await client.PostAsync(url, data);

      if (response.StatusCode != HttpStatusCode.OK)
      {
        throw new BackendException();
      }

      string responseData = await response.Content.ReadAsStringAsync();

      return responseData;
    }
  }
}