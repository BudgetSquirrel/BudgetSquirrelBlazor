using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Backend.Biz.Accounts;
using BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure;
using BudgetSquirrel.Web.Common.Messages.Auth;
using Dapper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;

namespace BudgetSquirrel.Backend.Dal.LocalDb.Accounts
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
      int userId = 0;

      string encryptedPassword = this.cryptor.Encrypt(
        password,
        this.gateKeeperConfig.EncryptionKey,
        this.gateKeeperConfig.Salt);

      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        // See schema.md
        userId = await conn.ExecuteScalarAsync<int>(
          "EXEC [dbo].[CreateAccount] @FirstName, @LastName, @Email, @Password",
          new {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = encryptedPassword,
          });
      }
    }

    public Task DeleteUser(Guid id)
    {
      throw new NotImplementedException();
    }

    public async Task<Account> GetByEmail(string email)
    {
      Account user;
      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        user = await conn.QuerySingleOrDefaultAsync<Account>(
          "EXEC [dbo].[GetAccountByEmail] @Email",
          new { Email = email });
      }
      return user;
    }

    public async Task<bool> IsPasswordAttemptCorrect(string email, string password)
    {
      string encryptedPassword = this.cryptor.Encrypt(
        password,
        this.gateKeeperConfig.EncryptionKey,
        this.gateKeeperConfig.Salt);
        
      string matchingEmail;
      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        matchingEmail = await conn.QuerySingleOrDefaultAsync<string>(
          "EXEC [dbo].[GetIsPasswordAttemptCorrect] @Email, @EncryptedPasswordAttempt",
          new {
            Email = email,
            EncryptedPasswordAttempt = encryptedPassword
          });
      }
      return matchingEmail != null;
    }
  }
}