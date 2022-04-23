using BudgetSquirrel.BudgetPlanning.Domain.Accounts;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Backend.Resolvers
{
  public static class AuthMessageResolver
  {
    public static CurrentUserResponse ToApiMessage(Account user)
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