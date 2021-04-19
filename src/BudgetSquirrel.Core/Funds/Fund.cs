namespace BudgetSquirrel.Core.Funds
{
  public class Fund
  {
    public Fund(string name, decimal balance, bool isRoot, int fundRootId, int id, int parentFundId)
    {
      this.Name = name;
      this.Balance = balance;
      this.IsRoot = isRoot;
      this.FundRootId = fundRootId;
      this.Id = id;
      ParentFundId = parentFundId;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }
    
    public decimal Balance { get; private set; }

    public bool IsRoot { get; private set; }

    public int FundRootId { get; private set; }

    public int ParentFundId { get; private set; }
  }
}