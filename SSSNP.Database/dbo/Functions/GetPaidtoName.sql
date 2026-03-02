
CREATE function [dbo].[GetPaidtoName] 
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
	
	Declare @alname as int
	set @alname= (select top 1 alternate_name from tbl_BenfMorethanAlternateDet where alternate_number=@householdnumber)
	

	
set @name=	(select case when @householdnumber='' then 'Before Biometric' else  case when household_number = @householdnumber then NameofHousehold
	else case when Alternate_number = @householdnumber then Name1stAlternatePayee else case when Secondalternate_number = @householdnumber
	then Name2ndAlternatePayee else case when @cnts <>0 then @alname else  'Paid Person details is not available in MIS due to alternative not registered.' end  end end end end
	 from tbl_BeneficiaryDet where intBenid like @benid)

	--set @Count=(Select count(tbl_Grievance.GrievanceId)  from tbl_Grievance where intBomasid=@IntBomaId)
	return @name

END