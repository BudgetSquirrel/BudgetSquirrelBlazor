using System.Threading.Tasks;
using BudgetSquirrel.Frontend.Authentication.Login;
using BudgetSquirrel.Frontend.BackendClient;
using BudgetSquirrel.Frontend.Infrastructure;
using BudgetSquirrel.Web.Common.Messages.Auth;

namespace BudgetSquirrel.Frontend.Authentication.Registration
{
    public class RegistrationService : IRegistrationService
    {

        private readonly IBackendClient backendClient;
        private readonly ILoginService loginService;

        public RegistrationService(
          IBackendClient backendClient,
          ILoginService loginService)
        {
            this.backendClient = backendClient;
            this.loginService = loginService;
        }

        public async Task Register(string firstName, string lastName, string email, string password, string confirmPassword)
        {
            RegisterRequest request = new RegisterRequest()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            await this.backendClient.ExecuteCommand("auth/register", request);
            await this.loginService.Login(email, password);
        }
    }
}