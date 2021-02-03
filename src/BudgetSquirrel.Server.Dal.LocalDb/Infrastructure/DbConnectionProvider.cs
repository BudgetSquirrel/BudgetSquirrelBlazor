using System.Data;
using System.Data.SqlClient;

namespace BudgetSquirrel.Server.Dal.LocalDb.Infrastructure
{
  public class DbConnectionProvider
  {
    private string connectionString;

    public DbConnectionProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }

    public IDbConnection GetConnection()
    {
      return new SqlConnection(this.connectionString);
    }
  }
}