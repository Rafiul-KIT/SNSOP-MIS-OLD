CREATE procedure [dbo].[InsAttendanceDiffdays]
(
@intWorkid int,
@intBenid int,
@FromDate datetime
)
As 
Begin
Delete from tr_BenAttendanceDet where intBenid like @intBenid and intWorkActivityId like @intWorkid and COnvert(varchar(11), AttendanceDate ,106)=COnvert(varchar(11), @FromDate ,106)
insert into tr_BenAttendanceDet
(
intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt
)
values
(
@intWorkid,@intBenid,@FromDate,'Present','1000',@FromDate
)

select 1
return 1

End