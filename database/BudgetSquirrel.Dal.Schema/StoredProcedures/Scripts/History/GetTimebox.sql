CREATE PROCEDURE [GetTimebox] (
  @TimeboxId INT
)
AS
BEGIN

SELECT [dbo].[Timebox].[StartDate], [dbo].[Timebox].[EndDate]
FROM [dbo].[Timebox]
WHERE [dbo].[Timebox].[Id] = @TimeboxId;

END