using BudgetSquirrel.BudgetPlanning.Data.Infrastructure;

namespace BudgetSquirrel.BudgetPlanning.Domain.Funds
{
  /// <summary>
  /// DTO for <see cref="Fund"/>
  /// </summary>
  public class FundDto : IDto<Fund>
  {
    public int Id { get; set; }

    public string Name { get; set; }
    
    public decimal Balance { get; set; }

    public bool IsRoot { get; set; }

    public int ProfileId { get; set; }

    public int ParentFundId { get; set; }

    public Fund ToDomain()
    {
      return new Fund(this.Name, this.Balance, this.IsRoot, this.ProfileId, this.Id, this.ParentFundId);
    }
  }
}