using System.Threading.Tasks;

namespace BudgetSquirrel.Frontend.Infrastructure
{
  public interface ICookieService
  {
    Task SetCookie(string name, string value, int hours);

    Task<string> GetCookie(string name);
  }
}