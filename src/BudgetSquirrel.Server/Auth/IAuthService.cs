using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Server.Auth
{
  public interface IAuthService
    {
        Task<Account> GetCurrentUser();
        Task<Account> Authenticate(LoginRequest credentials);
    }
}