using System.Threading.Tasks;
using BudgetSquirrel.Core.BudgetPlanning;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
{
  public class EditPlannedIncomeCommand : ICommand<Budget>
  {
    private IBudgetRepository budgetRepository;

    private int fundId;
    private int timeboxId;
    private decimal plannedIncome;

    public EditPlannedIncomeCommand(IBudgetRepository budgetRepository, int fundId, int timeboxId, decimal plannedIncome)
    {
      this.budgetRepository = budgetRepository;
      this.fundId = fundId;
      this.timeboxId = timeboxId;
      this.plannedIncome = plannedIncome;
    }
    
    public Task Execute(Budget loadedInputs)
    {
      loadedInputs.PlannedAmount = this.plannedIncome;
      return this.budgetRepository.SaveBudget(this.fundId, this.timeboxId, loadedInputs);
    }

    public Task<Budget> Load()
    {
      return this.budgetRepository.GetBudget(this.fundId, this.timeboxId);
    }

    public Task<Budget> Validate(Budget loadedInputs)
    {
      if (this.plannedIncome < 0)
      {
        throw new InvalidCommandArgumentException("Cannot enter a negative income.");
      }

      return Task.FromResult(loadedInputs);
    }
  }
}