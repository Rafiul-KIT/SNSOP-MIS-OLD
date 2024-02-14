CREATE procedure [dbo].[UpdateCashPlusActivitiesLIPW]
(
@intWorkActivityId varchar(8),
@intBenId varchar(8),
@attendanceDate datetime,
@FLI varchar(1),
@ECD varchar(1),
@WASH varchar(1)
)
AS
Begin


Update tr_BenAttendanceDet set cashP_FLI=@FLI,cashP_ECD=@ECD, cashP_WASH=@WASH
where intWorkActivityId=@intWorkActivityId and intBenid=@intBenId and AttendanceDate=@attendanceDate

End