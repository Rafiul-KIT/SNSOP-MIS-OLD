
CREATE procedure [dbo].[getCheckingWagesApplyrestrictionbyID]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(8),
@intBenid varchar(8)
)  
as  
begin  
  select * from td_WorkWageRequest where intWorkid like @intWorkActivityId and Convert(varchar(11),ReqFromDate,103)= Convert(varchar(11),@FromDate,103) and  Convert(varchar(11),ReqToDate,103)= Convert(varchar(11),@ToDate,103) and intWagePaymentReqid in
  (
  select a.intWagePaymentReqid from td_WageReqBenDet as a where a.intBenid like @intBenid and a.intWorkid like @intWorkActivityId
  )

   select intAttendanceid from tr_BenAttendanceDet where  intBenid like @intBenid and WageApplyFlag is not null and AttendanceDate between @FromDate and @ToDate+1
end


