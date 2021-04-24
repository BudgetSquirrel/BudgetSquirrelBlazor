using System.Threading.Tasks;

namespace BudgetSquirrel.Backend.Biz
{
  public interface IQuery<TData>
  {
    Task<TData> Query();
  }
}