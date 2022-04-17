using System.Threading.Tasks;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public class DeleteBudgetCommand
  {
    private IBudgetRepository budgetRepository;

    private int fundId;
    private int timeboxId;

    public DeleteBudgetCommand(IBudgetRepository budgetRepository, int fundId, int timeboxId)
    {
      this.budgetRepository = budgetRepository;
      this.fundId = fundId;
      this.timeboxId = timeboxId;
    }

    public Task Execute()
    {
      return this.budgetRepository.DeleteBudget(this.fundId, this.timeboxId);
    }
  }
}