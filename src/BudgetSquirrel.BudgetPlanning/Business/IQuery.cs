using System.Threading.Tasks;

namespace BudgetSquirrel.BudgetPlanning.Business
{
  public interface IQuery<TData>
  {
    Task<TData> Query();
  }
}