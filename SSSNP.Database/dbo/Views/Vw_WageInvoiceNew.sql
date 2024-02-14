


CREATE VIEW [dbo].[Vw_WageInvoiceNew]
AS
SELECT DISTINCT 
                      dbo.td_WageReqBenDet.intWagesReqBenTransid, dbo.td_WorkWageRequestPA.intWagePaymentReqPAid, dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo, CASE WHEN td_WageReqBenDet.traineealternate IS NOT NULL 
                      THEN td_WageReqBenDet.traineealternate ELSE tbl_BeneficiaryDet.NameofHousehold END AS NameofHousehold, dbo.tbl_BeneficiaryDet.Sex, 
                      dbo.tbl_BeneficiaryDet.ContactNo, dbo.tbl_BeneficiaryDet.Email, dbo.tbl_BeneficiaryDet.FamilyIncome, dbo.tm_Bomas.BomaName, 
                      dbo.td_WageReqBenDet.TotAttendance AS cnt, dbo.td_WageReqBenDet.TotAttendance * 70 AS Amt,tr_BenfWorkDet.intWorkid,
				case when 	tr_BenWagesPayment.Paidto is null then '' else   	case when tbl_BeneficiaryDet.household_number=tr_BenWagesPayment.Paidto then   tbl_BeneficiaryDet.NameofHousehold else case when tbl_BeneficiaryDet.alternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name1stAlternatePayee else case when tbl_BeneficiaryDet.Secondalternate_number=tr_BenWagesPayment.Paidto then tbl_BeneficiaryDet.Name2ndAlternatePayee else tbl_BeneficiaryDet.NameofHousehold end end end end as PaidToName,
						 
					case when 	tr_BenWagesPayment.Paidto is null then 'Unpaid' else 	 'Paid' end as PayStatus, tr_BenWagesPayment.Paidto

FROM         dbo.tbl_BeneficiaryDet INNER JOIN
                      dbo.tr_BenfWorkDet ON dbo.tr_BenfWorkDet.intBenid = dbo.tbl_BeneficiaryDet.intBenid INNER JOIN
                      dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.tr_BenfWorkDet.intWorkid LEFT OUTER JOIN
                      dbo.tm_Bomas ON dbo.tm_Bomas.intBomasid = dbo.tbl_BeneficiaryDet.intBomasid INNER JOIN
                      dbo.td_WageReqBenDet ON dbo.td_WageReqBenDet.intBenid = dbo.tr_BenfWorkDet.intBenid AND 
                      dbo.td_WageReqBenDet.intWorkid = dbo.tr_BenfWorkDet.intWorkid INNER JOIN
                      dbo.td_WageReqBenPADet ON dbo.td_WageReqBenPADet.intWagePaymentReqid = dbo.td_WageReqBenDet.intWagePaymentReqid AND 
                      dbo.td_WageReqBenPADet.intBenid = dbo.td_WageReqBenDet.intBenid AND dbo.td_WageReqBenDet.intWorkid = dbo.td_WageReqBenPADet.intWorkid AND 
                      dbo.td_WageReqBenPADet.intBenid = dbo.tr_BenfWorkDet.intBenid AND dbo.td_WageReqBenPADet.intWorkid = dbo.tr_BenfWorkDet.intWorkid INNER JOIN
                      dbo.td_WorkWageRequestPA ON dbo.td_WorkWageRequestPA.intWagePaymentReqid = dbo.td_WageReqBenDet.intWagePaymentReqid
left outer join dbo.tr_BenWagesPayment ON tr_BenWagesPayment.uuid=td_WageReqBenDet.intWagePaymentReqid



