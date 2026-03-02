
CREATE procedure [dbo].[getPaymentsFirstStep]  
(  
@intWagePaymentReqid varchar(8) 
)  
as  


begin
declare @firstPaym as datetime
declare @reqfromdate as datetime
declare @reqtodate as datetime

set @reqfromdate = (select reqfromdate from td_WorkWageRequest where intWagePaymentReqid=@intWagePaymentReqid )
set @reqtodate = (select reqtodate from td_WorkWageRequest where intWagePaymentReqid=@intWagePaymentReqid )

CREATE TABLE #AbsentAttendance 
(          
	[intbenid] int,          
	[intworkid] int,   
	[AbsentDays] int
)  

insert into #AbsentAttendance (intbenid, intworkid, Absentdays)

select intbenid, intworkactivityid, count(status) as [Absent] from tr_BenAttendanceDet
where intWageReqid = @intWagePaymentReqid
and status ='Absent'
and attendancedate between @reqfromdate  and @reqtodate
group by intbenid, intworkactivityid

set @firstPaym =(
select min(paiddate) 
from tr_BenWagesPayment
where uuid =@intWagePaymentReqid
group by uuid
)

select vw.*
,isnull(ab.AbsentDays,0) as AbsentDays
,vw.cnt-isnull(ab.AbsentDays,0) as PresentDays
from Vw_WageInvoiceNewLatest vw
left join #AbsentAttendance ab
on vw.intbenid=ab.intbenid
and vw.intworkid=ab.intworkid
where intWagePaymentReqid=@intWagePaymentReqid
and vw.intBenid in (
select intbenid from tr_BenWagesPayment 
where uuid = @intWagePaymentReqid
and DATEDIFF(day, @firstPaym,paiddate)<=30)
order by PayStatus asc

end