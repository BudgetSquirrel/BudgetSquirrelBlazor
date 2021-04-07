using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;

namespace BudgetSquirrel.Backend.Biz.Accounts
{
  public interface IAccountRepository
  {
    Task<Account> GetByEmail(string email);
    Task CreateUser(string email, string password, string firstName, string lastName);
    Task DeleteUser(Guid id);
    Task<bool> IsPasswordAttemptCorrect(string email, string password);
  }
}