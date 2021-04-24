CREATE PROCEDURE [CreateTimebox] (
  @ProfileId INT,
  @StartDate DATE,
  @EndDate DATE
)
AS
BEGIN

INSERT INTO [dbo].[Timebox]
  ( [dbo].[Timebox].[ProfileId], [dbo].[Timebox].[StartDate], [dbo].[Timebox].[EndDate] )
  VALUES
  ( @ProfileId, @StartDate, @EndDate );

END
