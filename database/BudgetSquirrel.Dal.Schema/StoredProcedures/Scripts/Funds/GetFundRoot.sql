CREATE PROCEDURE [GetFundRoot] (
  @FundRootId INT
)
AS
BEGIN

SELECT [dbo].[FundRoots].[Id] AS [FundRootId]
FROM [dbo].[FundRoots]
WHERE [dbo].[FundRoots].[Id] = @FundRootId;

END