using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
      string requestJson = JsonConvert.SerializeObject(request);
      string url = $"{this.backendConfiguration.RootUrl}/backend/{endpoint}";
      using (HttpClient client = this.GetClient())
      {
        HttpContent data = new StringContent(requestJson, Encoding.UTF8, "application/json");
        return client.PostAsync(url, data);
      }
    }

    private HttpClient GetClient()
    {
      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", this.backendConfiguration.RootUrl + "/");
      client.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
      client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");
      return client;
    }
  }
}