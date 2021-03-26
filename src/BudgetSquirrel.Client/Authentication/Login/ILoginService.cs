using System.Threading.Tasks;

namespace BudgetSquirrel.Client.Authentication.Login
{
  public interface ILoginService
  {
    Task Login(string username, string password);
  }
}