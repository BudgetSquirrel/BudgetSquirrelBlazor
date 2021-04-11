CREATE PROCEDURE [CreateFund] (
  @FundRootId INT,
  @ParentFundId INT,
  @Name NVARCHAR(45),
  @IsRoot BIT
)
AS
BEGIN

DECLARE @FundId INT;

INSERT INTO [dbo].[Funds]
  ( [dbo].[Funds].[FundRootId], [dbo].[Funds].[ParentFundId], [dbo].[Funds].[Name], [dbo].[Funds].[IsRoot] )
  VALUES
  ( @FundRootId, @Name, @ParentFundId, @IsRoot );

SELECT @FundId = CAST(SCOPE_IDENTITY() AS INT);
RETURN
END
