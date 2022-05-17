CREATE PROCEDURE [SetBudgetIsFinalized] (
  @ProfileId INT,
  @TimeboxId INT,
  @IsFinalized BIT
)
AS
BEGIN

UPDATE [dbo].[Budgets]
  SET [dbo].[Budgets].[IsFinalized] = @IsFinalized
  FROM [dbo].[Funds] INNER JOIN [dbo].[Budgets]
    ON [dbo].[Budgets].[FundId] = [dbo].[Funds].[Id]
    WHERE [dbo].[Funds].[ProfileId] = @ProfileId
    AND [dbo].[Budgets].[TimeboxId] = @TimeboxId;

END
