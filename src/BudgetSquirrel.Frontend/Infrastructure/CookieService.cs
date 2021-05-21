using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BudgetSquirrel.Frontend.Infrastructure
{
  public class CookieService : ICookieService
  {
    private IJSRuntime jSRuntime;

    public CookieService(IJSRuntime jSRuntime)
    {
      this.jSRuntime = jSRuntime;
    }

    public Task<string> GetCookie(string name)
    {
      string setCookieInvokePath = JSRuntimeHelpers.InvokePath(JSRuntimeHelpers.ServicePathBrowserService, "getCookie");
      return this.jSRuntime.InvokeAsync<string>(setCookieInvokePath, name).AsTask();
    }

    public Task SetCookie(string name, string value, int hours)
    {
      string setCookieInvokePath = JSRuntimeHelpers.InvokePath(JSRuntimeHelpers.ServicePathBrowserService, "setCookie");
      return this.jSRuntime.InvokeAsync<object>(setCookieInvokePath, name, value, hours).AsTask();
    }
  }
}