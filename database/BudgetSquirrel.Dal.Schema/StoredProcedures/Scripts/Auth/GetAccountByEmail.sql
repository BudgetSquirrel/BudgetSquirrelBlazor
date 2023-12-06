CREATE PROCEDURE [GetAccountByEmail] (
  @Email NVARCHAR(75)
)
AS
BEGIN

SELECT
    [dbo].[Accounts].[Email],
    [dbo].[Users].[FirstName],
    [dbo].[Users].[LastName],
    [dbo].[Profiles].[Id] AS ProfileId
    FROM [dbo].[Accounts]
    INNER JOIN [dbo].[Users]
        ON [dbo].[Users].[Id] = [dbo].[Accounts].[UserId]
    INNER JOIN [dbo].[Profiles]
        ON [dbo].[Profiles].[UserId] = [dbo].[Users].[Id]
    WHERE [dbo].[Accounts].[Email] = @Email

END
