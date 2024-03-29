using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Domain.Funds;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public class EditFundNameCommand : ICommand<Fund>
  {
    private IFundRepository fundRepository;

    private int fundId;
    private string newName;

    public EditFundNameCommand(IFundRepository fundRepository, int fundId, string newName)
    {
      this.fundRepository = fundRepository;
      this.fundId = fundId;
      this.newName = newName;
    }
    
    public Task Execute(Fund loadedInputs)
    {
      return this.fundRepository.UpdateFund(loadedInputs.Id, new FundDetails(name: this.newName));
    }

    public Task<Fund> Load()
    {
      return this.fundRepository.GetFundById(this.fundId);
    }

    public Task<Fund> Validate(Fund loadedInputs)
    {
      if (this.newName.Length == 0)
      {
        throw new InvalidCommandArgumentException("Fund name cannot be blank.");
      }

      return Task.FromResult(loadedInputs);
    }
  }
}