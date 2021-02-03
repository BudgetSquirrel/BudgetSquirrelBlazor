using System.Threading.Tasks;
using BudgetSquirrel.Core.Accounts;

namespace BudgetSquirrel.Server.Biz.Accounts
{
  public class CreateAccountCommand : AbstractCommand<
    (string email,
      string password,
      string confirmPassword,
      string firstName,
      string lastName),
    LoginUser>
  {
    private IAccountRepository accountRepository;
    
    public CreateAccountCommand(
      IAccountRepository accountRepository,
      (string email,
      string password,
      string confirmPassword,
      string firstName,
      string lastName) args)
      : base(args)
    {
      this.accountRepository = accountRepository;
    }

    public override Task Execute(LoginUser duplicateUser)
    {
      return this.accountRepository.CreateUser(
        this.arguments.email,
        this.arguments.password,
        this.arguments.firstName,
        this.arguments.lastName);
    }

    public override Task<LoginUser> Load()
    {
      return this.accountRepository.GetByEmail(this.arguments.email);
    }

    public override Task<LoginUser> Validate(LoginUser duplicateUser)
    {
      if (duplicateUser != null)
      {
        throw new InvalidCommandOperationException("That user already exists");
      }
      if (this.arguments.password != this.arguments.confirmPassword)
      {
        throw new InvalidCommandArgumentException("Password must match confirmation password");
      }
      return Task.FromResult(duplicateUser);
    }
  }
}
