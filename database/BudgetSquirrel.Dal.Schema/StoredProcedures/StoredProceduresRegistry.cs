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
        { "CreateOverallBudgetForUser", $"{ScriptsDir}/BudgetPlanning/CreateOverallBudgetForUser.sql" }
      };
    }
  }
}