
CREATE FUNCTION [dbo].[getAttendnaceDaysNew]
(
	@intworkid varchar(8),
@intBenid varchar(8),
@fromdate datetime,
@todate datetime
)
RETURNS decimal(18,2)
AS
BEGIN

Declare @Attendancedays as int
	set @Attendancedays = (select count(*) from tr_BenAttendanceDetTemp where  intWorkActivityId like @intworkid and intBenid like @intBenid and AttendanceDate between @fromdate and @todate+1)
	
	RETURN @Attendancedays

END

