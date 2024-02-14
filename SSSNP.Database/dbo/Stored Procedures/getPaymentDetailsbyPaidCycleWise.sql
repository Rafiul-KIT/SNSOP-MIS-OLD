CREATE procedure [dbo].[getPaymentDetailsbyPaidCycleWise]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(8)

)  
as  
begin  

Declare @BiometricFilePaths varchar(380)
set @BiometricFilePaths=(select BiomatricPath from tm_FilePathMap)

create table #tblAlternatemorethan
(
intBenid int,
household_number varchar(240),
alternate_number varchar(240),
alternate_name varchar(180),
photo_url varchar(180),
Photopath varchar(380)
)
insert into #tblAlternatemorethan
select intBenid,household_number,alternate_number,alternate_name,photo_url,@BiometricFilePaths + photo_url as Photopath from tbl_BenfMorethanAlternateDet
	             

SELECT DISTINCT 
                  
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
                      dbo.tr_BenWagesPayment.AttendanceDays AS cnt,tr_BenWagesPayment.PaidAmountUSD  AS Amt,tr_BenWagesPayment.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
					
				case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else case when (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) <>0 then (select alternate_name from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) else  'Paid Person details is not available in MIS due to alternative not registered.' end end end end end as PaidToName,
						 
					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto,
					case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) <>0 then 'Alternate' else  'Not registered in MIS' end  end end end end as PaidToType
					,tbl_BeneficiaryDet.household_number,
					td_WorkWageRequest.ReqNo,Convert(varchar(11),  tr_BenWagesPayment.PaidDate,106) as PayDate,
					Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106) as ReqFDate,Convert(varchar(11),td_WorkWageRequest.ReqToDate,106) as ReqTDate,
					tr_BenWagesPayment.WageRateUSD,tr_BenWagesPayment.ExchangeRate
FROM       td_WorkWageRequest                     
                   INNER JOIN
            dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WorkWageRequest.intWagePaymentReqid  
			and tr_BenWagesPayment.intWorkid=td_WorkWageRequest.intWorkid
			inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid
			
			where tr_BenWagesPayment.intWorkid=@intWorkActivityId
			and td_WorkWageRequest.intWorkid=@intWorkActivityId
			and tr_BenWagesPayment.PaidDate between @FromDate and @ToDate+1
			and tr_BenWagesPayment.uuid is not null

			Drop table #tblAlternatemorethan

		
end


