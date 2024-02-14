
CREATE function [dbo].[GetPaidto] 
( @householdnumber varchar(500),
@benid varchar(8)
)
RETURNS varchar(100)
AS
BEGIN
	
	DECLARE @name as varchar(100)


	
set @name=	(select case when household_number = @householdnumber then NameofHousehold
	else case when Alternate_number = @householdnumber then Name1stAlternatePayee else case when Secondalternate_number = @householdnumber
	then Name2ndAlternatePayee else NameofHousehold end end end
	 from tbl_BeneficiaryDet where intBenid like @benid)

	--set @Count=(Select count(tbl_Grievance.GrievanceId)  from tbl_Grievance where intBomasid=@IntBomaId)
	return @name

END