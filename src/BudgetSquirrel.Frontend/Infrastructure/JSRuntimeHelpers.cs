namespace BudgetSquirrel.Frontend.Infrastructure
{
  public static class JSRuntimeHelpers
  {
    public const string ServicePathBrowserService = "window.browserService";

    public static string InvokePath(string servicePath, string member)
    {
      return $"{servicePath}.{member}";
    }
  }
}