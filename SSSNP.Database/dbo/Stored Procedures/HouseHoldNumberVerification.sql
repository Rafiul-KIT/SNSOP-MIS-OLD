
CREATE procedure [dbo].[HouseHoldNumberVerification] --HouseHoldNumberVerification '75a8212b-f44d-4cdc-a32e-31fbb7481987'
(
@household_number varchar(200)
)
As
Begin

select intBenTempid as intBenid,household_number,EnrollmentNo,intBenTempid from tbl_BeneficiaryServiceDet where household_number like @household_number -- and (Name1stAlternatePayee is null or Name2ndAlternatePayee is null)

select * from tbl_BeneficiaryServiceDet where household_number like @household_number and Name1stAlternatePayee is not null

End

