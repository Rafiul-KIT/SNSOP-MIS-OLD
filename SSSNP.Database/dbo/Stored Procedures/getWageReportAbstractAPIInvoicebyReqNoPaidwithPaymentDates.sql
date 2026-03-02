

CREATE procedure [dbo].[getWageReportAbstractAPIInvoicebyReqNoPaidwithPaymentDates] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8)  ,
@ReqNo varchar(30)
)  
as  
begin  
  
 -- Developer : Srikanth
-- Date : 14-03-2018
-- Purpose: Attendance Report New Format 






CREATE TABLE #TempWorDetails          
(          
[IPName] varchar(40),          
[StateName] varchar(40),             
[CountieName] varchar(40),         
[PayamName] varchar(40), 
[BomaName] varchar(40),         
[WorkCode] varchar(80),
[intWorkid] varchar(8)
     
)  

INSERT INTO #TempWorDetails

SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, w.WorkCode,w.intWorkid
FROM           
                         dbo.tbl_WorkMaster as w INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
   
   where intIP like @IPid and w.intStateid like @intStateid and w.intCountieid like @intCountieid and w.intPayamid like @intPayamid and w.intBomasid like @intBomasid 

   and w.WorkCode like @workCode





CREATE TABLE #WageDetails          
(          
[intWorkid] varchar(8),          
[RequestNo] varchar(30),             
[FromDate] varchar(15),         
[ToDate] varchar(15), 
[TotalMembers] varchar(15),         
[TotalWorkDays] varchar(15),
[AmountPaidUSD] decimal(18,2),
[ExchangeRate] decimal(18,2),
[WageStatus] varchar(50),
[intWagePaymentReqid] varchar(8)
     
)  

INSERT INTO #WageDetails

SELECT DISTINCT 
                  td_WorkWageRequest.intWorkid,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate, COUNT(DISTINCT dbo.td_WageReqBenDet.intBenid) AS TotalMembers, SUM(dbo.td_WageReqBenDet.TotAttendance) AS TotalWorkDays, 
                         SUM(dbo.td_WageReqBenDet.WageRateUSDTotal) AS AmountPaidUSD,
						 
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,

				'Paid' as WageStatus, td_WorkWageRequest.intWagePaymentReqid
			
FROM            dbo.td_WorkWageRequest 
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid 

where td_WorkWageRequest.ReqNo like @ReqNo
and  td_WorkWageRequest.Status in ('5','6')
GROUP BY  td_WorkWageRequest.intWorkid,td_WorkWageRequest.ReqNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106), CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106),td_WorkWageRequest.Exchangerateattime,td_WorkWageRequest.Status,td_WorkWageRequest.intWagePaymentReqid

						


CREATE TABLE #WageDetailsPaid          
(          
[intWorkid] varchar(8),          
[RequestNo] varchar(30),             
[FromDate] varchar(15),         
[ToDate] varchar(15), 
[TotalMembers] varchar(15),         
[TotalWorkDays] varchar(15),
[AmountPaidUSD] decimal(18,2),
[ExchangeRate] decimal(18,2),
[WageStatus] varchar(50),
[intWagePaymentReqid] varchar(8)
     
)  
if(@FromDate<>'' and @ToDate<>'')
Begin
INSERT INTO #WageDetailsPaid

SELECT DISTINCT 
                  td_WorkWageRequest.intWorkid,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate, COUNT(DISTINCT dbo.tr_BenWagesPayment.intBenid) AS TotalMembers, SUM(dbo.tr_BenWagesPayment.AttendanceDays) AS TotalWorkDays, 
                         SUM(dbo.tr_BenWagesPayment.PaidAmountUSD) AS AmountPaidUSD,
						 
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,

				'Paid' as WageStatus, td_WorkWageRequest.intWagePaymentReqid
FROM            dbo.td_WorkWageRequest   
inner join tr_BenWagesPayment on tr_BenWagesPayment.uuid=td_WorkWageRequest.intWagePaymentReqid
where td_WorkWageRequest.ReqNo like @ReqNo
and  td_WorkWageRequest.Status in ('5','6') and tr_BenWagesPayment.uuid is not null
and tr_BenWagesPayment.PaidDate between @FromDate and @ToDate+1

  
GROUP BY  td_WorkWageRequest.intWorkid,td_WorkWageRequest.ReqNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106), CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106),td_WorkWageRequest.Exchangerateattime,td_WorkWageRequest.Status,td_WorkWageRequest.intWagePaymentReqid

End
else
Begin
INSERT INTO #WageDetailsPaid

SELECT DISTINCT 
                  td_WorkWageRequest.intWorkid,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate, COUNT(DISTINCT dbo.tr_BenWagesPayment.intBenid) AS TotalMembers, SUM(dbo.tr_BenWagesPayment.AttendanceDays) AS TotalWorkDays, 
                         SUM(dbo.tr_BenWagesPayment.PaidAmountUSD) AS AmountPaidUSD,
						 
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,

				'Paid' as WageStatus, td_WorkWageRequest.intWagePaymentReqid
FROM            dbo.td_WorkWageRequest   
inner join tr_BenWagesPayment on tr_BenWagesPayment.uuid=td_WorkWageRequest.intWagePaymentReqid
where td_WorkWageRequest.ReqNo like @ReqNo
and  td_WorkWageRequest.Status in ('5','6') and tr_BenWagesPayment.uuid is not null


  
GROUP BY  td_WorkWageRequest.intWorkid,td_WorkWageRequest.ReqNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106), CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106),td_WorkWageRequest.Exchangerateattime,td_WorkWageRequest.Status,td_WorkWageRequest.intWagePaymentReqid

End						


CREATE TABLE #WageResultDetails          
(          
 intWagePaymentReqid varchar(50),
[IPName] varchar(40),          
[StateName] varchar(40),             
[CountieName] varchar(40),          
[PayamName] varchar(40),  
[BomaName] varchar(40),         
[WorkCode] varchar(140), 
 [RequestNo] varchar(30), 
 [FromDate] varchar(15), 
 [ToDate] varchar(15), 
  [TotalMembers] decimal(18,0), 
[TotalWorkDays] decimal(18,0), 
[TotalAmount] decimal(18,2),
[ExchangeRate] decimal(18,2),
[WageStatus] varchar(50),
TotalMembersPaid  decimal(18,0), 
AmountPaidUSD  decimal(18,2)     
)  

INSERT INTO #WageResultDetails


select   b.intWagePaymentReqid, IPName,StateName, CountieName, PayamName, BomaName, WorkCode,b.RequestNo, b.FromDate, b.ToDate, b.TotalMembers, b.TotalWorkDays, 
                       b.AmountPaidUSD as TotalAmount,b.ExchangeRate,b.WageStatus,p.TotalMembers as TotalMembersPaid,p.AmountPaidUSD as PaidAmountusdnew  from #TempWorDetails as a inner join #WageDetails as b on a.intWorkid=b.intWorkid
					   inner join #WageDetailsPaid as p on p.intWorkid=a.intWorkid and p.intWorkid=b.intWorkid and p.intWagePaymentReqid=b.intWagePaymentReqid




select * from #WageResultDetails order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo,FromDate, ToDate
  
--select      IPName,StateName, CountieName, PayamName, BomaName, WorkCode,RequestNo, FromDate, ToDate, TotalMembers, TotalWorkDays, 
--                         AmountPaidUSD,ExchangeRate,WageStatus from Vw_WageAbstractAPIInvoice
--where intIP like @IPid and intStateid like @intStateid and intCountieid like @intCountieid and intPayamid like @intPayamid and intBomasid like @intBomasid 

--order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo,FromDate, ToDate


SELECT  SUM(TotalMembers) AS TotalMembers, SUM(TotalWorkDays) AS TotalWorkDays, 
                         SUM(TotalAmount) AS AmountPaidUSD,SUM(TotalMembersPaid) AS TotalMembersPaid
						 ,SUM(AmountPaidUSD) AS TotalMembersPaidMain,SUM(TotalMembers)-SUM(TotalMembersPaid) as TotalMembersUnPaid
						 ,SUM(TotalAmount)-SUM(AmountPaidUSD) as TotalAmountUnPaid
FROM           #WageResultDetails



      truncate table #WageDetailsPaid      
	  truncate table #WageResultDetails
	  truncate table #WageDetails
	  truncate table #TempWorDetails




end




