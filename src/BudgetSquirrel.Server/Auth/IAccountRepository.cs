using System;
using System.Threading.Tasks;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Server.Auth
{
  public interface IAccountRepository
  {
    Task<LoginUser> GetByEmail(string email);
    Task CreateUser(RegisterRequest newUser);
    Task DeleteUser(Guid id);
  }
}