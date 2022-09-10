using FluentMigrator;

namespace BudgetSquirrel.Dal.Schema.Migrations
{
  [Migration(4, "Add Budget IsFinalized")]
  public class AddBudgetIsFinalized : Migration
  {
    public override void Up()
    {
      Alter.Table("Budgets")
           .AddColumn("IsFinalized").AsBoolean().WithDefaultValue(false);
    }

    public override void Down()
    {
      Delete.Column("[Budgets].[IsFinalized]");
    }
  }
}