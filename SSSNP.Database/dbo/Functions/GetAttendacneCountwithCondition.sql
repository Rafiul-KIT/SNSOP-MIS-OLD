
CREATE function [dbo].[GetAttendacneCountwithCondition] 
( @intWorkActivityId int,
@intBenid int,
@FromDate datetime,
@ToDate datetime
)
RETURNS int
AS
BEGIN
	DECLARE @Count as int
	set @Count=ISNULL((select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet  where (tr_BenAttendanceDet.AttendanceDate between @FromDate and @ToDate) and tr_BenAttendanceDet.intWorkActivityId like @intWorkActivityId and tr_BenAttendanceDet.intBenid=@intBenid and tr_BenAttendanceDet.Status='Present'
and tr_BenAttendanceDet.WageApplyFlag is null),0)

return @Count

END