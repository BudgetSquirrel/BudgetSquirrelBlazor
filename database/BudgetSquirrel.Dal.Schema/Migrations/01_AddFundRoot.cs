using System.Data;
using FluentMigrator;

namespace BudgetSquirrel.Dal.Schema.Migrations
{
  [Migration(2, "Add FundRoot")]
  public class AddFundRoot : Migration
  {
    public override void Up()
    {
      Create.Table("FundRoots")
            .WithColumn("Id").AsInt64().Identity()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id").OnDelete(Rule.Cascade);
    }
    
    public override void Down()
    {
      Delete.Table("FundRoots");
    }
  }
}