CREATE procedure [dbo].[getAttendanceCountFromTobyReqid]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100) ,
@intWagePaymentReqid varchar(8)  
)  
as  
begin  

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





  select distinct  td_WageReqBenDet.intWagesReqBenTransid,  td_WageReqBenDet.intWagePaymentReqid, tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
case when td_WageReqBenDet.traineealternate is not null then  td_WageReqBenDet.traineealternate else tbl_BeneficiaryDet.NameofHousehold  end as NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName

,td_WageReqBenDet.TotAttendance - isnull(abCount.[AbsenceDays],0) as cnt
, isnull(abCount.[AbsenceDays],0) as [AbsenceDays]
, age as Age, SelectionCriteria as SelectionCriteria,

td_WageReqBenDet.WageRateUSDTotal
 as Amt,
 tr_BenWagesPayment.PaidDate as PaidDate,
	case
	when tr_BenWagesPayment.intBenid is null then 'Unpaid'
	else 'Paid'
	end as PaymentStatus,
  case  
	when td_WageReqBenDet.ExchangeRateNew is null then (select top 1 ExchangeRate  from td_ExchangeRateDet  where intStateid = tbl_WorkMaster.intStateid   and intCountyid = tbl_WorkMaster.intCountieid )
	else td_WageReqBenDet .ExchangeRateNew 
end as ExchangeRateNew
,
 case 
 when  td_WageReqBenDet.AmountInSSP = 0 then   
 ( case  
	when td_WageReqBenDet.ExchangeRateNew is null then (select top 1 ExchangeRate  from td_ExchangeRateDet  where intStateid = tbl_WorkMaster.intStateid   and intCountyid = tbl_WorkMaster.intCountieid )
	else td_WageReqBenDet .ExchangeRateNew 
end ) * td_WageReqBenDet.WageRateUSDTotal
 else  td_WageReqBenDet.AmountInSSP
 end as AmountInSSP
 
from dbo.tbl_BeneficiaryDet  
		inner join tr_BenfWorkDet 
			on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid  
		inner join tbl_WorkMaster 
			on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid  
		left outer join tm_Bomas 
			on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid 
		inner join td_WageReqBenDet 
			on td_WageReqBenDet.intBenid=tr_BenfWorkDet.intBenid and td_WageReqBenDet.intWorkid=tr_BenfWorkDet.intWorkid 
		left join tr_BenWagesPayment 
			on td_WageReqBenDet.intBenid=tr_BenWagesPayment.intBenid and td_WageReqBenDet.intWagePaymentReqid=tr_BenWagesPayment.uuid
		left join #TempAbsenceCount abCount on td_WageReqBenDet.intbenid=abCount.intbenid
where 
tr_BenfWorkDet.intWorkid like @intWorkActivityId  
and 
td_WageReqBenDet.intWagePaymentReqid like @intWagePaymentReqid
order by tbl_BeneficiaryDet.EnrollmentNo asc

end


