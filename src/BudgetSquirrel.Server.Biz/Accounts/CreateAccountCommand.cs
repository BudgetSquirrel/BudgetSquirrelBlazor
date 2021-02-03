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

    public override Task Execute()
    {
      return this.accountRepository.CreateUser(
        this.arguments.email,
        this.arguments.password,
        this.arguments.firstName,
        this.arguments.lastName);
    }

    protected override Task<LoginUser> Loaded()
    {
      throw new System.NotImplementedException();
    }

    protected override Task Validate()
    {
      throw new System.NotImplementedException();
    }
  }
}
