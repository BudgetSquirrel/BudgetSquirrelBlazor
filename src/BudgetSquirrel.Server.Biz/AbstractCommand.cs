using System.Threading.Tasks;

namespace BudgetSquirrel.Server.Biz
{
  public abstract class AbstractCommand<TInputs, TLoadedInputs>
  {
    protected TInputs arguments;

    public AbstractCommand(TInputs args)
    {
      this.arguments = args;
    }

    public abstract Task Execute();
    protected abstract Task<TLoadedInputs> Loaded();
    protected abstract Task Validate();
  }
}