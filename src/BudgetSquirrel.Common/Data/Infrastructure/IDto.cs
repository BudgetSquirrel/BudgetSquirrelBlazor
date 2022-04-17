namespace BudgetSquirrel.Common.Data.Infrastructure
{
  public interface IDto<TDomain>
  {
    TDomain ToDomain();
  }
}