

CREATE procedure [dbo].[getWageReportAbstractAPIInvoicebyReqNowithDates] -- '05/23/2016','05/30/2016',1015
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

if(@FromDate<>'' and @ToDate<>'')
Begin
INSERT INTO #WageDetails

SELECT DISTINCT 
                  td_WorkWageRequest.intWorkid,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate, COUNT(DISTINCT dbo.td_WageReqBenDet.intBenid) AS TotalMembers, SUM(dbo.td_WageReqBenDet.TotAttendance) AS TotalWorkDays, 
                         SUM(dbo.td_WageReqBenDet.WageRateUSDTotal) AS AmountPaidUSD,
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,

				isnull(	(select aa.StageName from tm_WageStages as aa where aa.Stageid=td_WorkWageRequest.Status),'Contact Admin') as WageStatus, td_WorkWageRequest.intWagePaymentReqid
FROM            dbo.td_WorkWageRequest 
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid 
where td_WorkWageRequest.ReqNo like @ReqNo
  and CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106)=CONVERT(varchar(11),@FromDate, 106) and  CONVERT(varchar(11),  dbo.td_WorkWageRequest.ReqToDate, 106)= CONVERT(varchar(11),  @ToDate, 106)
GROUP BY  td_WorkWageRequest.intWorkid,td_WorkWageRequest.ReqNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106), CONVERT(varchar(11),  dbo.td_WorkWageRequest.ReqToDate, 106),td_WorkWageRequest.Exchangerateattime,td_WorkWageRequest.Status,td_WorkWageRequest.intWagePaymentReqid


End
Else
Begin
INSERT INTO #WageDetails

SELECT DISTINCT 
                  td_WorkWageRequest.intWorkid,     td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate, COUNT(DISTINCT dbo.td_WageReqBenDet.intBenid) AS TotalMembers, SUM(dbo.td_WageReqBenDet.TotAttendance) AS TotalWorkDays, 
                         SUM(dbo.td_WageReqBenDet.WageRateUSDTotal) AS AmountPaidUSD,
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,

				isnull(	(select aa.StageName from tm_WageStages as aa where aa.Stageid=td_WorkWageRequest.Status),'Contact Admin') as WageStatus, td_WorkWageRequest.intWagePaymentReqid
FROM            dbo.td_WorkWageRequest 
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid 
where td_WorkWageRequest.ReqNo like @ReqNo
  
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
[AmountPaidUSD] decimal(18,2),
[ExchangeRate] decimal(18,2),
[WageStatus] varchar(50)
     
)  

INSERT INTO #WageResultDetails


select   intWagePaymentReqid, IPName,StateName, CountieName, PayamName, BomaName, WorkCode,RequestNo, FromDate, ToDate, TotalMembers, TotalWorkDays, 
                       AmountPaidUSD,ExchangeRate,WageStatus from #TempWorDetails as a inner join #WageDetails as b on a.intWorkid=b.intWorkid




select * from #WageResultDetails order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo,FromDate, ToDate
  
--select      IPName,StateName, CountieName, PayamName, BomaName, WorkCode,RequestNo, FromDate, ToDate, TotalMembers, TotalWorkDays, 
--                         AmountPaidUSD,ExchangeRate,WageStatus from Vw_WageAbstractAPIInvoice
--where intIP like @IPid and intStateid like @intStateid and intCountieid like @intCountieid and intPayamid like @intPayamid and intBomasid like @intBomasid 

--order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo,FromDate, ToDate


SELECT  SUM(TotalMembers) AS TotalMembers, SUM(TotalWorkDays) AS TotalWorkDays, 
                         SUM(AmountPaidUSD) AS AmountPaidUSD
FROM           #WageResultDetails
      




end




