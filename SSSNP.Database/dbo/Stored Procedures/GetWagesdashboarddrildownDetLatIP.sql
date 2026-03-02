CREATE procedure [dbo].[GetWagesdashboarddrildownDetLatIP]
(
@intOfficerid varchar(8),
@OfficerLevel varchar(20),
@ReqStatus varchar(8),
@intIPid varchar(8),
@intStateid varchar(8),
@intCountyid varchar(8),
@intProjectid varchar(8),
@intTypeofworkid varchar(8),
@intPayamid varchar(8),
@intBomaid varchar(8),
@workcode varchar(80),
@wagereqno varchar(30)
)
AS
Begin

if(@ReqStatus='1')
Begin
select distinct 
td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequest 
inner join tbl_WorkMaster 
on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails 
on td_IPDetails.intIP=tbl_WorkMaster.IPid
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
where
tbl_WorkMaster.IPid like @intIPid 
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno 
and td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid ) )  order by td_WorkWageRequest.ReqNo asc
End
else if(@ReqStatus='2')
Begin
select distinct 
td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequest 
inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno
and

  td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid
and Status=1))   order by td_WorkWageRequest.ReqNo asc
End
else if(@ReqStatus='3')
Begin
select distinct 
td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequest 
inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno
and


 td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid
and Status=2 ))   order by td_WorkWageRequest.ReqNo asc
End
else if(@ReqStatus='4')
Begin
select distinct 
td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequest 
inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno 


and td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid
and Status=3 ))   order by td_WorkWageRequest.ReqNo asc
End
else if(@ReqStatus='5')
Begin
select distinct td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
, boma.BomaName as BomaName
from 
td_WorkWageRequest 
inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
left join tm_bomas boma  on boma.intBomasid=tbl_WorkMaster.intBomasid
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno 


and td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid
and (Status=100 or Status=101 or Status=102 or Status=103) ))   order by td_WorkWageRequest.ReqNo asc
End
else if(@ReqStatus='6')
Begin
select distinct td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequest 
inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno 


and td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid
and Status=4 ))   order by td_WorkWageRequest.ReqNo asc
End
else if(@ReqStatus='6')
Begin
select distinct 
td_WorkWageRequestPA.*
,'' as PaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequestPA.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequestPA.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequestPA.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequestPA 
inner join tbl_WorkMaster on td_WorkWageRequestPA.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequestPA.ReqNo like @wagereqno 


and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid from td_WorkWageRequestPA where intCoOrdinatorid like @intOfficerid ))   order by td_WorkWageRequestPA.ReqNo asc
End
else if(@ReqStatus='6')
Begin
select distinct 
td_WorkWageRequestPA.*
,'' as PaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequestPA.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequestPA.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequestPA.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequestPA inner join tbl_WorkMaster on td_WorkWageRequestPA.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequestPA.ReqNo like @wagereqno 


and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid from td_WorkWageRequestPA where intCoOrdinatorid like @intOfficerid
and Status=6))   order by td_WorkWageRequestPA.ReqNo asc
End

else if(@ReqStatus='7')
Begin
select distinct 
td_WorkWageRequestPA.*
,'' as PaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequestPA.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequestPA.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequestPA.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequestPA inner join tbl_WorkMaster on td_WorkWageRequestPA.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequestPA.ReqNo like @wagereqno 


and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid from td_WorkWageRequestPA where intCoOrdinatorid like @intOfficerid
and Status>=7
and (Status<>1000
and Status<>1001
and Status<>1002
and Status<>1003
and Status<>104) ))   order by td_WorkWageRequestPA.ReqNo asc
End
else if(@ReqStatus='8')
Begin
select distinct 
td_WorkWageRequestPA.*
,'' as PaymentCycle
,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequestPA.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequestPA.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequestPA.ReqToDate,106) as ReqTo 
from 
td_WorkWageRequestPA inner join tbl_WorkMaster on td_WorkWageRequestPA.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequestPA.ReqNo like @wagereqno 
and td_WorkWageRequestPA.intWagePaymentReqPAid in ((select intWagePaymentReqPAid from td_WorkWageRequestPA where intCoOrdinatorid like @intOfficerid
and Status=1000) )   order by td_WorkWageRequestPA.ReqNo asc
End

else if(@ReqStatus='30')
Begin
select distinct
td_WorkWageRequest.*
,wpc.WagePaymentCycle
,tbl_WorkMaster.WorkCode
,td_IPDetails.IPName
,Convert(varchar(11)
,td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11)
,td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11)
,td_WorkWageRequest.ReqToDate,106) as ReqTo 
, boma.BomaName as BomaName
from td_WorkWageRequest 
inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid 
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid 
left join tm_wagepaymentcycle wpc on td_WorkWageRequest.PaymentCycle= wpc.intPaymentCycleID
left join tm_bomas boma  on boma.intBomasid=tbl_WorkMaster.intBomasid
where
tbl_WorkMaster.IPid like @intIPid
and (tbl_WorkMaster.intStateid like @intStateid or tbl_WorkMaster.intStateid  is null)
and (tbl_WorkMaster.intCountieid like @intCountyid or tbl_WorkMaster.intCountieid  is null)
and (tbl_WorkMaster.intPayamid like @intPayamid or tbl_WorkMaster.intPayamid  is null)
 and tbl_WorkMaster.intPrjid like @intProjectid
and (tbl_WorkMaster.intBomasid like @intBomaid or tbl_WorkMaster.intBomasid  is null)
and tbl_WorkMaster.WorkCode like @workcode
and td_WorkWageRequest.ReqNo like @wagereqno
and


 td_WorkWageRequest.intWagePaymentReqid in ((select intWagePaymentReqid from td_WorkWageRequest where Created_by like @intOfficerid
and Status=100))   order by td_WorkWageRequest.ReqNo asc

End
End