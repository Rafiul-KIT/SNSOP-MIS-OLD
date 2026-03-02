
CREATE PROCEDURE [dbo].[GetrptPaymentFailure] 
(@workid nvarchar(15), @householdNo nvarchar(500), @Date datetime, @reqid  nvarchar(15))
AS
BEGIN
if(@Date <>'')
Begin
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
WHERE CONVERT(varchar(11), PullDate, 106) = CONVERT(varchar(11), @Date, 106)
AND Beneficiary_API_Failure_Payments.household_number LIKE @householdNo
AND work_id LIKE @workid
AND uuid LIKE @reqid
End
Else
Begin
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
WHERE  Beneficiary_API_Failure_Payments.household_number LIKE @householdNo
AND work_id LIKE @workid
AND uuid LIKE @reqid
End
END
