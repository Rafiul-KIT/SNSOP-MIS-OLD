
CREATE procedure [dbo].[getAttendanceReportNewReportMultipleWages] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8)  
)  
as  
begin  
  --exec getAttendanceReportNewReportMultipleWages null,null,'%','%','%','%','%','1005'
 -- Developer : Srikanth
-- Date : 14-03-2018
-- Purpose: Attendance Report New Format     




CREATE TABLE #TempBen          
(          
[IPName] varchar(40),          
[StateName] varchar(40),             
[CountieName] varchar(40),         
[PayamName] varchar(40), 
[BomaName] varchar(40),         
[EnrollmentNo] varchar(30),           
[NameofHousehold] varchar(60),           
[ContactNo] varchar(15),
[household_number] varchar(80),  
[alternate_number] varchar(80),  
[Name1stAlternatePayee] varchar(60),  
[Secondalternate_number] varchar(80),  
[Name2ndAlternatePayee] varchar(60),  
[intBenid] int,   
     
)  

INSERT INTO #TempBen

select  ip.IPName,s.StateName,c.CountieName,paym.PayamName,bom.BomaName,b.EnrollmentNo,b.NameofHousehold,b.ContactNo,b.household_number,

b.alternate_number,b.Name1stAlternatePayee,b.Secondalternate_number,b.Name2ndAlternatePayee,b.intBenid
 from dbo.tbl_BeneficiaryDet AS b INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = b.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = b.intIP
						
WHERE        (b.household_number IS NOT NULL)
and b.intBomasid like @intBomasid and b.intPayamid like @intPayamid and b.intCountieid like @intCountieid
and b.intStateid like @intStateid and b.intIP like @IPid


create table #TempWageTrasn
(
intWageReqid	int,
WStatus varchar(15),
ReqApprovalDate datetime
)
insert into #TempWageTrasn
select distinct ba.intWagePaymentReqid,ba.Status ,MIN(ba.Created_dt) from td_WageReqTrans  as ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						  inner join td_WorkWageRequest as wr on wr.intWagePaymentReqid=ba.intWagePaymentReqid
						   where w.IPid like @IPid
						  group by ba.intWagePaymentReqid,ba.Status 
						having (   ba.Status='2' )


						
create table #TempWageTrasn1
(
intWageReqid	int,
WStatus varchar(15),
ReqApprovalDate datetime
)
insert into #TempWageTrasn1
select distinct ba.intWagePaymentReqid,ba.Status ,MIN(ba.Created_dt) from td_WageReqTrans  as ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 
						  inner join td_WorkWageRequest as wr on wr.intWagePaymentReqid=ba.intWagePaymentReqid
						  where w.IPid like @IPid
						  group by ba.intWagePaymentReqid,ba.Status 
						having (    ba.Status='3' )



											
create table #TempWageTrasn2
(
intWageReqid	int,
WStatus varchar(15),
ReqApprovalDate datetime
)
insert into #TempWageTrasn2
select distinct ba.intWagePaymentReqid,ba.Status ,MIN(ba.Created_dt) from td_WageReqTrans  as ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						  inner join td_WorkWageRequest as wr on wr.intWagePaymentReqid=ba.intWagePaymentReqid
						   where w.IPid like @IPid
						  group by ba.intWagePaymentReqid,ba.Status 
						having (   ba.Status='4')




CREATE TABLE #TempWagePayment
(
[intBenid] int,       
[WorkCode] varchar(80),         
[FromDate] varchar(15), 
[ToDate] varchar(15), 
[WageRateUSD] decimal(18,2), 
[PaidAmountUSD] decimal(18,2), 
[AttendanceDays] int, 
[ExchangeRateInSSP] decimal(18,2), 
[PaidAmount] decimal(18,2), 
[WagePaiddate] varchar(15), 
[PaymentGenreatedDate] varchar(15), 
[PayStatus] varchar(8), 
[Paidto] varchar(80), 
[WageRequestNumber] varchar(20), 
WageReqDate varchar(15),
WageApprovalDate varchar(15),
WageReviewDate varchar(15),
WageVerifiedDate varchar(15),
Paidtoname varchar(240)
)
INSERT INTO #TempWagePayment
select ba.intBenid,w.WorkCode,   CONVERT(varchar(11), ba.FromDate, 106), CONVERT(varchar(11), ba.ToDate, 106),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,Convert(varchar(11),ba.PaidDate,106),
Convert(varchar(11),ba.PaidGenerateddate,106),'Paid',ba.Paidto,wr.ReqNo,
 CONVERT(varchar(11), wr.Created_dt, 106),  
  CONVERT(varchar(11),wr.MApproveDate,106),
  --CONVERT(varchar(11), (select top 1 ReqApprovalDate from  #TempWageTrasn2 as aa where aa.intWageReqid=wr.intWagePaymentReqid and aa.WStatus='4'),106),


    CONVERT(varchar(11), (select top 1 ReqApprovalDate from  #TempWageTrasn as aa where aa.intWageReqid=wr.intWagePaymentReqid and aa.WStatus='2'),106),
    CONVERT(varchar(11), (select top 1 ReqApprovalDate from  #TempWageTrasn1 as aa where aa.intWageReqid=wr.intWagePaymentReqid and aa.WStatus='3'),106),
	ba.Paidtoname
 from    dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 inner join td_WorkWageRequest as wr on wr.intWagePaymentReqid=ba.uuid
						 where  w.intBomasid like @intBomasid and w.intPayamid like @intPayamid and w.intCountieid like @intCountieid
and w.intStateid like @intStateid and w.IPid like @IPid  and w.WorkCode like @workCode 
--and ba.AttendanceDays <>0




select t1.IPName as IP,t1.StateName as [State Name],t1.CountieName as [County Name],
       t1.PayamName as [Payam Name], t1.BomaName as [Boma Name], t2.WorkCode,t1.EnrollmentNo as [Enrollment Number],
	   t1.NameofHousehold as [Benficiary Name],t1.household_number as [Original Household number],t1.ContactNo as [Mobile Number],t2.FromDate as [From Date], 
       t2.ToDate as [To Date],t2.WageRateUSD as [Wage Rate (In USD)],t2.PaidAmountUSD as [Paid Amount (In USD)],t2.AttendanceDays as AttendanceDays,t2.ExchangeRateInSSP as ExchangeRateInSSP,t2.PaidAmount as [Paid Amount (In SSP)], t2.WageReqDate as [Request Date], t2.WageReviewDate as [Request Review Date], t2.WageVerifiedDate as [Request Verified Date],  t2.WageApprovalDate as [Request Approval Date],    t2.WagePaiddate as [Paid Date]
					 ,
					 case when t1.household_number=t2.Paidto then   t1.NameofHousehold else case when t1.alternate_number=t2.Paidto then t1.Name1stAlternatePayee else case when t1.Secondalternate_number=t2.Paidto then t1.Name2ndAlternatePayee else  case when t2.Paidtoname is not null then t2.Paidtoname else  'Paid Person details is not available in MIS due to alternative not registered.'  end end end end 
					-- dbo.GetPaidtoName(t2.Paidto,t2.intBenid)
					 
					  as [Paid House Hold Name],
					 
					 	-- dbo.GetPaidtoType(t2.Paidto,t2.intBenid)
					 	case when t2.Paidto is null then '' else   case when t1.household_number=t2.Paidto then   'Household' else case when t1.alternate_number=t2.Paidto then 'Alternate' else case when t1.Secondalternate_number=t2.Paidto then  'Alternate' else case when t2.Paidtoname is not null then 'Alternate' else    'Paid Person details is not available in MIS due to alternative not registered.'  end  end end end end
						
						  as [PaidToType],Paidto,PayStatus,WageRequestNumber

from #TempBen as t1 inner join #TempWagePayment as t2 on t1.intBenid=t2.intBenid

order by 
t1.EnrollmentNo asc


  
        
--select  IP, [State Name], [County Name], [Payam Name], [Boma Name], WorkCode, [Enrollment Number], [Benficiary Name], [Mobile Number], [From Date], 
--                      [To Date], [Wage Rate (In USD)], [Paid Amount (In USD)], AttendanceDays, ExchangeRateInSSP, [Paid Amount (In SSP)] from Vw_wagereport
--where WorkCode like @workCode 
--and intBomasid like @intBomasid and intPayamid like @intPayamid and intCountieid like @intCountieid
--and intStateid like @intStateid and IPid like @IPid
--and AttendanceDays <>0
--					  order by   [Enrollment Number] asc


    
select  SUM( t2.PaidAmountUSD) as PaidAmountInUSD ,Sum(  t2.AttendanceDays) as AttendanceDays,Sum( t2.PaidAmount) as PaidAmountInSSP from #TempBen as t1 inner join #TempWagePayment as t2 on t1.intBenid=t2.intBenid

					
Drop table #TempBen
Drop table #TempWagePayment
Drop table #TempWageTrasn
Drop table #TempWageTrasn1
Drop table #TempWageTrasn2
--Drop table #tblAlternatemorethan
		

end




