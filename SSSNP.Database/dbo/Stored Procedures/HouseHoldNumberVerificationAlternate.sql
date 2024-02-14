
CREATE procedure [dbo].[HouseHoldNumberVerificationAlternate] --HouseHoldNumberVerification '75a8212b-f44d-4cdc-a32e-31fbb7481987'
(
@household_number varchar(200)
)
As
Begin

select intBenTempid as intBenid,household_number,EnrollmentNo,intBenTempid from tbl_BeneficiaryServiceDet where household_number like @household_number  and (Name1stAlternatePayee is null or Name2ndAlternatePayee is null)

select * from tbl_BeneficiaryServiceDet where household_number like @household_number and Name1stAlternatePayee is not null
select case when alternate_number is null and Secondalternate_number is null then '0' else case when alternate_number is not null and Secondalternate_number is  null then '1' else case when alternate_number is not null and Secondalternate_number is not null then '2' else '3' end end end as CountSt  from tbl_BeneficiaryServiceDet where household_number like @household_number

End

