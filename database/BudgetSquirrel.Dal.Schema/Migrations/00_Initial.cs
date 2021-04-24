using System.Data;
using FluentMigrator;

namespace BudgetSquirrel.Dal.Schema.Migrations
{
  [Migration(1, "Initial Users Schema")]
  public class Initial : Migration
  {
    public override void Up()
    {
      Create.Table("Users")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("FirstName").AsString(45)
            .WithColumn("LastName").AsString(45);
      Create.Table("Accounts")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("Email").AsString(75).NotNullable()
            .WithColumn("Password").AsString(255).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id").OnDelete(Rule.Cascade);
    }

    public override void Down()
    {
      Delete.Table("Accounts");
      Delete.Table("Users");
    }
  }
}