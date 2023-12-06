CREATE PROCEDURE [CreateTransactionAllocation] (
  @TransactionId NVARCHAR(36),
  @FundId INT,
  @TimeboxId INT,
  @Amount DECIMAL(18, 2)
)
AS
BEGIN

INSERT INTO TransactionAllocations (
  TransactionId,
  FundId,
  TimeboxId,
  Amount
)
VALUES (
  @TransactionId,
  @FundId,
  @TimeboxId,
  @Amount
);

END