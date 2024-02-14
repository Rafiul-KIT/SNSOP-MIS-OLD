
CREATE procedure [dbo].[getWageReportAbstractAPIInvoicebyReqNo]
(  
@FromDate datetime,  
@ToDate datetime,  
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




CREATE TABLE #TempWorDetails          
(          
[IPName] varchar(40),          
[StateName] varchar(40),             
[CountieName] varchar(40),         
[PayamName] varchar(40), 
[BomaName] varchar(40),         
[WorkCode] varchar(80),
[intWorkid] varchar(8),
[intWagePaymentReqid] int,
[intStateId] int,
[intCountyId] int,
[intPayamId] int,
[intBomaId] int,
)  
---

if @reportType='DIS'
begin 
	INSERT INTO #TempWorDetails
	   SELECT DISTINCT 
                         ip.IPName, NULL,NULL, NULL, NULL, w.WorkCode,w.intWorkid,wwr.intWagePaymentReqid, NULL, NULL, NULL, NULL
FROM           
                         dbo.tbl_WorkMaster as w 
						 INNER JOIN dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
						inner join td_WorkWageRequest wwr on w.intWorkid=wwr.intWorkid
   
   where intIP like @IPid 
   and w.intStateid is null
   and w.intCountieid is null
   and w.intPayamid is null
   and w.intBomasid is null
   and w.WorkCode like @workCode

end
else if @reportType='LIPW'
begin
INSERT INTO #TempWorDetails
SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, w.WorkCode,w.intWorkid,wwr.intWagePaymentReqid,s.intStateid,c.intCountieid,p.intPayamid,b.intBomasid
FROM           
                         dbo.tbl_WorkMaster as w INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
						 inner join td_WorkWageRequest wwr on w.intWorkid=wwr.intWorkid
   
   where intIP like @IPid 
   and w.intStateid like @intStateid 
   and w.intCountieid like @intCountieid 
   and w.intPayamid like @intPayamid 
   and w.intBomasid like @intBomasid 
   and w.WorkCode like @workCode
end

else -- @reportType='All'
begin
INSERT INTO #TempWorDetails
SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, w.WorkCode,w.intWorkid,wwr.intWagePaymentReqid,s.intStateid,c.intCountieid,p.intPayamid,b.intBomasid
FROM           
                         dbo.tbl_WorkMaster as w INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
						 inner join td_WorkWageRequest wwr on w.intWorkid=wwr.intWorkid
   
   where intIP like @IPid 
   and w.intStateid like @intStateid 
   and w.intCountieid like @intCountieid 
   and w.intPayamid like @intPayamid 
   and w.intBomasid like @intBomasid 
   and w.WorkCode like @workCode
UNION
   SELECT DISTINCT 
                         ip.IPName, NULL,NULL, NULL, NULL, w.WorkCode,w.intWorkid,wwr.intWagePaymentReqid, NULL, NULL, NULL, NULL
FROM           
                         dbo.tbl_WorkMaster as w 
						 INNER JOIN dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
						 inner join td_WorkWageRequest wwr on w.intWorkid=wwr.intWorkid
   
   where intIP like @IPid 
   and w.intStateid is null
   and w.intCountieid is null
   and w.intPayamid is null
   and w.intBomasid is null
   and w.WorkCode like @workCode
end
---


CREATE TABLE #WageDetails          
(          
[intWorkid] varchar(8),          
[RequestNo] varchar(30),             
[FromDate] varchar(15),         
[ToDate] varchar(15), 
[TotalMembers] varchar(15),         
[TotalWorkDays] varchar(15),
[AmountPaidUSD] decimal(18,2),
[AmountPaidSSP] decimal(18,2),
[ExchangeRate] decimal(18,2),
[WageStatus] varchar(50),
[intWagePaymentReqid] varchar(8),
[PaymentCycle] varchar(50)
)  

INSERT INTO #WageDetails

SELECT DISTINCT 
    td_WorkWageRequest.intWorkid,     td_WorkWageRequest.ReqNo as RequestNo
	, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate
	, CONVERT(varchar(11),dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate
	, COUNT(DISTINCT dbo.td_WageReqBenDet.intBenid) AS TotalMembers
	, SUM(dbo.td_WageReqBenDet.TotAttendance) AS TotalWorkDays
	, SUM(dbo.td_WageReqBenDet.WageRateUSDTotal) AS AmountPaidUSD
	,SUM(dbo.td_WageReqBenDet.AmountInSSP) AS AmountPaidSSP
	,case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate
	, isnull(	(select aa.StageName from tm_WageStages as aa where aa.Stageid=td_WorkWageRequest.Status),'Contact Admin') as WageStatus
	, td_WorkWageRequest.intWagePaymentReqid
	,wpc.WagePaymentCycle
FROM            dbo.td_WorkWageRequest
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid 
inner join Tm_WagePaymentCycle wpc  on td_WorkWageRequest.PaymentCycle=wpc.intPaymentCycleID
where td_WorkWageRequest.ReqNo like @ReqNo
  
GROUP BY  td_WorkWageRequest.intWorkid,td_WorkWageRequest.ReqNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106), CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106),td_WorkWageRequest.Exchangerateattime,td_WorkWageRequest.Status,td_WorkWageRequest.intWagePaymentReqid,
						 wpc.WagePaymentCycle
						 						 						
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
[AmountPaidSSP] decimal(18,2),
[ExchangeRate] decimal(18,2),
[WageStatus] varchar(50),
[PaymentCycle] varchar(50) ,
[intStateId] int,
[intCountyId] int,
[intPayamId] int,
[intBomaId] int,
)  





INSERT INTO #WageResultDetails

select   
	b.intWagePaymentReqid
	, IPName
	--,StateName,Countiename,PayamName,BomaName
	, isnull(StateName, (select top 1 s.StateName from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_States s on s.intStateid=benf.intStateid
												inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid
											))
	,isnull(CountieName, (select top 1 c.CountieName from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_counties c on c.intCountieid=benf.intCountieid
												inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid
												))
	,isnull(PayamName, (select top 1 p.PayamName from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_payams p on p.intPayamid=benf.intPayamid
											inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid))
	,isnull(BomaName, (select top 1 b.BomaName from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_bomas b on b.intBomasid=benf.intBomasid
												inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid))
											
	
	, WorkCode
	,RequestNo
	, FromDate
	, ToDate
	, TotalMembers
	, TotalWorkDays
	, AmountPaidUSD
	,AmountPaidSSP
	, ExchangeRate
	,WageStatus 
	,PaymentCycle
	
	, isnull(intStateId, (select top 1 s.intStateId from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_States s on s.intStateid=benf.intStateid
												inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid
											))
	,isnull(intCountyId, (select top 1 c.intCountieId from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_counties c on c.intCountieid=benf.intCountieid
												inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid
												))
	,isnull(intPayamId, (select top 1 p.intPayamId from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_payams p on p.intPayamid=benf.intPayamid
											inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid))
	,isnull(intBomaId, (select top 1 b.intBomasId from td_WorkWageRequest wwr 
												inner join td_WageReqBenDet wrbd on wrbd.intWagePaymentReqid=wwr.intWagePaymentReqid
												inner join tbl_BeneficiaryDet benf on benf.intBenid=wrbd.intBenid
												inner join tm_bomas b on b.intBomasid=benf.intBomasid
												inner join #WageDetails on wwr.intWagePaymentReqid=a.intWagePaymentReqid))




from #TempWorDetails as a 
inner join #WageDetails as b on b.intWagePaymentReqid=a.intWagePaymentReqid
--inner join #WageDetails as b on a.intWorkid=b.intWorkid


select * 
from #WageResultDetails 
where intStateId like @intStateid
and intCountyId like @intCountieid
and intPayamId like @intPayamid
and intBomaId like @intBomasid
order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo,FromDate, ToDate


 
SELECT  SUM(TotalMembers) AS TotalMembers, SUM(TotalWorkDays) AS TotalWorkDays, 
                         SUM(AmountPaidUSD) AS AmountPaidUSD,
						 SUM(AmountPaidSSP) AS AmountPaidSSP,
						 sum(ExchangeRate) as ExchangeRate
FROM           #WageResultDetails
where intStateId like @intStateid
and intCountyId like @intCountieid
and intPayamId like @intPayamid
and intBomaId like @intBomasid
end