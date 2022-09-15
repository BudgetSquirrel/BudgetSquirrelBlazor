using BudgetSquirrel.Common.Data.Infrastructure;

namespace BudgetSquirrel.Common.Data.Schema.Funds
{
  /// <summary>
  /// DTO for <see cref="Fund"/>
  /// </summary>
  public class FundDto
  {
    public int Id { get; set; }

    public string Name { get; set; }
    
    public decimal Balance { get; set; }

    public bool IsRoot { get; set; }

    public int ProfileId { get; set; }

    public int ParentFundId { get; set; }
  }
}