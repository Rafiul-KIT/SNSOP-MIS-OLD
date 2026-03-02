


CREATE function [dbo].[GetWageManagerSenton_LOC] 
( @Requestid varchar(8),
@ids int
)
RETURNS varchar(11)
AS
BEGIN
	DECLARE @Count as varchar(11)
	set @Count=(select Convert(varchar(11), MAX(Created_dt),106) from td_WageReqTrans_LOC where intWagePaymentReqid like @Requestid and Status like '4')
return @Count

END