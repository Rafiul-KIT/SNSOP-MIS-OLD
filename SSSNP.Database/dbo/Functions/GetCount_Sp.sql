
CREATE function [dbo].[GetCount_Sp] 
( @IntBomaId int
)
RETURNS varchar(100)
AS
BEGIN
	
	DECLARE @Count as varchar(100)
	set @Count=(Select count(tbl_Grievance.GrievanceId)  from tbl_Grievance where intBomasid=@IntBomaId)
	return @Count

END


