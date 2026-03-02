CREATE procedure [dbo].[GetPhotoid]
(
@household_number varchar(380),
	@ReturnVal int output
)
as
Begin
Declare @ids as int
if((select count(*) from tbl_BeneficiaryServiceDet where household_number like @household_number and PhotoPath is null)=0)
Begin
set @ids=0
ENd
Else
Begin
set @ids=(
select intBenTempid from tbl_BeneficiaryServiceDet where household_number like @household_number
)
End


 set @ReturnVal =  @ids  

End