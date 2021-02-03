using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Server.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Web.Common.Messages.Auth;
using Dapper;

namespace BudgetSquirrel.Server.Dal.LocalDb.Accounts
{
  public class AccountRepository : IAccountRepository
  {
    private DbConnectionProvider connectionProvider;

    public AccountRepository(DbConnectionProvider connectionProvider)
    {
      this.connectionProvider = connectionProvider;
    }

    public async Task CreateUser(RegisterRequest newUser)
    {
      int numCreated = 0;
      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        numCreated = await conn.ExecuteAsync(
          "INSERT INTO [dbo].[Account] (\"Email\", \"Password\", \"FirstName\", \"LastName\") VALUES (@Email, @Password, @FirstName, @LastName);",
          new {
            Email = newUser.Email,
            Password = newUser.Password,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName
          });
      }
    }

    public Task DeleteUser(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<LoginUser> GetByEmail(string email)
    {
      throw new NotImplementedException();
    }
  }
}