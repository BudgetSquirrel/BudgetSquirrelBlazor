namespace BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure
{
  /// <summary>
  /// Contains helpers which return fragments of SQL that can be used together to
  /// write SQL commands and queries.
  /// </summary>
  public static class SqlHelpers
  {
    /// <summary>
    /// Returns a SQL fragment string which references the given stored procedure.
    /// </summary>
    public static string StoredProcRef(string storedProc)
    {
      return $"[dbo].[{storedProc}]";
    }
  }
}