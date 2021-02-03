using System;
using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Server.Dal.LocalDb.Accounts
{
  public interface IAccountRepository
  {
    Task<LoginUser> GetByEmail(string email);
    Task CreateUser(RegisterRequest newUser);
    Task DeleteUser(Guid id);
  }
}