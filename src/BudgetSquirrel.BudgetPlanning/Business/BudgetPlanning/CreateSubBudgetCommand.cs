using System.Threading.Tasks;
using BudgetSquirrel.BudgetPlanning.Business.Funds;
using BudgetSquirrel.BudgetPlanning.Business.History;
using BudgetSquirrel.BudgetPlanning.Domain.BudgetPlanning;
using BudgetSquirrel.BudgetPlanning.Domain.Funds;
using BudgetSquirrel.BudgetPlanning.Domain.History;

namespace BudgetSquirrel.BudgetPlanning.Business.BudgetPlanning
{
  public class CreateSubBudgetCommandLoadedContext
  {
    public FundBudget ParentFund { get; private set; }
    public Timebox Timebox { get; private set; }

    public CreateSubBudgetCommandLoadedContext(
      Fund parentFund,
      Budget parentBudget,
      Timebox timebox)
    {
      this.ParentFund = new FundBudget(parentBudget, parentFund);
      this.Timebox = timebox;
    }
  }

  public class CreateSubBudgetCommand : ICommand<CreateSubBudgetCommandLoadedContext>
  {
    private IBudgetRepository budgetRepository;
    private IFundRepository fundRepository;
    private ITimeboxRepository timeboxRepository;

    private int parentFundId;
    private int timeboxId;
    private string name;
    private decimal plannedAmount;

    public CreateSubBudgetCommand(
      IBudgetRepository budgetRepository,
      IFundRepository fundRepository,
      ITimeboxRepository timeboxRepository,
      int parentFundId,
      int timeboxId,
      string name,
      decimal plannedAmount)
    {
      this.budgetRepository = budgetRepository;
      this.fundRepository = fundRepository;
      this.timeboxRepository = timeboxRepository;

      this.parentFundId = parentFundId;
      this.timeboxId = timeboxId;
      this.name = name;
      this.plannedAmount = plannedAmount;
    }
    
    public async Task Execute(CreateSubBudgetCommandLoadedContext loadedInputs)
    {
      int profileId = loadedInputs.ParentFund.Fund.ProfileId;
      int parentFundId = loadedInputs.ParentFund.Fund.Id;
      int fundId = await this.budgetRepository.CreateFund(profileId, parentFundId, this.name, false);
      await this.budgetRepository.CreateBudgetForFund(fundId, this.plannedAmount, this.timeboxId);
    }

    public async Task<CreateSubBudgetCommandLoadedContext> Load()
    {
      Fund parentFund = await this.fundRepository.GetFundById(this.parentFundId);
      Budget parentBudget = await this.budgetRepository.GetBudget(parentFund.Id, this.timeboxId);
      Timebox timebox = await this.timeboxRepository.GetTimebox(this.timeboxId);
      return new CreateSubBudgetCommandLoadedContext(parentFund, parentBudget, timebox);
    }

    public Task<CreateSubBudgetCommandLoadedContext> Validate(CreateSubBudgetCommandLoadedContext loadedInputs)
    {
      int profileId = loadedInputs.ParentFund.Fund.ProfileId;

      if (loadedInputs.ParentFund.Fund == null)
      {
        throw new InvalidCommandArgumentException("Cannot find fund with id " + this.parentFundId);
      }
      if (loadedInputs.Timebox == null)
      {
        throw new InvalidCommandArgumentException($"Cannot find timebox with id {this.timeboxId} for profile id {profileId}");
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