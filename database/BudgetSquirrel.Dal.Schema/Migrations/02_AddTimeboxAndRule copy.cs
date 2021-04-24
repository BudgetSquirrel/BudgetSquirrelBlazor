using System.Data;
using FluentMigrator;

namespace BudgetSquirrel.Dal.Schema.Migrations
{
  [Migration(3, "Add Timebox and Timebox Rule")]
  public class AddTimeboxAndRule : Migration
  {
    public override void Up()
    {
      Create.Table("Timebox")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("ProfileId").AsInt64()
                                  .ForeignKey("Profiles", "Id")
                                  .OnDelete(Rule.Cascade)
                                  .NotNullable()
            .WithColumn("StartDate").AsDate().NotNullable()
            .WithColumn("EndDate").AsDate().NotNullable();
      
      Create.Table("TimeboxRule")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("ProfileId").AsInt64()
                                    .ForeignKey("Profiles", "Id")
                                    .OnDelete(Rule.Cascade)
                                    .NotNullable()
                                    .Unique()
            .WithColumn("EndDayOfMonth").AsInt16().NotNullable()
            .WithColumn("ShouldRolloverOnShortMonths").AsBoolean().NotNullable();

      Alter.Table("Budgets")
           .AddColumn("TimeboxId").AsInt64().ForeignKey("Timebox", "Id").OnDelete(Rule.None).NotNullable();
    }

    public override void Down()
    {
      Delete.Column("[Budgets].[TimeboxId]");
      Delete.Table("Timebox");
      Delete.Table("TimeboxRule");
    }
  }
}