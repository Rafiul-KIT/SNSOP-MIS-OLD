CREATE function [dbo].[GetStatusNameByStatusCode] 
( @StatusCode varchar(100)
)
RETURNS varchar(100)
AS
BEGIN
	declare @Stat varchar(100)
	set @Stat=(select (case when @StatusCode is null then 'New'
	when @StatusCode='Co-Approve' then 'Coordinator Approved'
	when @StatusCode='Co-Reject' then 'Coordinator Rejected'
	when @StatusCode='Sp-Approve' then 'Specialist Approved'
	when @StatusCode='Sp-Reject' then 'Specialist Rejected'  
	when @StatusCode='Ma-Approve' then 'Manager Approved'
	when @StatusCode='Ma-Reject' then 'Manager Rejected'
	else '' end) as Status)
	return @Stat

END


