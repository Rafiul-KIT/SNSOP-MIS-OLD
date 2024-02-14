
CREATE procedure [dbo].[sp_GetWagesrecords] --  exec sp_GetWagesrecords '06/22/2018','10/23/2018',1015
(  
@FromDate datetime,  
@ToDate datetime 
--@workCode varchar(120),
--@intStateid varchar(8),
--@intCountieid varchar(8),
--@intPayamid varchar(8),
--@intBomasid varchar(8) ,
--@IPid varchar(8) ,
--@EnrollmentNo varchar(30) 
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
PostDate datetime
)
INSERT INTO #TempWagePayment
select ba.intBenid,w.WorkCode,   CONVERT(varchar(11), ba.FromDate, 106), CONVERT(varchar(11), ba.ToDate, 106),ba.WageRateUSD,ba.PaidAmountUSD,ba.AttendanceDays,ba.ExchangeRate,ba.PaidAmount
,Convert(varchar(11),ba.PaidDate,106),
Convert(varchar(11),ba.PaidGenerateddate,106),'Paid',ba.Paidto,Convert(varchar(4),Year(ba.Created_dt))+'-SNSDPWR-'+ Convert(varchar(8), ba.uuid),
ba.Created_dt
 from    dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid
						 where 
						 ba.Created_dt between @FromDate and @ToDate+1




select t1.IPName as IP,t1.StateName as [State Name],t1.CountieName as [County Name],
       t1.PayamName as [Payam Name], t1.BomaName as [Boma Name], t2.WorkCode,WageRequestNumber,t2.FromDate as [From Date], 
       t2.ToDate as [To Date],t1.EnrollmentNo as [Enrollment Number],
	   t1.NameofHousehold as [Benficiary Name],t1.household_number as [Original Household number],t1.ContactNo as [Mobile Number],t2.WageRateUSD as [Wage Rate (In USD)],t2.PaidAmountUSD as [Paid Amount (In USD)],t2.AttendanceDays as AttendanceDays,t2.ExchangeRateInSSP as ExchangeRateInSSP,t2.PaidAmount as [Paid Amount (In SSP)],t2.WagePaiddate as [Payment Date]
					  ,t2.PaymentGenreatedDate as [Payment Generate Date],case when t1.household_number=t2.Paidto then   t1.NameofHousehold else case when t1.alternate_number=t2.Paidto then t1.Name1stAlternatePayee else case when t1.Secondalternate_number=t2.Paidto then t1.Name2ndAlternatePayee else 'Paid Person details is not available in MIS due to more than two alternative beneficiaries or alternative not registered.' end end end  as [Paid House Hold Name],Paidto,PayStatus,

					  					case when 	t2.Paidto is null then '' else   	case when t1.household_number=t2.Paidto then   'Household' else case when t1.alternate_number=t2.Paidto then 'Alternate' else case when t1.Secondalternate_number=t2.Paidto then 'Alternate' else '' end end end end as PaidToType,
										Convert(varchar(11),t2.PostDate,106) as PulledDate

from #TempBen as t1 inner join #TempWagePayment as t2 on t1.intBenid=t2.intBenid



 Drop table #TempBen
Drop table #TempWagePayment
end