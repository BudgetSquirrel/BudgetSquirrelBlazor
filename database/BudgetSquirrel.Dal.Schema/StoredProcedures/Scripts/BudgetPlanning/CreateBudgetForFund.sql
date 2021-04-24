CREATE PROCEDURE [CreateBudgetForFund] (
  @FundId INT,
  @PlannedAmount DECIMAL(10, 2),
  @TimeboxId INT
)
AS
BEGIN

INSERT INTO [dbo].[Budgets] ( [dbo].[Budgets].[FundId], [dbo].[Budgets].[PlannedAmount], [dbo].[Budgets].[TimeboxId] ) VALUES ( @FundId, @PlannedAmount, @TimeboxId );

END
