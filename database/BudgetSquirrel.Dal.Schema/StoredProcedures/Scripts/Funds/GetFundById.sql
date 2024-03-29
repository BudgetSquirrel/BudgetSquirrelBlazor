CREATE PROCEDURE [GetFundById] (
  @FundId INT
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
WHERE [dbo].[Funds].[Id] = @FundId;

END
