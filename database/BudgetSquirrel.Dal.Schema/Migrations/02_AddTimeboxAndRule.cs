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
            .WithColumn("FundRootId").AsInt64()
                                  .ForeignKey("FundRoots", "Id")
                                  .OnDelete(Rule.Cascade)
                                  .NotNullable()
            .WithColumn("StartDate").AsDate().NotNullable()
            .WithColumn("EndDate").AsDate().NotNullable();
      
      Create.Table("TimeboxRule")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("FundRootId").AsInt64()
                                    .ForeignKey("FundRoots", "Id")
                                    .OnDelete(Rule.Cascade)
                                    .NotNullable()
                                    .Unique()
            .WithColumn("EndDayOfMonth").AsInt16().NotNullable()
            .WithColumn("ShouldRolloverOnShortMonths").AsBoolean().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Timebox");
      Delete.Table("TimeboxRule");
    }
  }
}