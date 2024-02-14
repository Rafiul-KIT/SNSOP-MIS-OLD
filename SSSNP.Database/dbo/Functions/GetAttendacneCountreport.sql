
CREATE function [dbo].[GetAttendacneCountreport] 
( @intWorkActivityId int,
@intBenid int,
@FromDate varchar(11),
@ToDate varchar(11)
)
RETURNS int
AS
BEGIN
	DECLARE @Count as int
	set @Count=ISNULL((select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet  where (tr_BenAttendanceDet.AttendanceDate between Convert(datetime, @FromDate) and Convert(datetime, @ToDate)+1) and tr_BenAttendanceDet.intWorkActivityId like @intWorkActivityId and tr_BenAttendanceDet.intBenid=@intBenid and tr_BenAttendanceDet.Status='Present'
and tr_BenAttendanceDet.PaymentFlag is  null),0)
return @Count

END