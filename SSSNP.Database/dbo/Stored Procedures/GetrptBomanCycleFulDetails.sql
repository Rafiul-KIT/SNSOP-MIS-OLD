
CREATE PROCEDURE [dbo].[GetrptBomanCycleFulDetails] (@intIPid varchar(8),@Bomasid varchar(8),@Date as varchar(15))
AS
BEGIN

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
and b.intBomasid like @Bomasid and b.intIP like @intIPid


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
[uuid] varchar(8), 
[trans_id] varchar(8)
)
INSERT INTO #TempWagePayment
select ba.intBenid,w.WorkCode,   CONVERT(varchar(11), ba.FromDate, 106), CONVERT(varchar(11), ba.ToDate, 106),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,Convert(varchar(11),ba.PaidDate,106),
Convert(varchar(11),ba.PaidGenerateddate,106),'Paid',ba.Paidto,Convert(varchar(4),Year(ba.Created_dt))+'-SNSDPWR-'+ Convert(varchar(8), ba.uuid),
ba.uuid,ba.trans_id
 from    dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 where  w.intBomasid like @Bomasid  and w.IPid like @intIPid  
						 and CONVERT(varchar(11), ba.FromDate, 106)=@Date



CREATE TABLE #TempWagePaymentUnPaid
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
[uuid] varchar(8), 
[trans_id] varchar(8)
)
INSERT INTO #TempWagePaymentUnPaid
select ba.intBenid,w.WorkCode,   CONVERT(varchar(11), ba.FromDate, 106), CONVERT(varchar(11), ba.ToDate, 106),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,'',
'','UnPaid',ba.Paidto,Convert(varchar(4),Year(ba.Created_dt))+'-SNSDPWR-'+ Convert(varchar(8), ba.uuid),
ba.uuid,ba.trans_id
 from    dbo.tr_BenWagesPaymentNotPaid AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 where  w.intBomasid like @Bomasid  and w.IPid like @intIPid 
						  and CONVERT(varchar(11), ba.FromDate, 106)=@Date 





select t1.IPName as IP,t1.StateName as [State Name],t1.CountieName as [County Name],
       t1.PayamName as [Payam Name], t1.BomaName as [Boma Name], t2.WorkCode,t1.EnrollmentNo as [Enrollment Number],
	   t1.NameofHousehold as [Benficiary Name],t1.household_number as [Original Household number],t1.ContactNo as [Mobile Number],t2.FromDate as [From Date], 
       t2.ToDate as [To Date],t2.WageRateUSD as [Wage Rate (In USD)],t2.PaidAmountUSD as [Paid Amount (In USD)],t2.AttendanceDays as AttendanceDays,t2.ExchangeRateInSSP as ExchangeRateInSSP,t2.PaidAmount as [Paid Amount (In SSP)]
					  ,t2.PaymentGenreatedDate as [Payment Generate Date]
					  ,t2.WagePaiddate as [Payment Date]
					  ,case when t1.household_number=t2.Paidto then   'Household' else case when t1.alternate_number=t2.Paidto then 'Alternate one' else case when t1.Secondalternate_number=t2.Paidto then 'Alternate two' else 'Not Registerd in MIS' end end end as PaidtoType
					  
					  
					  ,case when t1.household_number=t2.Paidto then   t1.NameofHousehold else case when t1.alternate_number=t2.Paidto then t1.Name1stAlternatePayee else case when t1.Secondalternate_number=t2.Paidto then t1.Name2ndAlternatePayee else t1.NameofHousehold end end end  as [Paid House Hold Name],Paidto,PayStatus,WageRequestNumber,
					  uuid,trans_id
from #TempBen as t1 inner join #TempWagePayment as t2 on t1.intBenid=t2.intBenid

union
select t1.IPName as IP,t1.StateName as [State Name],t1.CountieName as [County Name],
       t1.PayamName as [Payam Name], t1.BomaName as [Boma Name], t2.WorkCode,t1.EnrollmentNo as [Enrollment Number],
	   t1.NameofHousehold as [Benficiary Name],t1.household_number as [Original Household number],t1.ContactNo as [Mobile Number],t2.FromDate as [From Date], 
       t2.ToDate as [To Date],t2.WageRateUSD as [Wage Rate (In USD)],t2.PaidAmountUSD as [Paid Amount (In USD)],t2.AttendanceDays as AttendanceDays,t2.ExchangeRateInSSP as ExchangeRateInSSP,t2.PaidAmount as [Paid Amount (In SSP)]
					  ,'' as [Payment Generate Date]
					  ,'' as [Payment Date]
					  
					  ,'' as PaidtoType
					  
					  ,''  as [Paid House Hold Name],'' as Paidto,PayStatus,WageRequestNumber,
					   uuid,trans_id
from #TempBen as t1 inner join #TempWagePaymentUnPaid as t2 on t1.intBenid=t2.intBenid


Drop table #TempBen
Drop table #TempWagePayment
Drop table #TempWagePaymentUnPaid
END
