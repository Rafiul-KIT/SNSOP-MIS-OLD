CREATE procedure [dbo].[GetWagesdashboarddrildownDet]
(
@intOfficerid varchar(8),
@OfficerLevel varchar(20),
@ReqStatus varchar(8)
)
AS
Begin
if(@OfficerLevel='Cordinator')
Begin
if(@ReqStatus='1')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
End
else if(@ReqStatus='2')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='1'
End
else if(@ReqStatus='3')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='2'
End
else if(@ReqStatus='4')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='100'
End
End
else if(@OfficerLevel='Specialist')
Begin
if(@ReqStatus='1')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
End
else if(@ReqStatus='2')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='2'
End
else if(@ReqStatus='3')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='3'
End
else if(@ReqStatus='4')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='101'
End
End
else if(@OfficerLevel='Manager')
Begin
if(@ReqStatus='1')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
End
else if(@ReqStatus='2')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='3'
End
else if(@ReqStatus='3')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='4'
End
else if(@ReqStatus='4')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='102'
End
End
else if(@OfficerLevel='PA')
Begin
if(@ReqStatus='1')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
End
else if(@ReqStatus='2')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='4'
End
else if(@ReqStatus='3')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='5'
End
else if(@ReqStatus='4')
Begin
select distinct td_WorkWageRequest.*,tbl_WorkMaster.WorkCode,td_IPDetails.IPName,Convert(varchar(11),td_WorkWageRequest.Created_dt,106) as RDates ,
Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFrom,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTo 
,0 as AttCounter
from td_WorkWageRequest inner join tbl_WorkMaster on td_WorkWageRequest.intWorkid=tbl_WorkMaster.intWorkid inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid where td_WorkWageRequest.Status='102'
End
End

End

