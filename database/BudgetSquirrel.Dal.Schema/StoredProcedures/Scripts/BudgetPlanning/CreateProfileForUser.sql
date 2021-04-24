CREATE PROCEDURE [CreateProfileForUser] (
  @Email NVARCHAR(75)
)
AS
BEGIN

DECLARE @UserId AS INT;

SELECT @UserId = [dbo].[Users].[Id]
  FROM [dbo].[Users]
  INNER JOIN [dbo].[Accounts]
    ON [dbo].[Accounts].[UserId] = [dbo].[Users].[Id]
  WHERE [dbo].[Accounts].[Email] = @Email;

INSERT INTO [dbo].[Profiles] ( [dbo].[Profiles].[UserId] ) VALUES ( @UserId );

SELECT CAST(SCOPE_IDENTITY() AS INT);
RETURN
END
