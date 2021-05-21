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
      public static readonly string CreateProfileForUser = _("CreateProfileForUser");
      public static readonly string CreateFund = _("CreateFund");
      public static readonly string CreateBudgetForFund = _("CreateBudgetForFund");
      public static readonly string GetBudgetForFund = _("GetBudgetForFund");
    }

    public static class Funds
    {
      public static readonly string GetAllFundsInFundTree = _("GetAllFundsInFundTree");
      public static readonly string GetProfile = _("GetProfile");
    }

    public static class History
    {
      public static readonly string GetTimebox = _("GetTimebox");
      public static readonly string GetTimeboxByStartDate = _("GetTimeboxByStartDate");
      public static readonly string CreateTimebox = _("CreateTimebox");
    }
  }
}