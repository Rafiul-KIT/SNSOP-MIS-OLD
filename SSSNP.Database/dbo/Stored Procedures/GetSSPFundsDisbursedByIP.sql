  CREATE Procedure [dbo].[GetSSPFundsDisbursedByIP](@id varchar(8))
as
 select w.WorkCode,tr_FinancialApproval.ApprovedDate ,tr_FinancialApproval.PaymentMode,
 tr_FinancialApproval.TransactionNo,tr_FinancialApproval.PaidAmount  from tr_FinancialApproval
 inner join tbl_WorkMaster w
 on tr_FinancialApproval.intWorkId=w.intWorkid
  where tr_FinancialApproval.intWorkId in 
  (select distinct tbl_BeneficiaryDet.WorkCode from tbl_BeneficiaryDet 
  inner join td_IPDetails on tbl_BeneficiaryDet.intIP= td_IPDetails.intIP where td_IPDetails.intIP= @id)



