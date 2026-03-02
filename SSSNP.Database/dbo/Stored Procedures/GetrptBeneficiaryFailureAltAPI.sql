
CREATE PROCEDURE [dbo].[GetrptBeneficiaryFailureAltAPI] 

AS
BEGIN

  SELECT
    Household_Number,
    name,
    Failure_Message,
    Ipid, 
  CONVERT(char(10), PullDate,126) as PullDate
  FROM Beneficiary_API_Failure_Alternate
 
END
