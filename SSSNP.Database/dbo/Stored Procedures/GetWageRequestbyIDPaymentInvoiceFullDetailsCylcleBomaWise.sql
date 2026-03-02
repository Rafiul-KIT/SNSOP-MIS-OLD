CREATE procedure [dbo].[GetWageRequestbyIDPaymentInvoiceFullDetailsCylcleBomaWise]  -- GetWageRequestbyIDPaymentInvoiceFullDetailsCylcleBomaWise '3138','7/12/2018','7/28/2018'
(
@intBomasid varchar(8),
@FromDate datetime,
@ToDate datetime
)
As
Begin
select distinct states.StateName, countie.CountieName ,payam.PayamName, boms.BomaName, work.intBomasid,a.ReqFromDate,a.ReqToDate,

(select count(distinct wb.intBenid) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106) ) as TotalCount,
(select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as PaidCount,
(select count(distinct wb.intBenid) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106))- (select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as UnpaidCount,
(select sum(wb.WageRateUSDTotal) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as ToalAmount,
(select sum(wb.PaidAmountUSD) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as PaidAmount,
(select sum(wb.WageRateUSDTotal) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) - (select sum(wb.PaidAmountUSD) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as UnPaidAmount,
(select Convert(varchar(11), Min(wb.Created_dt),106) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as paidDate,
(select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join tbl_BeneficiaryDet as ben on ben.intBenid=wb.intBenid inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106) and ben.household_number = wb.Paidto) as PaidActualHouseholds,
(select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join tbl_BeneficiaryDet as ben on ben.intBenid=wb.intBenid inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106) and ben.household_number <> wb.Paidto) as PaidAlternateHouseholds,

(select sum(wb.PaidAmount) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid inner join tbl_WorkMaster as works1 on works1.intWorkid=aa.intWorkid where works1.intBomasid=work.intBomasid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as PaidAmountSSP


 from td_WorkWageRequest as a 
 inner join tbl_WorkMaster as work on work.intWorkid=a.intWorkid
 inner join tm_Bomas as boms on boms.intBomasid=work.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=boms.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
  where work.intBomasid like @intBomasid
  
   and convert(varchar(11), a.ReqFromDate,106)=convert(varchar(11),@FromDate,106)
  and convert(varchar(11), a.ReqToDate,106)=convert(varchar(11),@ToDate,106)


  

End

