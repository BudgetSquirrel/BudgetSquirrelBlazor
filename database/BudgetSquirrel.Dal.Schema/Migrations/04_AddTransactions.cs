using System.Data;
using FluentMigrator;

namespace BudgetSquirrel.Dal.Schema.Migrations
{
  [Migration(5, "Add Transactions")]
  public class AddTransactions : Migration
  {
    public override void Up()
    {
      Create.Table("Transactions")
        .WithColumn("Id").AsGuid().PrimaryKey().WithDefaultValue(SystemMethods.NewGuid)
        .WithColumn("FundId").AsInt64()
                            .ForeignKey("Funds", "Id")
                            .OnDelete(Rule.Cascade)
                            .NotNullable()
        .WithColumn("VendorName").AsString().NotNullable()
        .WithColumn("Description").AsString().NotNullable()
        .WithColumn("Amount").AsDecimal().NotNullable()
        .WithColumn("DateOfTransaction").AsDate().NotNullable()
        .WithColumn("CheckNumber").AsString(20).NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Transactions");
    }
  }
}