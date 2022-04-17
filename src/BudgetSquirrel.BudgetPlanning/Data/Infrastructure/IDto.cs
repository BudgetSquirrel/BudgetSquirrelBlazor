namespace BudgetSquirrel.BudgetPlanning.Data.Infrastructure
{
  public interface IDto<TDomain>
  {
    TDomain ToDomain();
  }
}