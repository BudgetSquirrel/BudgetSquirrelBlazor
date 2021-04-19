CREATE PROCEDURE [GetAllFundsInFundTree] (
  @FundRootId INT
)
AS
BEGIN

SELECT
  [dbo].[Funds].[Id],
  [dbo].[Funds].[Name],
  [dbo].[Funds].[Balance],
  [dbo].[Funds].[IsRoot],
  [dbo].[Funds].[FundRootId],
  [dbo].[Funds].[ParentFundId]
FROM [dbo].[Funds]
WHERE [dbo].[Funds].[FundRootId] = @FundRootId;

END