CREATE PROCEDURE [DeleteTransaction] (
  @TransactionId NVARCHAR(36)
)
AS
BEGIN

DELETE FROM Transactions WHERE Id = @TransactionId;

END