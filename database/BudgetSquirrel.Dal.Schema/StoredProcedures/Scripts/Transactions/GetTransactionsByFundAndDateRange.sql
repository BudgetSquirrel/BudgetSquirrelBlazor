CREATE PROCEDURE [GetTransactionsByFundAndDateRange] (
  @FundId INT,
  @StartDate DATE, -- Inclusive
  @EndDate DATE -- Exclusive
)
AS
BEGIN

SELECT
  [dbo].[Transactions].[Id],
  [dbo].[Transactions].[FundId],
  [dbo].[Transactions].[VendorName],
  [dbo].[Transactions].[Description],
  [dbo].[Transactions].[Amount],
  [dbo].[Transactions].[DateOfTransaction],
  [dbo].[Transactions].[CheckNumber]
FROM [dbo].[Transactions]
WHERE [dbo].[Transactions].[FundId] = @FundId
  AND [dbo].[Transactions].[DateOfTransaction] >= @StartDate
  AND [dbo].[Transactions].[DateOfTransaction] <= @EndDate;

END