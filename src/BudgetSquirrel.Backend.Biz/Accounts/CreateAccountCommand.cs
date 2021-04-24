using System;
using System.Linq;
using System.Threading.Tasks;
using BudgetSquirrel.Backend.Biz.BudgetPlanning;
using BudgetSquirrel.Core.Accounts;
using BudgetSquirrel.Core.History;

namespace BudgetSquirrel.Backend.Biz.Accounts
{
  public class CreateAccountCommand : BasicCommand<
    (string email,
      string password,
      string confirmPassword,
      string firstName,
      string lastName),
    Account>
  {
    private IAccountRepository accountRepository;
    private IBudgetRepository budgetRepository;
    private ITimeboxRepository timeboxRepository;
    
    public CreateAccountCommand(
      IAccountRepository accountRepository,
      IBudgetRepository budgetRepository,
      ITimeboxRepository timeboxRepository,
      (string email,
      string password,
      string confirmPassword,
      string firstName,
      string lastName) args)
      : base(args)
    {
      this.accountRepository = accountRepository;
      this.budgetRepository = budgetRepository;
      this.timeboxRepository = timeboxRepository;
    }

    public override async Task Execute(Account duplicateUser)
    {
      await this.accountRepository.CreateUser(
        this.arguments.email,
        this.arguments.password,
        this.arguments.firstName,
        this.arguments.lastName);

      int fundRootId = await this.budgetRepository.CreateFundRootForUser(this.arguments.email);
      int rootFundId = await this.budgetRepository.CreateFund(fundRootId, null, "ROOT_FUND", true);
      await this.budgetRepository.CreateBudgetForFund(rootFundId, 0);
      await this.timeboxRepository.CreateTimebox(fundRootId, DateTime.Now, DateTime.Now.AddDays(30));
    }

    public override Task<Account> Load()
    {
      return this.accountRepository.GetByEmail(this.arguments.email);
    }

    public override Task<Account> Validate(Account duplicateUser)
    {
      if (duplicateUser != null)
      {
        throw new InvalidCommandOperationException("That user already exists");
      }
      if (this.arguments.password != this.arguments.confirmPassword)
      {
        throw new InvalidCommandArgumentException("Password must match confirmation password");
      }
      // Must contain one and only one '@' and it must not be the first or last character.
      bool isInvalidEmailFormat = !this.arguments.email.Contains("@") ||
                                  this.arguments.email.Where(c => c == '@').Count() != 1 ||
                                  this.arguments.email.EndsWith("@") ||
                                  this.arguments.email.StartsWith("@");
      if (isInvalidEmailFormat)
      {
        throw new InvalidCommandArgumentException("That email is not a valid email address");
      }
      return Task.FromResult(duplicateUser);
    }
  }
}
