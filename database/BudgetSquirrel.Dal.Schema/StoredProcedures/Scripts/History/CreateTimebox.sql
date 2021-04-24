CREATE PROCEDURE [CreateTimebox] (
  @FundRootId INT,
  @StartDate DATE,
  @EndDate DATE
)
AS
BEGIN

INSERT INTO [dbo].[Timebox]
  ( [dbo].[Timebox].[FundRootId], [dbo].[Timebox].[StartDate], [dbo].[Timebox].[EndDate] )
  VALUES
  ( @FundRootId, @StartDate, @EndDate );

END
