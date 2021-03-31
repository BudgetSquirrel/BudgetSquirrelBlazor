CREATE PROCEDURE [GetIsPasswordAttemptCorrect] (
  @Email NVARCHAR(75),
  @EncryptedPasswordAttempt NVARCHAR(255)
)
AS
BEGIN

SELECT [dbo].[Accounts].[Email] FROM [dbo].[Accounts]
  INNER JOIN [dbo].[Users]
    ON [dbo].[Users].[Id] = [dbo].[Accounts].[UserId]
  WHERE [dbo].[Accounts].[Email] = @Email AND
        [dbo].[Accounts].[Password] = @EncryptedPasswordAttempt

END
