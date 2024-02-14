











CREATE VIEW [dbo].[Vw_WageInvoiceNewLatest]
AS
SELECT DISTINCT 
                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
                      dbo.tr_BenWagesPayment.AttendanceDays AS cnt,tr_BenWagesPayment.PaidAmountUSD  AS Amt,tr_BenWagesPayment.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
				case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else  case when (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) <>0 then (select alternate_name from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) else  'Paid Person details is not available in MIS due to alternative not registered.' end end end end end as PaidToName,
						 
					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto,
					case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then 'Alternate' else  case when (select count(*)  from tbl_BenfMorethanAlternateDet where alternate_number=tr_BenWagesPayment.Paidto) <>0 then 'Alternate' else  '' end end end end end as PaidToType
					,tbl_BeneficiaryDet.household_number,tr_BenWagesPayment.matched_fp
                    ,tbl_BeneficiaryDet.SelectionCriteria as Component
FROM       td_WageReqBenDet inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid INNER JOIN
td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
                    
                   INNER JOIN
            dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPayment.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPayment.intWorkid=td_WageReqBenDet.intWorkid
UNION
SELECT DISTINCT 
                     dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.ContactNo,  tbl_BeneficiaryDet.NameofHousehold,
                      dbo.tr_BenWagesPaymentNotPaid.AttendanceDays AS cnt,tr_BenWagesPaymentNotPaid.PaidAmountUSD  AS Amt,tr_BenWagesPaymentNotPaid.PaidAmount  AS Amt1,td_WorkWageRequest.intWorkid,
				case when 	tr_BenWagesPaymentNotPaid.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPaymentNotPaid.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPaymentNotPaid.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPaymentNotPaid.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else '' end end end end as PaidToName,
						 
					case when 	tr_BenWagesPaymentNotPaid.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPaymentNotPaid.Paidto,
					case when 	tr_BenWagesPaymentNotPaid.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPaymentNotPaid.Paidto then   'Household' else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPaymentNotPaid.Paidto then 'Alternate' else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPaymentNotPaid.Paidto then 'Alternate' else '' end end end end as PaidToType
					,tbl_BeneficiaryDet.household_number,'' as matched_fp
                    ,tbl_BeneficiaryDet.SelectionCriteria as Component
FROM       td_WageReqBenDet inner join  dbo.tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=td_WageReqBenDet.intBenid INNER JOIN
td_WorkWageRequest on td_WorkWageRequest.intWagePaymentReqid= td_WageReqBenDet.intWagePaymentReqid
                    
                    INNER JOIN
            dbo.tr_BenWagesPaymentNotPaid ON tr_BenWagesPaymentNotPaid.uuid=td_WageReqBenDet.intWagePaymentReqid and tr_BenWagesPaymentNotPaid.intBenid=td_WageReqBenDet.intBenid and tr_BenWagesPaymentNotPaid.intWorkid=td_WageReqBenDet.intWorkid











