
CREATE procedure [dbo].[VerificationofWageofBen]
(
@intBenid varchar(8),
@intWorkid varchar(8),
@FromDate datetime
)
As
Begin

create table #attend
(
intworkid int,
intBenid int,
Attendace datetime,
WageFlag varchar(8)
)
insert into #attend
select intWorkActivityId,intBenid,AttendanceDate,WageApplyFlag from tr_BenAttendanceDet where intBenid like @intBenid
select intBenid from  #attend  where intBenid like @intBenid and intworkid like @intWorkid and Convert(varchar(11), Attendace,106)=Convert(varchar(11), @FromDate,106) and WageFlag is not null
--select * from tr_BenWagesPayment where intBenid like @intBenid and intWorkid like @intWorkid and @FromDate between FromDate and ToDate
--select * from td_WorkWageRequest inner join td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid=td_WorkWageRequest.intWagePaymentReqid  where td_WageReqBenDet.intBenid like @intBenid and td_WageReqBenDet.intWorkid like @intWorkid and @FromDate between td_WorkWageRequest.ReqFromDate and td_WorkWageRequest.ReqToDate and td_WorkWageRequest.Status<>100


Drop table #attend
End

