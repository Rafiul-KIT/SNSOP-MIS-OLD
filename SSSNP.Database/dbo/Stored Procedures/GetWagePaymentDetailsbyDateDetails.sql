CREATE procedure [dbo].[GetWagePaymentDetailsbyDateDetails]  -- GetWageRequestbyIDPaymentInvoiceFullDetails '1015'
(
@intWorkid varchar(8),
@FromDate datetime,
@ToDate datetime
)
As
Begin

create table #tblPaymentsDet
(
intWorkid int,
intBenid int,
PaidDate datetime,
PaidAmount decimal(18,2),
PaidAmountUSD decimal(18,2),
Paidto varchar(300)

)
insert into #tblPaymentsDet
select intWorkid,intBenid,PaidDate,PaidAmount,PaidAmountUSD,Paidto from tr_BenWagesPayment where intWorkid like @intWorkid and PaidDate between @FromDate and @ToDate+1 and uuid is not null and paidto is not null

create table #tblBendet
(
intBenid int,
household_number varchar(300)
)
insert into #tblBendet
select intBenid,household_number from tbl_BeneficiaryDet where household_number is not null

select distinct a.*,bom.BomaName,payam.PayamName,countie.CountieName,bom.BomaName,
states.StateName,
p.PrjName,

(select count(wb.intBenid) from #tblPaymentsDet as wb where wb.intWorkid=a.intWorkid and wb.PaidDate between @FromDate and @ToDate+1) as PaidCount,


(select sum(wb.PaidAmountUSD) from #tblPaymentsDet as wb where wb.intWorkid=a.intWorkid and wb.PaidDate between @FromDate and @ToDate+1) as PaidAmount,


(select count(wb.intBenid) from #tblPaymentsDet as wb inner join #tblBendet as ben on ben.intBenid=wb.intBenid where wb.intWorkid=a.intWorkid and wb.PaidDate between @FromDate and @ToDate+1 and ben.household_number = wb.Paidto) as PaidActualHouseholds,

(select count(wb.intBenid) from #tblPaymentsDet as wb where wb.intWorkid=a.intWorkid and wb.PaidDate between @FromDate and @ToDate+1) -
(select count(wb.intBenid) from #tblPaymentsDet as wb inner join #tblBendet as ben on ben.intBenid=wb.intBenid where wb.intWorkid=a.intWorkid and wb.PaidDate between @FromDate and @ToDate+1 and ben.household_number = wb.Paidto)

 as PaidAlternateHouseholds,

(select sum(wb.PaidAmount) from #tblPaymentsDet as wb where wb.intWorkid=a.intWorkid and wb.PaidDate between @FromDate and @ToDate+1) as PaidAmountSSP


 from tbl_WorkMaster as a 
 inner join tm_Bomas as bom on bom.intBomasid=a.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
 inner join tm_States as states on states.intStateid=countie.intStateid
 inner join  td_ProjectDet as p on p.intPrjid=a.intPrjid
 
  where a.intWorkid like @intWorkid
  


  Drop table #tblPaymentsDet

End

