CREATE PROCEDURE [CreateTransaction] (
  @TransactionId NVARCHAR(36),
  @VendorName NVARCHAR(100),
  @Description NVARCHAR(200),
  @Amount DECIMAL(18, 2),
  @DateOfTransaction DATE,
  @CheckNumber NVARCHAR(20)
)
AS
BEGIN

INSERT INTO Transactions (
  Id,
  VendorName,
  Description,
  Amount,
  DateOfTransaction,
  CheckNumber
)
VALUES (
  @TransactionId,
  @VendorName,
  @Description,
  @Amount,
  @DateOfTransaction,
  @CheckNumber
);

END