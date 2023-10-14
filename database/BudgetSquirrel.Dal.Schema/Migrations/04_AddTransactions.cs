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
        .WithColumn("VendorName").AsString(100).NotNullable()
        .WithColumn("Description").AsString(200).NotNullable()
        .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
        .WithColumn("DateOfTransaction").AsDate().NotNullable().Indexed()
        .WithColumn("CheckNumber").AsString(20).NotNullable();

      Create.Table("TransactionAllocations")
        .WithColumn("FundId").AsInt64()
                            .ForeignKey("Funds", "Id")
                            .OnDelete(Rule.Cascade)
                            .NotNullable()
                            .Indexed()
                            .PrimaryKey()
        .WithColumn("TransactionId").AsGuid()
                                    .ForeignKey("Transactions", "Id")
                                    .OnDelete(Rule.Cascade)
                                    .NotNullable()
                                    .Indexed()
                                    .PrimaryKey()
        .WithColumn("TimeboxId").AsInt64()
                                .ForeignKey("Timebox", "Id")
                                .OnDelete(Rule.None)
                                .NotNullable()
                                .Indexed()
                                .PrimaryKey()
        .WithColumn("Amount").AsDecimal(18, 2).NotNullable();
    }

    public override void Down()
    {
      Delete.Table("Transactions");
      Delete.Table("TransactionAllocations");
    }
  }
}