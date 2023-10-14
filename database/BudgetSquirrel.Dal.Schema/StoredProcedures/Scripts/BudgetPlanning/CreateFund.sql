CREATE PROCEDURE [CreateFund] (
  @ProfileId INT,
  @ParentFundId INT = NULL,
  @Name NVARCHAR(45),
  @IsRoot BIT
)
AS
BEGIN

INSERT INTO [dbo].[Funds]
  ( [dbo].[Funds].[ProfileId], [dbo].[Funds].[ParentFundId], [dbo].[Funds].[Name], [dbo].[Funds].[IsRoot] )
  VALUES
  ( @ProfileId, @ParentFundId, @Name, @IsRoot );

-- Get the id of the fund we just created
SELECT CAST(SCOPE_IDENTITY() AS INT);
RETURN
END
