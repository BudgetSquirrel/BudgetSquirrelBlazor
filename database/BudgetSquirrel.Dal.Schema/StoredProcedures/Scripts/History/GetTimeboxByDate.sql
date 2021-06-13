CREATE PROCEDURE [GetTimeboxByDate] (
  @ProfileId INT,
  @Date DATE
)
AS
BEGIN

SELECT
  [dbo].[Timebox].[Id],
  [dbo].[Timebox].[StartDate],
  [dbo].[Timebox].[EndDate]
FROM [dbo].[Timebox]
WHERE [dbo].[Timebox].[ProfileId] = @ProfileId
  AND [dbo].[Timebox].[StartDate] <= @Date
  AND [dbo].[Timebox].[EndDate] >= @Date;

END