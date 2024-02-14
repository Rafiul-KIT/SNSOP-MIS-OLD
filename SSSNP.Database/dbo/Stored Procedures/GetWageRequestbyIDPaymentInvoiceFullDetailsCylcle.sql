CREATE procedure [dbo].[GetWageRequestbyIDPaymentInvoiceFullDetailsCylcle]  -- GetWageRequestbyIDPaymentInvoiceFullDetails '1015'
(
@intWorkActivityId varchar(8),
@FromDate datetime,
@ToDate datetime
)
As
Begin
select distinct a.intWorkid,a.ReqFromDate,a.ReqToDate,a.WageRateUSD,

(select count(distinct wb.intBenid) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106) ) as TotalCount,
(select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as PaidCount,
(select count(distinct wb.intBenid) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106))- (select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as UnpaidCount,
(select sum(wb.WageRateUSDTotal) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as ToalAmount,
(select sum(wb.PaidAmountUSD) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as PaidAmount,
(select sum(wb.WageRateUSDTotal) from td_WageReqBenDet as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.intWagePaymentReqid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) - (select sum(wb.PaidAmountUSD) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as UnPaidAmount,
(select Convert(varchar(11), Min(wb.Created_dt),106) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as paidDate,
(select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join tbl_BeneficiaryDet as ben on ben.intBenid=wb.intBenid inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106) and ben.household_number = wb.Paidto) as PaidActualHouseholds,
(select count(distinct wb.intBenid) from tr_BenWagesPayment as wb inner join tbl_BeneficiaryDet as ben on ben.intBenid=wb.intBenid inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106) and ben.household_number <> wb.Paidto) as PaidAlternateHouseholds,

(select sum(wb.PaidAmount) from tr_BenWagesPayment as wb inner join td_WorkWageRequest as aa on aa.intWagePaymentReqid=wb.uuid where aa.intWorkid=a.intWorkid and convert(varchar(11), aa.ReqFromDate,106)=convert(varchar(11), a.ReqFromDate,106) and convert(varchar(11), aa.ReqToDate,106)=convert(varchar(11), a.ReqToDate,106)) as PaidAmountSSP


 from td_WorkWageRequest as a 
  where a.intWorkid like @intWorkActivityId and convert(varchar(11), a.ReqFromDate,106)=convert(varchar(11),@FromDate,106)
  and convert(varchar(11), a.ReqToDate,106)=convert(varchar(11),@ToDate,106)


  

End

