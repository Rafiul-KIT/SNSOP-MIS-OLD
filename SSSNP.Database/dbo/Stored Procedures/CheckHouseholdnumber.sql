CREATE procedure [dbo].[CheckHouseholdnumber]
(
@household_number varchar(380)
)
As
Begin

select household_number from tbl_BeneficiaryServiceDet where household_number like @household_number


End