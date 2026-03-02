
Create PROCEDURE [dbo].[GetrptBeneficiaryFailureAlt] 
(@AltName nvarchar(150),@AltNo nvarchar(500), @householdNo nvarchar(500), @Date datetime)
AS
BEGIN

  SELECT
    Household_Number,
    name,
    Failure_Message,
    Ipid, 
   CONVERT(varchar(11), PullDate, 106) as PullDate
  FROM Beneficiary_API_Failure_Alternate
  WHERE CONVERT(varchar(11), PullDate, 106) = CONVERT(varchar(11), @Date, 106)
  AND household_number LIKE @householdNo
  AND alternate_number LIKE @AltNo
  AND name LIKE @AltName
END
