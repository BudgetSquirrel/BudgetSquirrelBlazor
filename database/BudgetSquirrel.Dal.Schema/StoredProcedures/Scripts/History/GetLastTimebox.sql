CREATE PROCEDURE [GetLastTimebox] (
  @ProfileId INT
)
AS
BEGIN

SELECT
  TOP 1
  [dbo].[Timebox].[Id],
  [dbo].[Timebox].[StartDate],
  [dbo].[Timebox].[EndDate]
FROM [dbo].[Timebox]
WHERE [dbo].[Timebox].[ProfileId] = @ProfileId
ORDER BY [dbo].[Timebox].[StartDate] DESC;

END