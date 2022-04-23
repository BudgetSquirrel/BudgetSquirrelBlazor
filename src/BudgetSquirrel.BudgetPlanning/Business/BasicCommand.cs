using System.Threading.Tasks;

namespace BudgetSquirrel.BudgetPlanning.Business
{
  public abstract class BasicCommand<TInputs, TLoadedInputs> : ICommand<TLoadedInputs>
  {
    protected TInputs arguments;

    public BasicCommand(TInputs args)
    {
      this.arguments = args;
    }

    public abstract Task Execute(TLoadedInputs loadedInputs);
    public abstract Task<TLoadedInputs> Load();
    public abstract Task<TLoadedInputs> Validate(TLoadedInputs loadedInputs);
  }
}