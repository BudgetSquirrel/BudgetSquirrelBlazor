using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Server.Auth
{
  public static class AuthMessageResolver
  {
    public static CurrentUserResponse ToApiMessage(LoginUser user)
    {
      return new CurrentUserResponse()
      {
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName
      };
    }
  }
}