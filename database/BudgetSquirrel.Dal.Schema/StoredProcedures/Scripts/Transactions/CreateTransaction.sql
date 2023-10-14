CREATE PROCEDURE [CreateTransaction] (
  @VendorName NVARCHAR(100),
  @Description NVARCHAR(200),
  @Amount DECIMAL(18, 2),
  @DateOfTransaction DATE,
  @CheckNumber NVARCHAR(20)
)
AS
BEGIN

INSERT INTO Transactions (
  VendorName,
  Description,
  Amount,
  DateOfTransaction,
  CheckNumber
)
VALUES (
  @VendorName,
  @Description,
  @Amount,
  @DateOfTransaction,
  @CheckNumber
);

-- Get the id of the transaction we just created
SELECT CAST(SCOPE_IDENTITY() AS NVARCHAR(36));
RETURN
END