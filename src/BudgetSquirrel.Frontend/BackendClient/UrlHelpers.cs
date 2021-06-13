using System.Collections.Generic;

namespace BudgetSquirrel.Frontend.BackendClient
{
  public static class UrlHelpers
  {
    public static string ToQueryString(Dictionary<string, object> queryParams)
    {
      string queryString = "";
      bool includeAmpersand = false;
      foreach (KeyValuePair<string, object> queryParam in queryParams)
      {
        string queryParamValue = queryParam.Value.ToString();
        if (includeAmpersand)
        {
          queryString += "&";
        }
        queryString += $"{queryParam.Key}={queryParamValue}";
        includeAmpersand = true;
      }

      return queryString;
    }
  }
}