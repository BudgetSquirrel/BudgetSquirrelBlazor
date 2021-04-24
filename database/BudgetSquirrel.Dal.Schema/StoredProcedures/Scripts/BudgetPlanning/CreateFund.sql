CREATE PROCEDURE [CreateFund] (
  @FundRootId INT,
  @ParentFundId INT = NULL,
  @Name NVARCHAR(45),
  @IsRoot BIT
)
AS
BEGIN

INSERT INTO [dbo].[Funds]
  ( [dbo].[Funds].[FundRootId], [dbo].[Funds].[ParentFundId], [dbo].[Funds].[Name], [dbo].[Funds].[IsRoot] )
  VALUES
  ( @FundRootId, @ParentFundId, @Name, @IsRoot );

SELECT CAST(SCOPE_IDENTITY() AS INT);
RETURN
END
