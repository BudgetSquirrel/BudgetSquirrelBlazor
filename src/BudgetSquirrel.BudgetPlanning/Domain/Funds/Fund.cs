namespace BudgetSquirrel.BudgetPlanning.Domain.Funds
{
  public class Fund
  {
    public Fund(string name, decimal balance, bool isRoot, int profileId, int id, int parentFundId)
    {
      this.Name = name;
      this.Balance = balance;
      this.IsRoot = isRoot;
      this.ProfileId = profileId;
      this.Id = id;
      this.ParentFundId = parentFundId;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }
    
    public decimal Balance { get; private set; }

    public bool IsRoot { get; private set; }

    public int ProfileId { get; private set; }

    public int ParentFundId { get; private set; }
  }
}