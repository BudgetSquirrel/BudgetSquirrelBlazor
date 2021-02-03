using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Server.Biz.Accounts;
using BudgetSquirrel.Server.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Web.Common.Messages.Auth;
using Dapper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;

namespace BudgetSquirrel.Server.Dal.LocalDb.Accounts
{
  public class AccountRepository : IAccountRepository
  {
    private DbConnectionProvider connectionProvider;
    private GateKeeperConfig gateKeeperConfig;
    private ICryptor cryptor;

    public AccountRepository(
      DbConnectionProvider connectionProvider,
      GateKeeperConfig gateKeeperConfig,
      ICryptor cryptor)
    {
      this.connectionProvider = connectionProvider;
      this.gateKeeperConfig = gateKeeperConfig;
      this.cryptor = cryptor;
    }

    public async Task CreateUser(string email, string password, string firstName, string lastName)
    {
      int numCreated = 0;

      string encryptedPassword = this.cryptor.Encrypt(
        password,
        this.gateKeeperConfig.EncryptionKey,
        this.gateKeeperConfig.Salt);

      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        numCreated = await conn.ExecuteAsync(
          "INSERT INTO [dbo].[Account] (\"Email\", \"Password\", \"FirstName\", \"LastName\") VALUES (@Email, @Password, @FirstName, @LastName);",
          new {
            Email = email,
            Password = encryptedPassword,
            FirstName = firstName,
            LastName = lastName
          });
      }
    }

    public Task DeleteUser(Guid id)
    {
      throw new NotImplementedException();
    }

    public async Task<LoginUser> GetByEmail(string email)
    {
      LoginUser user;
      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        user = await conn.QuerySingleOrDefaultAsync<LoginUser>(
          "SELECT * FROM [dbo].[Account] WHERE \"Email\" = @Email",
          new { Email = email });
      }
      return user;
    }
  }
}