
CREATE PROCEDURE [dbo].[GetrptBeneficiaryFailureAPI] 

AS
BEGIN

  SELECT
    Household_Number,
    NameofHousehold,
    Failure_Message,
    Ipid,
    intBomaid,

	CONVERT(char(10), PullDate,126) as PullDate
   
  FROM Beneficiary_API_Failure_Beneficiary
  
END
