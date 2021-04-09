CREATE PROCEDURE [CreateOverallBudgetForUser] (
  @Email NVARCHAR(75)
)
AS
BEGIN

DECLARE @UserId AS INT;
DECLARE @FundRootId AS INT;
DECLARE @FundId AS INT;

SELECT @UserId = [dbo].[Users].[Id]
  FROM [dbo].[Users]
  INNER JOIN [dbo].[Accounts]
    ON [dbo].[Accounts].[UserId] = [dbo].[Users].[Id]
  WHERE [dbo].[Accounts].[Email] = @Email;

INSERT INTO [dbo].[FundRoots] ( [dbo].[FundRoots].[UserId] ) VALUES ( @UserId );
SELECT @FundRootId = CAST(SCOPE_IDENTITY() AS INT);

INSERT INTO [dbo].[Funds] ( [dbo].[Funds].[FundRootId], [dbo].[Funds].[Name], [dbo].[Funds].[IsRoot] ) VALUES ( @FundRootId, 'ROOT_FUND', 1 );
SELECT @FundId = CAST(SCOPE_IDENTITY() AS INT);

INSERT INTO [dbo].[Budgets] ( [dbo].[Budgets].[FundId] ) VALUES ( @FundId );

END
