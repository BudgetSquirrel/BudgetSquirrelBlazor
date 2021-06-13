CREATE PROCEDURE [GetBudgetForFund] (
  @FundId INT,
  @TimeboxId INT
)
AS
BEGIN

SELECT
  [dbo].[Budgets].[PlannedAmount]
FROM [dbo].[Budgets]
WHERE [dbo].[Budgets].[FundId] = @FundId
  AND [dbo].[Budgets].[TimeboxId] = @TimeboxId;

END
