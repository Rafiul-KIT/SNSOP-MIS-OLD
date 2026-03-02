
CREATE PROCEDURE [dbo].[GetrptPayamWiseReport] (@intIPid varchar(8),@Countieid varchar(8))
AS
BEGIN
create table #tblWorksReqDet
(
StateName varchar(180),
intStateid int,
intWorkid int,
intCountieid int,
CountieName varchar(180),
PayamName varchar(180),
intPayamid int
)
insert into #tblWorksReqDet
select distinct tm_States.StateName,tm_States.intStateid, intWorkid,tm_Counties.intCountieid,
tm_Counties.CountieName,tm_Payams.PayamName,tm_Payams.intPayamid from tbl_WorkMaster inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid 
inner join tm_Counties
on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
where tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intCountieid like @Countieid
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
intCountieid int,
intPayamid int,
FromDate datetime
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
tbl_WorkMaster.intCountieid,
tbl_WorkMaster.intPayamid,
td_WorkWageRequest.ReqFromDate
from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid
inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid

and tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid
where td_WorkWageRequest.Status in (4,5,6)
and tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intCountieid like @Countieid



select distinct
intPayamid,
PayamName,
(select count(distinct p.FromDate) from #tblWageReq as p where p.intPayamid=a.intPayamid) as NoOfBatches,
(select sum(p.WageRateUSDTotal) from #tblWageReq as p where p.intPayamid=a.intPayamid) as TotalAmount,

(select count(p.intBenid) from #tblWageReq as p where p.intPayamid=a.intPayamid) as TotalBeneficiries


from 
#tblWorksReqDet as a






Drop table #tblWorksReqDet
Drop table #tblWageReq


END
