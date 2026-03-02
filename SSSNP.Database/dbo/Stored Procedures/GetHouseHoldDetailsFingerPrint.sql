create procedure GetHouseHoldDetailsFingerPrint
AS
Begin
select  a.household_number from
(
select household_number,intBenTempid from tbl_BeneficiaryServiceDet where household_number is not null
union
select member_number as household_number,intBenTempid from tbl_BeneficiaryServiceDet where member_number is not null
union
select alternate_number as household_number,intBenTempid from tbl_BeneficiaryServiceDet where alternate_number is not null
)
as a
order by intBenTempid asc
End