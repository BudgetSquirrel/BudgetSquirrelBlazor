CREATE PROCEDURE [EditBudget] (
  @FundId INT,
  @TimeboxId INT,
  @PlannedAmount DECIMAL(10, 2)
)
AS
BEGIN

UPDATE [dbo].[Budgets]
  SET [dbo].[Budgets].[PlannedAmount] = @PlannedAmount
WHERE [dbo].[Budgets].[FundId] = @FundId
  AND [dbo].[Budgets].[TimeboxId] = @TimeboxId;

END
