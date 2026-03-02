
CREATE PROCEDURE [dbo].[GetrptPaymentFailureAPI] 

AS
BEGIN
SELECT
  Beneficiary_API_Failure_Payments.Household_Number,
  a.EnrollmentNo,
  a.NameofHousehold,
  work_id,
  trans_id,
  uuid,
  Failure_Message,
  CONVERT(varchar(11), PullDate, 106) as PullDate,
  Beneficiary_API_Failure_Payments.paid_to
FROM Beneficiary_API_Failure_Payments
left outer join tbl_BeneficiaryDet as a on a.Household_Number=Beneficiary_API_Failure_Payments.Household_Number

END
