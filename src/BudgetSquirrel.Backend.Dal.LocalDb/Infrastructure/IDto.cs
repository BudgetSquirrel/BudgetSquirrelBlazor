namespace BudgetSquirrel.Backend.Dal.LocalDb.Infrastructure
{
  public interface IDto<TDomain>
  {
    TDomain ToDomain();
  }
}