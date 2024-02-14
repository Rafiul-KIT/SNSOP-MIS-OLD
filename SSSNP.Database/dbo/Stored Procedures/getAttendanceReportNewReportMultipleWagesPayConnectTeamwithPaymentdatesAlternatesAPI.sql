

CREATE procedure [dbo].[getAttendanceReportNewReportMultipleWagesPayConnectTeamwithPaymentdatesAlternatesAPI] -- '05/23/2016','05/30/2016',1015
  
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

INSERT INTO #TempWagePayment
select ba.intBenid,w.WorkCode,   CONVERT(char(10), ba.FromDate,126), CONVERT(char(10), ba.ToDate, 126),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,CONVERT(char(10),ba.PaidDate,126),
CONVERT(char(10),ba.PaidGenerateddate,126),'Paid',ba.Paidto,Convert(varchar(4),Year(ba.Created_dt))+'-SNSDPWR-'+ Convert(varchar(8), ba.uuid)
 from    dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						



 





select t1.IPName as IP,t1.StateName as [State Name],t1.CountieName as [County Name],
       t1.PayamName as [Payam Name], t1.BomaName as [Boma Name], t2.WorkCode,t1.EnrollmentNo as [Enrollment Number],
	   t1.NameofHousehold as [Benficiary Name],t1.household_number as [Original Household number],t1.ContactNo as [Mobile Number],t2.FromDate as [From Date], 
       t2.ToDate as [To Date],t2.WageRateUSD as [Wage Rate (In USD)],t2.PaidAmountUSD as [Paid Amount (In USD)],t2.AttendanceDays as AttendanceDays,t2.ExchangeRateInSSP as ExchangeRateInSSP,t2.PaidAmount as [Paid Amount (In SSP)],t2.WagePaiddate as [Payment Date]
					  ,t2.PaymentGenreatedDate as [Payment Generate Date],case when t1.household_number=t2.Paidto then   t1.NameofHousehold else case when t1.alternate_number=t2.Paidto then t1.Name1stAlternatePayee else case when t1.Secondalternate_number=t2.Paidto then t1.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered' end end end  as [Paid House Hold Name],Paidto,PayStatus,WageRequestNumber
from #TempBen as t1 inner join #TempWagePayment as t2 on t1.intBenid=t2.intBenid
where t1.household_number<>t2.Paidto
order by t1.IPName,t2.WagePaiddate asc





 Drop table #TempBen
Drop table #TempWagePayment


  
end
