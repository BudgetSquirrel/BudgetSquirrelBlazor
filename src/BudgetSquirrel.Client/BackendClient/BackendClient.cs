using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
    private BackendConfiguration backendConfiguration;
    private IBackendAuthenticator backendAuthenticator;

    private HttpClient client;

    public BackendClient(
      BackendConfiguration backendConfiguration,
      IBackendAuthenticator backendAuthenticator)
    {
      this.backendConfiguration = backendConfiguration;
      this.backendAuthenticator = backendAuthenticator;
    }
    
    public async Task ExecuteCommand(string endpoint, object request)
    {
      string requestJson = JsonConvert.SerializeObject(request);
      string url = $"{this.backendConfiguration.RootUrl}/backend/{endpoint}";
      HttpClient client = this.GetClient();

      HttpContent data = new StringContent(requestJson, Encoding.UTF8, "application/json");
      HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
      requestMessage.Content = data;
      
      HttpResponseMessage response = await client.SendAsync(requestMessage);

      if (response.StatusCode != HttpStatusCode.OK)
      {
        throw new BackendException();
      }
      
      return;
    }

    public async Task<T> Fetch<T>(string endpoint)
    {
      string url = $"{this.backendConfiguration.RootUrl}/backend/{endpoint}";
      HttpClient client = this.GetClient();
      HttpResponseMessage responseMessage = await client.GetAsync(url);

      if (responseMessage.StatusCode != HttpStatusCode.OK)
      {
        throw new BackendException();
      }

      string responseData = await responseMessage.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<T>(responseData);
    }

    public async Task Authenticate(string username, string password)
    {
      HttpClient client = this.GetClient();
      string authToken = await this.backendAuthenticator.GetAuthToken(client, username, password);
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
    }

    private HttpClient GetClient()
    {
      if (this.client == null)
      {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", this.backendConfiguration.RootUrl + "/");
        client.DefaultRequestHeaders.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
        client.DefaultRequestHeaders.Add("Access-Control-Allow-Headers", "Origin, Content-Type, X-Auth-Token");
        this.client = client;
      }
      return client;
    }
  }
}