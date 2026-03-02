CREATE procedure [dbo].[DeleteAttendanceDiffdays]
(
@intWorkid int,
@intBenid int,
@FromDate datetime
)
As 
Begin
Delete from tr_BenAttendanceDet where intBenid like @intBenid and intWorkActivityId like @intWorkid and COnvert(varchar(11), AttendanceDate ,106)=COnvert(varchar(11), @FromDate ,106)


select 1
return 1

End