using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;

namespace BudgetSquirrel.Server.Biz.Accounts
{
  public interface IAccountRepository
  {
    Task<LoginUser> GetByEmail(string email);
    Task CreateUser(string email, string password, string firstName, string lastName);
    Task DeleteUser(Guid id);
  }
}