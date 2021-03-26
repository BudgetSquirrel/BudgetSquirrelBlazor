using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace BudgetSquirrel.Client.BackendClient
{
  public class BackendClient : IBackendClient
  {
    private IJSRuntime jSRunTime;
    private BackendConfiguration backendConfiguration;

    public BackendClient(
      IJSRuntime jSRuntime,
      BackendConfiguration backendConfiguration)
    {
      this.jSRunTime = jSRuntime;
      this.backendConfiguration = backendConfiguration;
    }
    
    public async Task ExecuteCommand(string endpoint, object request)
    {
      string requestJson = JsonConvert.SerializeObject(request);
      string url = $"{this.backendConfiguration.RootUrl}/backend/{endpoint}";
      HttpClient client = this.GetClient();
      try
      {
        HttpContent data = new StringContent(requestJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync(url, data);

        IEnumerable<string> headers = response.Headers.Select(h => h.Key + " - " + h.Value);
        Console.WriteLine("Headers: " + string.Join(",", headers));
        IEnumerable<string> cookies = response.Headers.Single(h => h.Key == "cookie").Value;
        Console.WriteLine("Cookies: " + string.Join(",", cookies));
        // await this.jSRunTime.InvokeVoidAsync("eval", $"document.cookie = \"{cookieValue}\"");

        if (response.StatusCode != HttpStatusCode.OK)
        {
          throw new BackendException();
        }
      }
      finally{
        client.Dispose();
      }
      
      return;
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