CREATE PROCEDURE [CreateBudgetForFund] (
  @FundId INT,
  @PlannedAmount DECIMAL(10, 2)
)
AS
BEGIN

INSERT INTO [dbo].[Budgets] ( [dbo].[Budgets].[FundId], [dbo].[Budgets].[PlannedAmount] ) VALUES ( @FundId, @PlannedAmount );

END
