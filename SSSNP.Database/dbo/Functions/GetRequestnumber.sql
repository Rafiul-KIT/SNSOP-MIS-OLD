
CREATE function [dbo].[GetRequestnumber] 
( 
@Reqid varchar(8)
)
RETURNS varchar(100)
AS
BEGIN
	
	DECLARE @name as varchar(100)


	
set @name=	(select ReqNo
	 from td_WorkWageRequest where intWagePaymentReqid like @Reqid)

	
	return @name

END