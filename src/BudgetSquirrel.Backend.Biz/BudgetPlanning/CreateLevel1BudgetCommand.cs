using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.Funds;
using BudgetSquirrel.Backend.Biz.History;
using BudgetSquirrel.Core.BudgetPlanning;
using BudgetSquirrel.Core.Funds;
using BudgetSquirrel.Core.History;

namespace BudgetSquirrel.Backend.Biz.BudgetPlanning
{
  public class CreateLevel1BudgetCommandLoadedContext
  {
    public FundBudget RootFund { get; private set; }
    public Timebox Timebox { get; private set; }

    public CreateLevel1BudgetCommandLoadedContext(
      Fund rootFund,
      Budget rootBudget,
      Timebox timebox)
    {
      this.RootFund = new FundBudget(rootBudget, rootFund);
      this.Timebox = timebox;
    }
  }

  public class CreateLevel1BudgetCommand : ICommand<CreateLevel1BudgetCommandLoadedContext>
  {
    private IBudgetRepository budgetRepository;
    private IFundRepository fundRepository;
    private ITimeboxRepository timeboxRepository;

    private int profileId;
    private int timeboxId;
    private string name;
    private decimal plannedAmount;

    public CreateLevel1BudgetCommand(
      IBudgetRepository budgetRepository,
      IFundRepository fundRepository,
      ITimeboxRepository timeboxRepository,
      int profileId,
      int timeboxId,
      string name,
      decimal plannedAmount)
    {
      this.budgetRepository = budgetRepository;
      this.fundRepository = fundRepository;
      this.timeboxRepository = timeboxRepository;

      this.profileId = profileId;
      this.timeboxId = timeboxId;
      this.name = name;
      this.plannedAmount = plannedAmount;
    }
    
    public async Task Execute(CreateLevel1BudgetCommandLoadedContext loadedInputs)
    {
      int fundId = await this.budgetRepository.CreateFund(this.profileId, loadedInputs.RootFund.Fund.Id, this.name, false);
      await this.budgetRepository.CreateBudgetForFund(fundId, this.plannedAmount, this.timeboxId);
    }

    public async Task<CreateLevel1BudgetCommandLoadedContext> Load()
    {
      Fund rootFund = await this.fundRepository.GetRootFundForProfile(this.profileId);
      Budget rootBudget = await this.budgetRepository.GetBudget(rootFund.Id, this.timeboxId);
      Timebox timebox = await this.timeboxRepository.GetTimebox(this.timeboxId);
      return new CreateLevel1BudgetCommandLoadedContext(rootFund, rootBudget, timebox);
    }

    public Task<CreateLevel1BudgetCommandLoadedContext> Validate(CreateLevel1BudgetCommandLoadedContext loadedInputs)
    {
      if (loadedInputs.RootFund.Fund == null)
      {
        throw new InvalidCommandArgumentException("Cannot find profile for id " + this.profileId);
      }
      if (loadedInputs.Timebox == null)
      {
        throw new InvalidCommandArgumentException($"Cannot find timebox with id {this.timeboxId} for profile id {this.profileId}");
      }
      
      if (this.plannedAmount < 0)
      {
        throw new InvalidCommandArgumentException("Planned amount cannot be negative");
      }
      if (string.IsNullOrWhiteSpace(this.name))
      {
        throw new InvalidCommandArgumentException("Name cannot be empty");
      }

      return Task.FromResult(loadedInputs);
    }
  }
}