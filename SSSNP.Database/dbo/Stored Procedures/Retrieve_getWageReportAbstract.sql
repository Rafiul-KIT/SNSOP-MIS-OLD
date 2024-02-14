
CREATE procedure [dbo].[Retrieve_getWageReportAbstract] -- '05/23/2016','05/30/2016',1015
(   
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8),
@reportType varchar(10) -- "All", "DIS", "LIPW" (non-DIS)
)  
as  
begin  




select 
IPName
,StateName
,CountieName
,PayamName
,BomaName
,WorkCode
,FromDate
,ToDate
,TotalMembers
,TotalWorkDays
,AmountPaidUSD
,AmountPaidSSP
,PaymentDate
,PaymentCycle

from wagePaidAbstract_TblTemp
where intStateid like @intStateid
and  intCountieid like @intCountieid
and intPayamId like @intPayamid
and  intBomasId like @intBomasid
	and
	(
	(@reportType = 'DIS' and workcode='DIS - Direct Income Support')
	or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support')
	or (@reportType <> 'DIS' and @reportType <> 'LIPW')
	)
order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, FromDate, ToDate

select    sum(distinct TotalMembers) as TotalMembers,sum(TotalWorkDays) as TotalWorkDays, sum(AmountPaidUSD)  as AmountPaidUSD, sum(AmountPaidSSP)  as AmountPaidSSP  
from wagePaidAbstract_TblTemp
where intStateid like @intStateid
and  intCountieid like @intCountieid
and intPayamId like @intPayamid
and  intBomasId like @intBomasid
and
	(
	(@reportType = 'DIS' and workcode='DIS - Direct Income Support')
	or (@reportType = 'LIPW' and workcode <> 'DIS - Direct Income Support')
	or (@reportType <> 'DIS' and @reportType <> 'LIPW')
	)
end