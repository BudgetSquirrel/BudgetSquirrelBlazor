using System.Data;
using FluentMigrator;

namespace BudgetSquirrel.Dal.Schema.Migrations
{
  [Migration(2, "Add Profile, Fund and Budget")]
  public class AddProfileFundAndBudget : Migration
  {
    public override void Up()
    {
      Create.Table("Profiles")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id").OnDelete(Rule.Cascade);

      Create.Table("Funds")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("ParentFundId").AsInt64().Nullable().ForeignKey("Funds", "Id").OnDelete(Rule.None)
            .WithColumn("ProfileId").AsInt64().NotNullable().ForeignKey("Profiles", "Id").OnDelete(Rule.Cascade)
            .WithColumn("Balance").AsDecimal(10, 2).NotNullable().WithDefaultValue(0)
            .WithColumn("IsRoot").AsBoolean().NotNullable().WithDefaultValue(false);

      Create.Table("Budgets")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("FundId").AsInt64().NotNullable().ForeignKey("Funds", "Id").OnDelete(Rule.Cascade)
            .WithColumn("PlannedAmount").AsDecimal(10, 2).NotNullable().WithDefaultValue(0);
    }
    
    public override void Down()
    {
      Delete.Table("Budgets");
      Delete.Table("Funds");
      Delete.Table("Profiles");
    }
  }
}