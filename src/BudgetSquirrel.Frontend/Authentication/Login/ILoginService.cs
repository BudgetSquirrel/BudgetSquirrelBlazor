using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.Authentication.Login
{
  public interface ILoginService
  {
    Task Login(string username, string password);
  }
}