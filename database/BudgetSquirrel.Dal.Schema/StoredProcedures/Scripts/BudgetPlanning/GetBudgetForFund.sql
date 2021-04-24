CREATE PROCEDURE [GetBudgetForFund] (
  @FundId INT
)
AS
BEGIN

SELECT [dbo].[Budgets].[PlannedAmount] FROM [dbo].[Budgets] WHERE [dbo].[Budgets].[FundId] = @FundId;

END
