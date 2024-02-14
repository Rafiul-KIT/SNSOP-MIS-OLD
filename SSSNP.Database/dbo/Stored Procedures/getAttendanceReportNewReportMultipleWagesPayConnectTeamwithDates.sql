
CREATE procedure [dbo].[getAttendanceReportNewReportMultipleWagesPayConnectTeamwithDates] -- '05/23/2016','05/30/2016',1015
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
)
if(@FromDate<>'' and @ToDate<>'')
Begin
INSERT INTO #TempWagePayment
select ba.intBenid,w.WorkCode,   CONVERT(varchar(11), ba.FromDate, 106), CONVERT(varchar(11), ba.ToDate, 106),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,Convert(varchar(11),ba.PaidDate,106),
Convert(varchar(11),ba.PaidGenerateddate,106),'Paid',ba.Paidto,Convert(varchar(4),Year(ba.Created_dt))+'-SNSDPWR-'+ Convert(varchar(8), ba.uuid)
 from    dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 where  w.intBomasid like @intBomasid and w.intPayamid like @intPayamid and w.intCountieid like @intCountieid
and w.intStateid like @intStateid and w.IPid like @IPid  and w.WorkCode like @workCode 
and  CONVERT(varchar(11), ba.FromDate, 106)= CONVERT(varchar(11), @FromDate, 106) and CONVERT(varchar(11), ba.ToDate, 106)=CONVERT(varchar(11), @ToDate, 106)
End
Else
Begin
INSERT INTO #TempWagePayment
select ba.intBenid,w.WorkCode,   CONVERT(varchar(11), ba.FromDate, 106), CONVERT(varchar(11), ba.ToDate, 106),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,Convert(varchar(11),ba.PaidDate,106),
Convert(varchar(11),ba.PaidGenerateddate,106),'Paid',ba.Paidto,Convert(varchar(4),Year(ba.Created_dt))+'-SNSDPWR-'+ Convert(varchar(8), ba.uuid)
 from    dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 where  w.intBomasid like @intBomasid and w.intPayamid like @intPayamid and w.intCountieid like @intCountieid
and w.intStateid like @intStateid and w.IPid like @IPid  and w.WorkCode like @workCode 

End





select t1.IPName as IP,t1.StateName as [State Name],t1.CountieName as [County Name],
       t1.PayamName as [Payam Name], t1.BomaName as [Boma Name], t2.WorkCode,t1.EnrollmentNo as [Enrollment Number],
	   t1.NameofHousehold as [Benficiary Name],t1.household_number as [Original Household number],t1.ContactNo as [Mobile Number],t2.FromDate as [From Date], 
       t2.ToDate as [To Date],t2.WageRateUSD as [Wage Rate (In USD)],t2.PaidAmountUSD as [Paid Amount (In USD)],t2.AttendanceDays as AttendanceDays,t2.ExchangeRateInSSP as ExchangeRateInSSP,t2.PaidAmount as [Paid Amount (In SSP)],t2.WagePaiddate as [Payment Date]
					  ,t2.PaymentGenreatedDate as [Payment Generate Date],case when t1.household_number=t2.Paidto then   t1.NameofHousehold else case when t1.alternate_number=t2.Paidto then t1.Name1stAlternatePayee else case when t1.Secondalternate_number=t2.Paidto then t1.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered' end end end  as [Paid House Hold Name],Paidto,PayStatus,WageRequestNumber
from #TempBen as t1 inner join #TempWagePayment as t2 on t1.intBenid=t2.intBenid




select  SUM( t1.PaidAmountUSD) as PaidAmountInUSD ,Sum( t1.AttendanceDays) as AttendanceDays,Sum(t1.PaidAmount) as PaidAmountInSSP from 
 #TempWagePayment as t1 where t1.AttendanceDays<>0


 Drop table #TempBen
Drop table #TempWagePayment


  
 -- Developer : Srikanth
-- Date : 14-03-2018
-- Purpose: Attendance Report New Format       
        
--select  IP, [State Name], [County Name], [Payam Name], [Boma Name], WorkCode, [Enrollment Number], [Benficiary Name],household_number as [Original Household number], [Mobile Number], [From Date], 
--                      [To Date], [Wage Rate (In USD)], [Paid Amount (In USD)], AttendanceDays, ExchangeRateInSSP, [Paid Amount (In SSP)],WagePaiddate as [Payment Date]
--					  ,PaymentGenreatedDate as [Payment Generate Date],PaidToName as [Paid House Hold Name],PaidTo,PayStatus,WageRequestNumber
--					   from Vw_WageReportfromPayConnectAPI
--where WorkCode like @workCode 
--and intBomasid like @intBomasid and intPayamid like @intPayamid and intCountieid like @intCountieid
--and intStateid like @intStateid and IPid like @IPid
--and AttendanceDays <>0
--					  order by   [Enrollment Number] asc


    
--select  SUM( [Paid Amount (In USD)]) as PaidAmountInUSD ,Sum(  AttendanceDays) as AttendanceDays,Sum( [Paid Amount (In SSP)]) asPaidAmountInSSP from Vw_WageReportfromPayConnectAPI
--where WorkCode like @workCode 
--and intBomasid like @intBomasid and intPayamid like @intPayamid and intCountieid like @intCountieid
--and intStateid like @intStateid and IPid like @IPid
--and AttendanceDays <>0
					
					

		

end




