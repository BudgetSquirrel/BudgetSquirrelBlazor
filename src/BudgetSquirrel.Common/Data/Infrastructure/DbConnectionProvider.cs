using System.Data;
using System.Data.SqlClient;

namespace BudgetSquirrel.Common.Data.Infrastructure
{
  public class DbConnectionProvider
  {
    private string connectionString;

    public DbConnectionProvider(string connectionString)
    {
      this.connectionString = connectionString;
    }

    /// <summary>
    /// Opens a new connection to the Database.
    ///
    /// It is not meant to stay open indefinitely. It is designed to have
    /// a lifespan no longer than the execution of a single repository method.
    /// </summary>
    public IDbConnection GetConnection()
    {
      return new SqlConnection(this.connectionString);
    }
  }
}