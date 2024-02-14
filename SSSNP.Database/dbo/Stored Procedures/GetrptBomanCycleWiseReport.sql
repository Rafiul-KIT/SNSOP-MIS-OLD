
CREATE PROCEDURE [dbo].[GetrptBomanCycleWiseReport] (@intIPid varchar(8),@Bomasid varchar(8))
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
intPayamid int,
BomaName varchar(180),
intBomasid int
)
insert into #tblWorksReqDet
select distinct tm_States.StateName,tm_States.intStateid, intWorkid,tm_Counties.intCountieid,
tm_Counties.CountieName,tm_Payams.PayamName,tm_Payams.intPayamid,
tm_Bomas.BomaName,tm_Bomas.intBomasid from tbl_WorkMaster inner join tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid 
inner join tm_Counties
on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid
where tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intBomasid like @Bomasid
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
FromDate datetime,
intBomasid int
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
td_WorkWageRequest.ReqFromDate,
tbl_WorkMaster.intBomasid
from td_WageReqBenDet inner join td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid=td_WageReqBenDet.intWagePaymentReqid
inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=td_WageReqBenDet.intWorkid
and tbl_WorkMaster.intWorkid=td_WorkWageRequest.intWorkid
where td_WorkWageRequest.Status in (4,5,6)
and tbl_WorkMaster.IPid like @intIPid
and tbl_WorkMaster.intBomasid like @Bomasid



select distinct
a.intBomasid,
BomaName,

Convert(varchar(11), b.FromDate ,106) as Fromdates,
isnull((select sum(p.WageRateUSDTotal) from #tblWageReq as p where p.intBomasid=a.intBomasid and Convert(varchar(11), p.FromDate ,106)=Convert(varchar(11), b.FromDate ,106)),0) as TotalAmount,
isnull((select count( p.intBenid) from #tblWageReq as p where p.intBomasid=a.intBomasid and Convert(varchar(11), p.FromDate ,106)=Convert(varchar(11), b.FromDate ,106)),0) as TotalBeneficiries





from 
#tblWorksReqDet as a inner join #tblWageReq as b on a.intBomasid=b.intBomasid








Drop table #tblWorksReqDet
Drop table #tblWageReq

END
