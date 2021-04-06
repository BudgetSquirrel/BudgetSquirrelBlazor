using System.Threading.Tasks;

namespace BudgetSquirrel.Backend.Biz
{
  public abstract class AbstractCommand<TInputs, TLoadedInputs>
  {
    protected TInputs arguments;

    public AbstractCommand(TInputs args)
    {
      this.arguments = args;
    }

    public abstract Task Execute(TLoadedInputs loadedInputs);
    public abstract Task<TLoadedInputs> Load();
    public abstract Task<TLoadedInputs> Validate(TLoadedInputs loadedInputs);
  }
}