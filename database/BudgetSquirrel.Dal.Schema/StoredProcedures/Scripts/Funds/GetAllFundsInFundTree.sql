CREATE PROCEDURE [GetAllFundsInFundTree] (
  @ProfileId INT
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
WHERE [dbo].[Funds].[ProfileId] = @ProfileId;

END