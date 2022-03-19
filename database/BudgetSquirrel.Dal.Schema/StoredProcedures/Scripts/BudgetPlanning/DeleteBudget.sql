CREATE PROCEDURE [DeleteBudget] (
  @FundId INT,
  @TimeboxId INT
)
AS
BEGIN

DELETE FROM [dbo].[Budgets]
WHERE [dbo].[Budgets].[FundId] = @FundId
  AND [dbo].[Budgets].[TimeboxId] = @TimeboxId;

END
