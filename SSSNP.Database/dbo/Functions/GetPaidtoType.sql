
CREATE function [dbo].[GetPaidtoType] 
(
@householdnumber varchar(500),
@benid varchar(8)
)
RETURNS varchar(100)
AS
BEGIN
	
	DECLARE @name as varchar(100)

	Declare @cnts as int
	set @cnts= (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=@householdnumber)
	
	
set @name=	(select case when @householdnumber='' then 'Household' else  case when household_number = @householdnumber then 'Household'
	else case when Alternate_number = @householdnumber then 'Alternate' else case when Secondalternate_number = @householdnumber
	then 'Alternate' else case when  @cnts<>0 then 'Alternate'  else  'Paid Person details is not available in MIS due to alternative not registered.' end  end end end end
	 from tbl_BeneficiaryDet where intBenid like @benid)

	--set @Count=(Select count(tbl_Grievance.GrievanceId)  from tbl_Grievance where intBomasid=@IntBomaId)
	return @name

END