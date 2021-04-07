using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.Authentication.Registration
{
  public interface IRegistrationService
  {
    Task Register(string firstName, string lastName, string email, string password, string confirmPassword);
  }
}