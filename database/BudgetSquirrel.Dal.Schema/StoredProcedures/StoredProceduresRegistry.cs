using System.Collections.Generic;

namespace BudgetSquirrel.Dal.Schema.StoredProcedures
{
  public class StoredProceduresRegistry
  {
    public static Dictionary<string, string> GetStoredProcedureFiles()
    {
      return new Dictionary<string, string>
      {
        { "CreateAccount", "StoredProcedures/Scripts/Auth/CreateAccount.sql" },
        { "GetAccountByEmail", "StoredProcedures/Scripts/Auth/GetAccountByEmail.sql" }
      };
    }
  }
}