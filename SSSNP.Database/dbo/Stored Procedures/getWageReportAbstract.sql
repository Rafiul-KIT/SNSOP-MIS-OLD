
CREATE procedure [dbo].[getWageReportAbstract] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
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

CREATE TABLE #TempWorDetails          
(          
[IPName] varchar(40),          
[StateName] varchar(40),             
[CountieName] varchar(40),         
[PayamName] varchar(40), 
[BomaName] varchar(40),         
[WorkCode] varchar(80),
[intWorkid] int,
[PaymentCycle] varchar(50),
 [intWagePaymentReqid] int,
[intStateId] int,
[intCountyId] int,
[intPayamId] int,
[intBomaId] int,
)  

if @reportType='DIS'
begin
INSERT INTO #TempWorDetails
   SELECT DISTINCT 
                         ip.IPName, NULL,NULL, NULL, NULL, w.WorkCode,w.intWorkid, wpc.wagepaymentcycle as PaymentCycle,wwr.intWagePaymentReqid, NULL, NULL, NULL, NULL
FROM           
                         dbo.tbl_WorkMaster as w with (nolock)
						 INNER JOIN dbo.td_IPDetails AS ip with (nolock) ON ip.intIP = w.IPid
						  inner join td_WorkWageRequest wwr with (nolock) on 
							w.intWorkid=wwr.intWorkid
						left join tm_wagepaymentcycle wpc with (nolock)
							on wwr.paymentcycle= wpc.intpaymentcycleid
   
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
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, w.WorkCode,w.intWorkid, wpc.wagepaymentcycle as PaymentCycle, wwr.intWagePaymentReqid as intWagePaymentReqid,s.intStateid,c.intCountieid,p.intPayamid,b.intBomasid
FROM           
                         dbo.tbl_WorkMaster as w with (nolock) INNER JOIN
                         dbo.tm_Bomas AS b with (nolock) ON b.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS p with (nolock) ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c with (nolock) ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s with (nolock) ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip (nolock)  ON ip.intIP = w.IPid
						 inner join td_WorkWageRequest wwr with (nolock) on 
							w.intWorkid=wwr.intWorkid
						left join tm_wagepaymentcycle wpc with (nolock)
							on wwr.paymentcycle= wpc.intpaymentcycleid
   
   where intIP like @IPid and w.intStateid like @intStateid and w.intCountieid like @intCountieid and w.intPayamid like @intPayamid and w.intBomasid like @intBomasid 

end
else -- @reportType='All'
begin
INSERT INTO #TempWorDetails

SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, w.WorkCode,w.intWorkid, wpc.wagepaymentcycle as PaymentCycle,wwr.intWagePaymentReqid as intWagePaymentReqid,s.intStateid,c.intCountieid,p.intPayamid,b.intBomasid
FROM           
                         dbo.tbl_WorkMaster as w with (nolock)  INNER JOIN
                         dbo.tm_Bomas AS b with (nolock)  ON b.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS p  with (nolock) ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c with (nolock)  ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s  with (nolock) ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip with (nolock)  ON ip.intIP = w.IPid
						 inner join td_WorkWageRequest wwr with (nolock)  on 
							w.intWorkid=wwr.intWorkid
						left join tm_wagepaymentcycle wpc with (nolock) 
							on wwr.paymentcycle= wpc.intpaymentcycleid
   
   where intIP like @IPid and w.intStateid like @intStateid and w.intCountieid like @intCountieid and w.intPayamid like @intPayamid and w.intBomasid like @intBomasid 

   UNION
   SELECT DISTINCT 
                         ip.IPName, NULL,NULL, NULL, NULL, w.WorkCode,w.intWorkid, wpc.wagepaymentcycle as PaymentCycle,wwr.intWagePaymentReqid as intWagePaymentReqid, NULL, NULL, NULL, NULL
FROM           
                         dbo.tbl_WorkMaster as w  with (nolock) 
						 INNER JOIN dbo.td_IPDetails AS ip with (nolock)  ON ip.intIP = w.IPid
						  inner join td_WorkWageRequest wwr with (nolock)  on 
							w.intWorkid=wwr.intWorkid
						left join tm_wagepaymentcycle wpc with (nolock) 
							on wwr.paymentcycle= wpc.intpaymentcycleid
   
   where intIP like @IPid 
   and w.intStateid is null
   and w.intCountieid is null
   and w.intPayamid is null
   and w.intBomasid is null
   and w.WorkCode like @workCode
end

create table #tblPayment
(
intWorkid int,
FromDate varchar(11),
ToDate varchar(11),
TotalMembers int,
TotalWorkDays int,
AmountPaidUSD decimal(18,2),
AmountPaidSSP decimal(18,2),
PaymentDate varchar(11),
UUID int
)

insert into #tblPayment
   select tr_BenWagesPayment.intWorkid,
CONVERT(varchar(11), dbo.tr_BenWagesPayment.FromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.tr_BenWagesPayment.ToDate, 106) AS ToDate, COUNT(DISTINCT dbo.tr_BenWagesPayment.intBenid) AS TotalMembers, SUM(dbo.tr_BenWagesPayment.AttendanceDays) AS TotalWorkDays, 
                         SUM(dbo.tr_BenWagesPayment.PaidAmountUSD) AS AmountPaidUSD, SUM(dbo.tr_BenWagesPayment.PaidAmount) AS AmountPaidSSP,Convert(varchar(11), MAx(tr_BenWagesPayment.PaidDate),106) as PaymentDate, tr_BenWagesPayment.uuid as UUID


FROM            dbo.tr_BenWagesPayment
group by tr_BenWagesPayment.intWorkid,tr_BenWagesPayment.uuid,
 CONVERT(varchar(11), dbo.tr_BenWagesPayment.FromDate, 106), CONVERT(varchar(11), 
                         dbo.tr_BenWagesPayment.ToDate, 106)

create table #results
(
[IPName] varchar(40),          
[StateName] varchar(40),             
[CountieName] varchar(40),         
[PayamName] varchar(40), 
[BomaName] varchar(40),         
[WorkCode] varchar(80),
[FromDate] varchar(11), 
[ToDate] varchar(11),
[TotalMembers] int, 
[TotalWorkDays] int,
[AmountPaidUSD] decimal(18,2),
[AmountPaidSSP] decimal(18,2),
[PaymentDate] varchar(11),
[PaymentCycle] varchar(50),
[intStateid] int,
[intCountieid] int,
[intPayamId] int,
[intBomasId] int
)

insert into #results

select      a.IPName
, isnull(a.StateName,(select top 1 st.statename 
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_states st on st.intStateid=b.intStateid
						where paym.uuid= a.intWagePaymentReqid)) as StateName
, isnull(a.CountieName,(select top 1 cou.countiename 
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_Counties cou on cou.intCountieid=b.intCountieid
						where paym.uuid= a.intWagePaymentReqid)) as CountieName
, isnull(a.PayamName,(select top 1 p.PayamName 
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_payams p on p.intPayamid=b.intPayamid
						where paym.uuid= a.intWagePaymentReqid)) as PayamName
, isnull(a.BomaName,(select top 1 bom.BomaName 
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_Bomas bom on bom.intBomasid=b.intBomasid
						where paym.uuid= a.intWagePaymentReqid)) as BomaName
, a.WorkCode
, b.FromDate
, b.ToDate
, b.TotalMembers
, b.TotalWorkDays
, b.AmountPaidUSD
, b.AmountPaidSSP
,b.PaymentDate
, a.PaymentCycle 
, isnull(a.intStateId,(select top 1 st.intStateid 
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_states st on st.intStateid=b.intStateid
						where paym.uuid= a.intWagePaymentReqid)) as intStateid
, isnull(a.intCountyId,(select top 1 cou.intCountieid
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_Counties cou on cou.intCountieid=b.intCountieid
						where paym.uuid= a.intWagePaymentReqid)) as intCountieid
, isnull(a.intPayamId,(select top 1 p.intPayamid
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_payams p on p.intPayamid=b.intPayamid
						where paym.uuid= a.intWagePaymentReqid)) as intPayamid
, isnull(a.intBomaId,(select top 1 bom.intBomasid
						from tr_BenWagesPayment paym 
						inner join tbl_BeneficiaryDet b on paym.intBenid=b.intBenid 
						inner join tm_Bomas bom on bom.intBomasid=b.intBomasid
						where paym.uuid= a.intWagePaymentReqid)) as intBomaId
						--,intStateId,intCountyId,intPayamId,intBomaId
from #TempWorDetails as a 
inner join #tblPayment as b on b.uuid=a.intWagePaymentReqid
						 --BELOW ROW WAS PRESENT BEFORE TO ADD DIS SECTION
						 --inner join #tblPayment as b on b.intWorkid=a.intWorkid
--where intStateid like @intStateid
--and  intCountyId like @intCountieid
--and intPayamId like @intPayamid
--and  intBomaId like @intBomasid
order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, FromDate, ToDate

select * from #results
where #results.intStateid like @intStateid
and  #results.intCountieid like @intCountieid
and #results.intPayamId like @intPayamid
and  #results.intBomasId like @intBomasid
order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode, FromDate, ToDate

select    sum(distinct TotalMembers) as TotalMembers,sum(TotalWorkDays) as TotalWorkDays, sum(AmountPaidUSD)  as AmountPaidUSD, sum(AmountPaidSSP)  as AmountPaidSSP  
from #results
--from #TempWorDetails as a 
--inner join #tblPayment as b on b.uuid=a.intWagePaymentReqid
						 --BELOW ROW WAS PRESENT BEFORE TO ADD DIS SECTION
						 --inner join #tblPayment as b on b.intWorkid=a.intWorkid
where #results.intStateid like @intStateid
and  #results.intCountieid like @intCountieid
and #results.intPayamId like @intPayamid
and  #results.intBomasId like @intBomasid


Drop table #TempWorDetails
Drop table #tblPayment
end