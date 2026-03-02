
CREATE procedure [dbo].[VerificationofWageofBenCheckWages]
(
@intBenid varchar(8),
@intWorkid varchar(8),
@FromDate datetime
)
As
Begin

select * from tr_BenAttendanceDet where intBenid like @intBenid and intWorkActivityId like @intWorkid and AttendanceDate=@FromDate and WageApplyFlag is not null
select * from tr_BenWagesPayment where intBenid like @intBenid and intWorkid like @intWorkid and @FromDate between FromDate and ToDate
--select * from td_WorkWageRequest inner join td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid=td_WorkWageRequest.intWagePaymentReqid  where td_WageReqBenDet.intBenid like @intBenid and td_WageReqBenDet.intWorkid like @intWorkid and @FromDate between td_WorkWageRequest.ReqFromDate and td_WorkWageRequest.ReqToDate and td_WorkWageRequest.Status<>100
End