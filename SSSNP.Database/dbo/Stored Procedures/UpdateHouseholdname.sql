CREATE procedure [dbo].[UpdateHouseholdname]
(
@household_number varchar(380),
@NameofHousehold varchar(80),
@Created_by varchar(8),
@ReturnVal int output
)
As
Begin
 Update tbl_BeneficiaryDet set NameofHousehold=@NameofHousehold,householdnameupdate='Y',householddate=getdate()   where household_number=@household_number
 Update tbl_BeneficiaryServiceDet set NameofHousehold=@NameofHousehold,householdnameupdate='Y',householddate=getdate()  where household_number=@household_number
 select 1000
    set @ReturnVal = 1000
 --return 1000

End