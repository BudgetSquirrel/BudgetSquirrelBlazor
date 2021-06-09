CREATE PROCEDURE [UpdateFundDetails] (
  @FundId INT,
  @Name NVARCHAR(45)
)
AS
BEGIN

UPDATE [dbo].[Funds]
  SET [dbo].[Funds].[Name] = @Name
WHERE [dbo].[Funds].[Id] = @FundId;

END
