
CREATE PROCEDURE [dbo].[GetrptBeneficiaryFailure] 
(@intIPid varchar(8), @householdNo nvarchar(500),@NameofHousehold varchar(150), @Date datetime)
AS
BEGIN

  SELECT
    Household_Number,
    NameofHousehold,
    Failure_Message,
    Ipid,
    intBomaid,
   CONVERT(varchar(11), PullDate, 106) as PullDate
  FROM Beneficiary_API_Failure_Beneficiary
  WHERE CONVERT(varchar(11), PullDate, 106) = CONVERT(varchar(11), @Date, 106)
  AND household_number LIKE @householdNo
  AND Ipid LIKE @intIPid
  AND NameofHousehold LIKE @NameofHousehold
END
