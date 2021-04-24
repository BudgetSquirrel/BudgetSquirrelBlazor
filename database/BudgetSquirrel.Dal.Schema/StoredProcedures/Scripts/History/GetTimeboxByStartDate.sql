CREATE PROCEDURE [GetTimeboxByStartDate] (
  @ProfileId INT,
  @StartDate DATE
)
AS
BEGIN

SELECT
  [dbo].[Timebox].[Id],
  [dbo].[Timebox].[StartDate],
  [dbo].[Timebox].[EndDate]
FROM [dbo].[Timebox]
WHERE [dbo].[Timebox].[ProfileId] = @ProfileId
  AND [dbo].[Timebox].[StartDate] = @StartDate;

END