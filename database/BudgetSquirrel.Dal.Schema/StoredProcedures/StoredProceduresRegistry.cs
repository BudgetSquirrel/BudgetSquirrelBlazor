using System.Collections.Generic;

namespace BudgetSquirrel.Dal.Schema.StoredProcedures
{
  public class StoredProceduresRegistry
  {
    private const string ScriptsDir = "StoredProcedures/Scripts";

    public static Dictionary<string, string> GetStoredProcedureFiles()
    {
      return new Dictionary<string, string>
      {
        /* Auth */
        { "CreateAccount", $"{ScriptsDir}/Auth/CreateAccount.sql" },
        { "GetAccountByEmail", $"{ScriptsDir}/Auth/GetAccountByEmail.sql" },
        { "GetIsPasswordAttemptCorrect", $"{ScriptsDir}/Auth/GetIsPasswordAttemptCorrect.sql" },

        /* BudgetPlanning */
        { "CreateProfileForUser", $"{ScriptsDir}/BudgetPlanning/CreateProfileForUser.sql" },
        { "CreateFund", $"{ScriptsDir}/BudgetPlanning/CreateFund.sql" },
        { "CreateBudgetForFund", $"{ScriptsDir}/BudgetPlanning/CreateBudgetForFund.sql" },
        { "GetBudgetForFund", $"{ScriptsDir}/BudgetPlanning/GetBudgetForFund.sql" },
        { "EditBudget", $"{ScriptsDir}/BudgetPlanning/EditBudget.sql" },
        { "DeleteBudget", $"{ScriptsDir}/BudgetPlanning/DeleteBudget.sql" },

        /* Funds */
        { "GetAllFundsInFundTree", $"{ScriptsDir}/Funds/GetAllFundsInFundTree.sql" },
        { "GetProfile", $"{ScriptsDir}/Funds/GetProfile.sql" },
        { "GetFundById", $"{ScriptsDir}/Funds/GetFundById.sql" },
        { "UpdateFundDetails", $"{ScriptsDir}/Funds/UpdateFundDetails.sql" },
        { "GetRootFundForProfile", $"{ScriptsDir}/Funds/GetRootFundForProfile.sql" },

        /* History */
        { "CreateTimebox", $"{ScriptsDir}/History/CreateTimebox.sql" },
        { "GetTimebox", $"{ScriptsDir}/History/GetTimebox.sql" },
        { "GetTimeboxByDate", $"{ScriptsDir}/History/GetTimeboxByDate.sql" }
      };
    }
  }
}