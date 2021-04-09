using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;

namespace BudgetSquirrel.Backend.Dal.LocalDb.Schema
{
  /// <summary>
  /// Contains SQL fragments which reference each stored procedure in the database schema.
  /// </summary>
  public static class StoredProcedures
  {
    /// <summary>
    /// Convenience pass through method for calling <see cref="SqlHelpers.StoredProcRef(string)">.
    /// </summary>
    private static string _(string storedProcName) => SqlHelpers.StoredProcRef(storedProcName);
    
    public static class Auth
    {
      public static readonly string CreateAccount = _("CreateAccount");
      public static readonly string GetAccountByEmail = _("GetAccountByEmail");
      public static readonly string GetIsPasswordAttemptCorrect = _("GetIsPasswordAttemptCorrect");
    }

    public static class BudgetPlanning
    {
      public static readonly string CreateBudgetForUser = _("CreateBudgetForUser");
    }
  }
}