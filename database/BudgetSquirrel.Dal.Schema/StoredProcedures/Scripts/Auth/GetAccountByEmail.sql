CREATE PROCEDURE [GetAccountByEmail] (
  @Email NVARCHAR(75)
)
AS
BEGIN

SELECT * FROM [dbo].[Accounts] INNER JOIN [dbo].[Users] ON [dbo].[Users].[Id] = [dbo].[Accounts].[UserId] WHERE [dbo].[Accounts].[Email] = @Email

END
