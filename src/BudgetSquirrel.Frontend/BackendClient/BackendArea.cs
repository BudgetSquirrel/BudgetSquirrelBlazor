namespace BudgetSquirrel.Frontend.BackendClient
{
  public static class BackendArea
  {
    public static class AuthArea
    {
      private const string AREA_PREFIX = "auth";
      public const string AUTHENTICATE = AREA_PREFIX + "/authenticate";
      public const string ME = AREA_PREFIX + "/me";
    }
  }
}