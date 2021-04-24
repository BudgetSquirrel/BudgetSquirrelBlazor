using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace BudgetSquirrel.Dal.Schema.StoredProcedures
{
  public class CreateStoredProcedures
  {
    public static void CreateRegisteredProcedures(string connectionString)
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        Dictionary<string, string> stroredProcedureRegistry = StoredProceduresRegistry.GetStoredProcedureFiles();
        foreach (string procedureName in stroredProcedureRegistry.Keys)
        {
          string procedureFilePath = stroredProcedureRegistry[procedureName];

          try
          {
            DropProcedure(procedureName, connection);
            CreateProcedure(procedureFilePath, connection);
          }
          catch (Exception e)
          {
            throw new Exception($"Exception thrown while migrating Stored Procedure in {procedureFilePath}", e);
          }
        }
      }
    }

    private static string GetProcedureSql(string scriptFilePath)
    {
      string sql = File.ReadAllText(scriptFilePath);
      return sql;
    }

    private static void DropProcedure(string procedureName, SqlConnection connection)
    {
      string sql = $@"
      IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{procedureName}') AND type in (N'P', N'PC'))
        DROP PROCEDURE [dbo].[{procedureName}]";
      using (SqlCommand command = new SqlCommand(sql, connection))
      {
        command.ExecuteNonQuery();
      }
    }

    private static void CreateProcedure(string procedureFilePath, SqlConnection connection)
    {
      string createProcedureSql = GetProcedureSql(procedureFilePath);
      using (SqlCommand createCommand = new SqlCommand(createProcedureSql, connection))
      {
        createCommand.ExecuteNonQuery();
      }
    }
  }
}