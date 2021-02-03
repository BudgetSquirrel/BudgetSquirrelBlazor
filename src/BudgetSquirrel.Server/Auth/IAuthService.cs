using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Server.Auth
{
  public interface IAuthService
    {
        Task<LoginUser> GetCurrentUser();
        Task<LoginUser> Authenticate(LoginRequest credentials);
        Task SignInAsync(LoginUser user);
    }
}