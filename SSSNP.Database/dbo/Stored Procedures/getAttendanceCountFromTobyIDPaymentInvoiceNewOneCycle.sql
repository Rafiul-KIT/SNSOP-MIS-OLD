CREATE procedure [dbo].[getAttendanceCountFromTobyIDPaymentInvoiceNewOneCycle]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100) ,
@intWagePaymentReqid varchar(8) ,
@intWagePaymentReqPAid varchar(8) 
)  
as  
begin  
  SELECT DISTINCT 
                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
                      dbo.tr_BenWagesPayment.AttendanceDays AS cnt,tr_BenWagesPayment.PaidAmountUSD  AS Amt,tr_BenWagesPayment.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
				case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else '' end end end end as PaidToName,
						 
					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto,
					case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else '' end end end end as PaidToType
					,tbl_BeneficiaryDet.household_number,tr_BenWagesPayment.matched_fp
FROM       td_WageReqBenDet inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid INNER JOIN
td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid

                    
                   INNER JOIN
            dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPayment.intWorkid=td_WageReqBenDet.intWorkid
			where tr_BenWagesPayment.intWorkid like @intWorkActivityId and td_WorkWageRequest.intWorkid like @intWorkActivityId and 
			td_WageReqBenDet.intWorkid like @intWorkActivityId and Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),@FromDate,106) and Convert(varchar(11),td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),@ToDate,106)
UNION
SELECT DISTINCT 
                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
                      dbo.tr_BenWagesPaymentNotPaid.AttendanceDays AS cnt,tr_BenWagesPaymentNotPaid.PaidAmountUSD  AS Amt,tr_BenWagesPaymentNotPaid.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
				case when 	tr_BenWagesPaymentNotPaid.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPaymentNotPaid.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPaymentNotPaid.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPaymentNotPaid.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else '' end end end end as PaidToName,
						 
					case when 	tr_BenWagesPaymentNotPaid.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPaymentNotPaid.Paidto,
					case when 	tr_BenWagesPaymentNotPaid.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPaymentNotPaid.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPaymentNotPaid.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPaymentNotPaid.Paidto then 'Alternate' else '' end end end end as PaidToType
					,tbl_BeneficiaryDet.household_number,'' as matched_fp
FROM       td_WageReqBenDet inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid INNER JOIN
td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
                    
                    INNER JOIN
            dbo.tr_BenWagesPaymentNotPaid ON tr_BenWagesPaymentNotPaid.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPaymentNotPaid.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPaymentNotPaid.intWorkid=td_WageReqBenDet.intWorkid

			where tr_BenWagesPaymentNotPaid.intWorkid like @intWorkActivityId and td_WorkWageRequest.intWorkid like @intWorkActivityId and 
			td_WageReqBenDet.intWorkid like @intWorkActivityId and Convert(varchar(11),td_WorkWageRequest.ReqFromDate,106)=Convert(varchar(11),@FromDate,106) and Convert(varchar(11),td_WorkWageRequest.ReqToDate,106)=Convert(varchar(11),@ToDate,106)










--and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b where (AttendanceDate between @FromDate and @ToDate ))
--and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
--and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
--and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenDet as ab where ab.intWagePaymentReqid like @intWagePaymentReqid)
--and tbl_BeneficiaryDet.intBenid in (select ab.intBenid from td_WageReqBenPADet as ab where ab.intWagePaymentReqPAid like intWagePaymentReqPAid)
--and td_WorkWageRequestPA.intWagePaymentReqPAid like @intWagePaymentReqPAid
--and td_WageReqBenPADet.intWagePaymentReqPAid like @intWagePaymentReqPAid
order by EnrollmentNo asc
end


