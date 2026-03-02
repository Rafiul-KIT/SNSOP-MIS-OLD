
CREATE PROCEDURE [dbo].[GetrptCountieWiseReport] (@intIPid varchar(8),@Stateid varchar(8))
AS
BEGIN
  create table #tblWorksReqDet
(
StateName varchar(180),
intStateid int,
intWorkid int,
intCountieid int,
CountieName varchar(180)
)
insert into #tblWorksReqDet
select distinct tm_States.StateName,tm_States.intStateid, intWorkid,tm_Counties.intCountieid,
tm_Counties.CountieName from tbl_WorkMaster inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid 
inner join tm_Counties
on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid

where tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intStateid like @Stateid
and tbl_WorkMaster.intWorkid in (select a.intWorkid from td_WorkWageRequest as a where a.Status in (4,5,6)) 


create table #tblWageReq
(
intWorkid int,
intWagePaymentReqid int,
intWagesReqBenTransid int,
intBenid int,
TotAttendance int,
WageRateUSDTotal decimal(18,2),
intStateid int,
intCountieid int
)
insert into #tblWageReq
select 
td_WorkWageRequest.intWorkid,
td_WorkWageRequest.intWagePaymentReqid,
td_WageReqBenDet.intWagesReqBenTransid,
td_WageReqBenDet.intBenid,
td_WageReqBenDet.TotAttendance,
td_WageReqBenDet.WageRateUSDTotal,
tbl_WorkMaster.intStateid,
tbl_WorkMaster.intCountieid
from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid
inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid

and tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid
where td_WorkWageRequest.Status in (4,5,6)
and tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intStateid like @Stateid



create table #tblWageReqPaid
(
intWorkid int,
intWagePaymentReqid int,
intWagesReqBenTransid int,
intBenid int,
TotAttendance int,
WageRateUSDTotal decimal(18,2),
intStateid int,
intCountieid int
)
insert into #tblWageReqPaid
select 
td_WorkWageRequest.intWorkid,
td_WorkWageRequest.intWagePaymentReqid,
td_WageReqBenDet.intWagesReqBenTransid,
td_WageReqBenDet.intBenid,
tr_BenWagesPayment.AttendanceDays,
tr_BenWagesPayment.PaidAmountUSD,
tbl_WorkMaster.intStateid,
tbl_WorkMaster.intCountieid
from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid
inner join tr_BenWagesPayment on tr_BenWagesPayment.uuid=td_WorkWageRequest.intWagePaymentReqid
and tr_BenWagesPayment.trans_id=td_WageReqBenDet.intWagesReqBenTransid
and tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid
inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid
and  tbl_WorkMaster.intWorkid=tr_BenWagesPayment.intWorkid
and tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid
where td_WorkWageRequest.Status in (4,5,6)
and tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intStateid like @Stateid

select distinct
intCountieid,
CountieName,
(select sum(p.WageRateUSDTotal) from #tblWageReqPaid as p where p.intCountieid=a.intCountieid) as TotalPaidAmount,
isnull((select sum(p.WageRateUSDTotal) from #tblWageReq as p where p.intCountieid=a.intCountieid),0) - isnull((select sum(p.WageRateUSDTotal) from #tblWageReqPaid as p where p.intCountieid=a.intCountieid),0)  as TotaUnpaidAmount,
(select sum(p.WageRateUSDTotal) from #tblWageReq as p where p.intCountieid=a.intCountieid) as TotalAmount,
(select count(p.intBenid) from #tblWageReqPaid as p where p.intCountieid=a.intCountieid) as TotalPaidBeneficiries,
isnull((select count(p.intBenid) from #tblWageReq as p where p.intCountieid=a.intCountieid),0)- isnull((select count(p.intBenid) from #tblWageReqPaid as p where p.intCountieid=a.intCountieid),0) as TotalUnpaidBeneficiries,
(select count(p.intBenid) from #tblWageReq as p where p.intCountieid=a.intCountieid) as TotalBeneficiries,
(select sum(p.TotAttendance) from #tblWageReqPaid as p where p.intCountieid=a.intCountieid) as TotalPaidAttendance,
isnull((select sum(p.TotAttendance) from #tblWageReq as p where p.intCountieid=a.intCountieid),0)-isnull((select sum(p.TotAttendance) from #tblWageReqPaid as p where p.intCountieid=a.intCountieid),0) as TotalUnpaidAttendance,
(select sum(p.TotAttendance) from #tblWageReq as p where p.intCountieid=a.intCountieid) as TotalAttendance

from 
#tblWorksReqDet as a






Drop table #tblWorksReqDet
Drop table #tblWageReq
Drop table #tblWageReqPaid
END
