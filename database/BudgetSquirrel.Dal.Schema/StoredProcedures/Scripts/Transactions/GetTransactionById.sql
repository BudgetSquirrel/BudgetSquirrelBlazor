CREATE PROCEDURE [GetTransactionById] (
  @TransactionId NVARCHAR(36)
)
AS
BEGIN

SELECT
  [dbo].[Transactions].[Id],
  [dbo].[TransactionAllocations].[FundId],
  [dbo].[Transactions].[VendorName],
  [dbo].[Transactions].[Description],
  [dbo].[Transactions].[Amount],
  [dbo].[Transactions].[DateOfTransaction],
  [dbo].[Transactions].[CheckNumber]
FROM [dbo].[Transactions]
LEFT OUTER JOIN [dbo].[TransactionAllocations]
  ON [dbo].[TransactionAllocations].[TransactionId] = [dbo].[Transactions].[Id]
WHERE [dbo].[Transactions].[Id] = @TransactionId;

END