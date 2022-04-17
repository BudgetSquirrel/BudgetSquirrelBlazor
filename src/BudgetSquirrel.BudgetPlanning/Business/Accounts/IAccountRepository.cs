using System;
using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Domain.Accounts;

namespace BudgetSquirrel.BudgetPlanning.Business.Accounts
{
  public interface IAccountRepository
  {
    Task<Account> GetByEmail(string email);
    Task CreateUser(string email, string password, string firstName, string lastName);
    Task DeleteUser(Guid id);
    Task<bool> IsPasswordAttemptCorrect(string email, string password);
  }
}