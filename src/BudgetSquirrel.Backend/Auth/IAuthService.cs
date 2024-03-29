using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Domain.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Backend.Auth
{
  public interface IAuthService
    {
        Task<Account> GetCurrentUser();
        Task<Account> Authenticate(LoginRequest credentials);
    }
}