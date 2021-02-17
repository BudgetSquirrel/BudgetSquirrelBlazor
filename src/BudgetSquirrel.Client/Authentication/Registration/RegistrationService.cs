using System.Threading.Tasks;
using BudgetSquirrel.Client.BackendClient;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Client.Authentication.Registration
{
  public class RegistrationService : IRegistrationService
  {
    private IBackendClient backendClient;

    public RegistrationService(IBackendClient backendClient)
    {
      this.backendClient = backendClient;
    }
    
    public Task Register(string firstName, string lastName, string email, string password, string confirmPassword)
    {
      RegisterRequest request = new RegisterRequest()
      {
        FirstName = firstName,
        LastName = lastName,
        Email = email,
        Password = password,
        ConfirmPassword = confirmPassword
      };
      return this.backendClient.ExecuteCommand("auth/register", request);
    }
  }
}