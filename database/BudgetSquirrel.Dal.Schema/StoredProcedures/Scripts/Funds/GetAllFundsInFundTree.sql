CREATE PROCEDURE [GetAllFundsInFundTree] (
  @ProfileId INT,
  @TimeboxId INT
)
AS
BEGIN

SELECT
  [dbo].[Funds].[Id],
  [dbo].[Funds].[Name],
  [dbo].[Funds].[Balance],
  [dbo].[Funds].[IsRoot],
  [dbo].[Funds].[ProfileId],
  [dbo].[Funds].[ParentFundId]
FROM [dbo].[Funds]
LEFT JOIN [dbo].[Budgets]
  ON [dbo].[Budgets].[FundId] = [dbo].[Funds].[Id]
  AND [dbo].[Budgets].[TimeboxId] = @TimeboxId
WHERE [dbo].[Funds].[ProfileId] = @ProfileId
AND [dbo].[Budgets].[Id] IS NOT NULL;

END