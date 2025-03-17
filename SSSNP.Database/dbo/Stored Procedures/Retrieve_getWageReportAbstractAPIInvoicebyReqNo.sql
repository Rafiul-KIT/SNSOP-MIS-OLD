
CREATE procedure [dbo].[Retrieve_getWageReportAbstractAPIInvoicebyReqNo]
(   
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8)  ,
@ReqNo varchar(30),
@reportType varchar(10) -- "All", "DIS", "LIPW" (non-DIS)
)  
as  
begin  


select * 
from WageResultDetails_tblTemp 
where intStateId like @intStateid
and intip  like @IPid
and intCountyId like @intCountieid
and intPayamId like @intPayamid
and intBomaId like @intBomasid
and RequestNo like @reqno
and WorkCode like @workCode
	and
	(
	(@reportType = 'DIS' and (workcode='DIS - Direct Income Support' or workremarks like '%DIS%' ))
	or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support' and workremarks not like '%DIS%')
	or (@reportType <> 'DIS' and @reportType <> 'LIPW')
	)
order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo,FromDate, ToDate


 
SELECT  SUM(TotalMembers) AS TotalMembers, SUM(TotalWorkDays) AS TotalWorkDays, 
                         SUM(AmountPaidUSD) AS AmountPaidUSD,
						 SUM(AmountPaidSSP) AS AmountPaidSSP,
						 sum(ExchangeRate) as ExchangeRate
FROM           WageResultDetails_tblTemp
where intStateId like @intStateid
and intCountyId like @intCountieid
and intPayamId like @intPayamid
and intBomaId like @intBomasid
and
	(
	(@reportType = 'DIS' and workcode='DIS - Direct Income Support')
	or (@reportType = 'LIPW' and workcode<>'DIS - Direct Income Support')
	or (@reportType <> 'DIS' and @reportType <> 'LIPW')
	)
end