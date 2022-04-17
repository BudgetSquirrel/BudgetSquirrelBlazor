using System;
using System.Data;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Domain.Accounts;
using BudgetSquirrel.BudgetPlanning.Business.Accounts;
using BudgetSquirrel.BudgetPlanning.Data.Infrastructure;
using Dapper;
using GateKeeper.Configuration;
using GateKeeper.Cryptogrophy;
using AuthProcedures = BudgetSquirrel.BudgetPlanning.Data.Schema.StoredProcedures.Auth;

namespace BudgetSquirrel.BudgetPlanning.Data.Accounts
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
      string encryptedPassword = this.cryptor.Encrypt(
        password,
        this.gateKeeperConfig.EncryptionKey,
        this.gateKeeperConfig.Salt);

      using (IDbConnection conn = this.connectionProvider.GetConnection())
      {
        // See schema.md
        await conn.ExecuteScalarAsync<int>(
          $"EXEC {AuthProcedures.CreateAccount} @FirstName, @LastName, @Email, @Password",
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
          $"EXEC {AuthProcedures.GetAccountByEmail} @Email",
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
          $"EXEC {AuthProcedures.GetIsPasswordAttemptCorrect} @Email, @EncryptedPasswordAttempt",
          new {
            Email = email,
            EncryptedPasswordAttempt = encryptedPassword
          });
      }
      return matchingEmail != null;
    }
  }
}