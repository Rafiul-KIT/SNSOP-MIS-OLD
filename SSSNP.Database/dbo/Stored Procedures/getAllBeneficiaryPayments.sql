
CREATE procedure [dbo].[getAllBeneficiaryPayments] 
(  
	@workCode varchar(120),
	@intStateid varchar(8),
	@intCountieid varchar(8),
	@intPayamid varchar(8),
	@intBomasid varchar(8) ,
	@IPid varchar(8)  ,
	@RequestNo varchar(8)  ,
	@EnrollmentNo varchar(30)  ,
	@Household_number varchar(30)  
)  
as  
begin  

--Date interval from WR
--temp tabel for each Beneficiary with Absence days: intbenid, intworkid, interal base



CREATE TABLE #TempBen          
(          
	[IPName] varchar(80),          
	[StateName] varchar(80),             
	[CountieName] varchar(80),         
	[PayamName] varchar(80), 
	[BomaName] varchar(80),         
	[EnrollmentNo] varchar(60),           
	[NameofHousehold] varchar(180),           
	[ContactNo] varchar(30),
	[household_number] varchar(180),  
	[alternate_number] varchar(180),  
	[Name1stAlternatePayee] varchar(180),  
	[Secondalternate_number] varchar(180),  
	[Name2ndAlternatePayee] varchar(180),  
	[intBenid] int,
	[gender] varchar(1),
	[component] varchar(200)     
)  

INSERT INTO #TempBen

select  
	ip.IPName
	,s.StateName
	,c.CountieName
	,paym.PayamName
	,bom.BomaName
	,b.EnrollmentNo
	,b.NameofHousehold
	,b.ContactNo
	,b.household_number
	,b.alternate_number
	,b.Name1stAlternatePayee
	,b.Secondalternate_number
	,b.Name2ndAlternatePayee
	,b.intBenid
	,b.Sex
	,b.SelectionCriteria
from	dbo.tbl_BeneficiaryDet AS b
	INNER JOIN dbo.tm_Bomas AS bom 
			ON bom.intBomasid = b.intBomasid 
	INNER JOIN dbo.tm_Payams AS paym 
			ON paym.intPayamid = bom.intPayamid 
	INNER JOIN dbo.tm_Counties AS c 
			ON c.intCountieid = paym.intCountieid 
	INNER JOIN dbo.tm_States AS s 
			ON s.intStateid = c.intStateid 
	INNER JOIN dbo.td_IPDetails AS ip 
			ON ip.intIP = b.intIP
						
WHERE
	b.intBomasid like @intBomasid 
	and b.intPayamid like @intPayamid 
	and b.intCountieid like @intCountieid
	and b.intStateid like @intStateid 
	and b.intIP like @IPid
	and b.EnrollmentNo like @EnrollmentNo 
	and isnull(b.household_number,'') like @Household_number


CREATE TABLE #TempWagePayment
(
	[intBenid] int,       
	[WorkCode] varchar(380),         
	[FromDate] varchar(15), 
	[ToDate] varchar(15), 
	[WageRateUSD] decimal(18,2), 
	[PaidAmountUSD] decimal(18,2), 
	[AttendanceDays] int, 
	[ExchangeRateInSSP] decimal(18,2), 
	[PaidAmount] decimal(18,2), 
	[WagePaiddate] varchar(15), 
	[PaymentGenreatedDate] varchar(15), 
	[PayStatus] varchar(40), 
	[Paidto] varchar(80), 
	[WageRequestNumber] varchar(40), 
	[PaymentCycle] varchar(50),
	Latitude varchar(180),
	Longitude varchar(180)
)

INSERT INTO #TempWagePayment
select 
	req.intBenid
	,w.WorkCode
	,Convert(varchar(11),wwr.ReqFromDate,106) as FromDate
	,Convert(varchar(11),wwr.ReqToDate,106) as toDate
	,req.WageRateUSD as WageRateUSD
	,isnull(ba.PaidAmountUSD,0) as PaidAmountUSD
	,req.TotAttendance as AttendanceDays
	,req.ExchangeRateNew as ExchangeRate
	,isnull(ba.PaidAmount,0) as PaidAmount
	,Convert(varchar(11),ba.PaidDate,106) as Paiddate
	,Convert(varchar(11),ba.PaidGenerateddate,106) as PaidGeneratedDate
	,case when ba.paidto  is null
		then 'Unpaid'
		else 'Paid' 
		end
	,ba.Paidto as Paidto
	,wwr.ReqNo as ReqNo
	,pc.WagePaymentCycle as WagePaymentCycle
	,isnull(ba.latitude,'---') as latitude
	,isnull(ba.longitude,'---') as longitude
	from td_WageReqBenDet req 
		left join dbo.tr_BenWagesPayment AS ba 
			on req.intWagesReqBenTransid=ba.trans_id
		left JOIN dbo.tbl_WorkMaster AS w 
			ON w.intWorkid = req.intWorkid 
		left join td_WorkWageRequest wwr
			on req.intWagePaymentReqid=wwr.intWagePaymentReqid
		left join tm_WagePaymentCycle pc
			on wwr.PaymentCycle=pc.intPaymentCycleID
		left join tbl_BeneficiaryDet benf
			on req.intBenid=benf.intBenid
	where  
		benf.intBomasid like @intBomasid 
		and benf.intPayamid like @intPayamid 
		and benf.intCountieid like @intCountieid
		and benf.intStateid like @intStateid 
		and w.IPid like @IPid 
		and w.WorkCode like @workCode 
		and isnull(convert(varchar, ba.uuid),'') like @RequestNo

CREATE TABLE #TempAbsenceCount
(
	[intBenId] int,     
	[intWagePaymentReqid] int,       
	[ReqFromDate] datetime,         
	[ReqToDate] datetime, 
	[AbsenceDays] int	
)

insert into #TempAbsenceCount (intbenid, intwagepaymentreqid,reqfromdate,reqtodate,absencedays)
SELECT att.intbenid,intWagePaymentReqid, ReqFromDate, ReqToDate, count (att.status) FROM td_WorkWageRequest WWR
INNER JOIN tr_BenfWorkDet bw on bw.intWorkid=wwr.intWorkid
left join tr_BenAttendanceDet att 
on att.intBenid=bw.intBenid
and att.intWorkActivityId=bw.intWorkid
where att.status='Absent'
group by att.intbenid,intWagePaymentReqid,ReqFromDate,ReqToDate



select distinct
	t1.IPName as IP
	,t1.StateName as [State Name]
	,t1.CountieName as [County Name]
	,t1.PayamName as [Payam Name]
	,t1.BomaName as [Boma Name]
	,t2.WorkCode
	,t1.EnrollmentNo as [Enrollment Number]
	,t1.NameofHousehold as [Benficiary Name]
	,t1.household_number as [Original Household number]
	,t1.gender as [Gender]
	,t1.component as [Component]
	,t2.FromDate as [From Date]
	,t2.ToDate as [To Date]
	,t2.WageRateUSD as [Wage Rate (In USD)]
	,t2.PaidAmountUSD as [Paid Amount (In USD)]
	,t2.AttendanceDays-ISNULL(tabs.absencedays, 0) as AttendanceDays
	,ISNULL(tabs.absencedays, 0) as [Absence Days]
	,t2.ExchangeRateInSSP as ExchangeRateInSSP
	,t2.PaidAmount as [Paid Amount (In SSP)]
	,t2.WagePaiddate as [Payment Date]
	,t2.PaymentGenreatedDate as [Payment Generate Date]
	,case 
		when Paidto is null 
			then '' 
				else 	  
				case 
					when t1.household_number=t2.Paidto 
						then   t1.NameofHousehold 
						else 
						case 
							when t1.alternate_number=t2.Paidto 
								then t1.Name1stAlternatePayee 
								else 
								case 
									when t1.Secondalternate_number=t2.Paidto 
									then t1.Name2ndAlternatePayee 
									else t1.NameofHousehold 
								end 
						end 
				end 
		end  as [Paid House Hold Name]					  
	,Paidto
	,PayStatus
	,WageRequestNumber
	,t2.PaymentCycle
	,Latitude
	,Longitude

from #TempBen as t1 
	inner join #TempWagePayment as t2 
		on t1.intBenid=t2.intBenid
	left join #TempAbsenceCount tAbs
		on t1.intbenid=tAbs.intbenid

order by  
	t1.IPName
	,t1.StateName
	,t1.CountieName
	,t1.PayamName
	, t1.BomaName
	, t2.WorkCode
		
Drop table #TempBen
Drop table #TempWagePayment
drop table #TempAbsenceCount

end




