CREATE PROCEDURE [CreateAccount] (
  @FirstName NVARCHAR(45),
  @LastName NVARCHAR(45),
  @Email NVARCHAR(75),
  @Password NVARCHAR(255)
)
AS
BEGIN

DECLARE @UserId INT;

INSERT INTO [dbo].[Users] (
  [dbo].[Users].[FirstName],
  [dbo].[Users].[LastName]
) VALUES (
  @FirstName,
  @LastName
);
SELECT @UserId = CAST(SCOPE_IDENTITY() as int);

INSERT INTO [dbo].[Accounts] (
  [dbo].[Accounts].[Email],
  [dbo].[Accounts].[Password],
  [dbo].[Accounts].[UserId]
) VALUES (
  @Email,
  @Password,
  @UserId
);

END