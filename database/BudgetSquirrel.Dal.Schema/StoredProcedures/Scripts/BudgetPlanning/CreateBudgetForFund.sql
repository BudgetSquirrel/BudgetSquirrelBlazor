CREATE PROCEDURE [CreateBudgetForFund] (
  @FundId INT,
  @PlannedAmount DECIMAL(10, 2)
)
AS
BEGIN

INSERT INTO [dbo].[Budget] ( [dbo].[Budget].[FundId], [dbo].[Budget].[PlannedAmount] ) VALUES ( @FundId, @PlannedAmount );

END
