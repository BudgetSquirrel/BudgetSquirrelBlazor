CREATE PROCEDURE [GetProfile] (
  @ProfileId INT
)
AS
BEGIN

SELECT [dbo].[Profiles].[Id] AS [ProfileId]
FROM [dbo].[Profiles]
WHERE [dbo].[Profiles].[Id] = @ProfileId;

END