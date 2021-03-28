using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
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
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
        requestMessage.Content = data;
        requestMessage.SetBrowserRequestMode(BrowserRequestMode.Cors);
        
        HttpResponseMessage response = await client.SendAsync(requestMessage);

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