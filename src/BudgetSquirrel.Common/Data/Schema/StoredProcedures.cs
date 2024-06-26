using BudgetSquirrel.Common.Data.Infrastructure;

namespace BudgetSquirrel.Common.Data.Schema
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
      public static readonly string EditBudget = _("EditBudget");
      public static readonly string DeleteBudget = _("DeleteBudget");
      public static readonly string SetBudgetIsFinalized = _("SetBudgetIsFinalized");
    }

    public static class Funds
    {
      public static readonly string GetAllFundsInFundTree = _("GetAllFundsInFundTree");
      public static readonly string GetProfile = _("GetProfile");
      public static readonly string GetFundById = _("GetFundById");
      public static readonly string UpdateFundDetails = _("UpdateFundDetails");
      public static readonly string GetRootFundForProfile = _("GetRootFundForProfile");
    }

    public static class History
    {
      public static readonly string GetTimebox = _("GetTimebox");
      public static readonly string GetTimeboxByDate = _("GetTimeboxByDate");
      public static readonly string GetLastTimebox = _("GetLastTimebox");
      public static readonly string CreateTimebox = _("CreateTimebox");
    }

    public static class Transactions
    {
      public static readonly string GetTransactionsByFundAndDateRange = _("GetTransactionsByFundAndDateRange");
      public static readonly string GetTransactionById = _("GetTransactionById");
      public static readonly string CreateTransaction = _("CreateTransaction");
      public static readonly string CreateTransactionAllocation = _("CreateTransactionAllocation");
      public static readonly string DeleteTransaction = _("DeleteTransaction");
    }
  }
}