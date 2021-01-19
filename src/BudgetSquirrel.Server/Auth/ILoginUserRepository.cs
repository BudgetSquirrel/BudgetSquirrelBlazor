using System.Threading.Tasks;

namespace BudgetSquirrel.Server.Auth
{
  public interface ILoginUserRepository
  {
    Task<LoginUser> GetByEmail(string email);
  }
}