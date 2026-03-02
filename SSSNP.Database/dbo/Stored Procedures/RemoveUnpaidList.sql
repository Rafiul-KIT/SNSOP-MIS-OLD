CREATE procedure [dbo].[RemoveUnpaidList]

as
Begin
Delete from tr_BenWagesPaymentNotPaid
Delete from tr_BenWagesPayment where isnull(trans_id,0) not in (select a.intWagesReqBenTransid from td_WageReqBenDet as a) and trans_id is not null
insert into tr_BenWagesPaymentNotPaid
(
 intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,  Wagerate, WageRateUSD, PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, 
                         WageYear, AttendanceDays, trans_id, uuid

)
select  intBenid, YEAR(b.Created_dt), MONTH(b.Created_dt), null,Convert(decimal(18,2), WageRateUSDTotal * (case when ExchangeRateNew is null then b.Exchangerateattime else ExchangeRateNew end)), b.Created_by, getdate(), Convert(decimal(18,2),a.WageRateUSD*(case when ExchangeRateNew is null then b.Exchangerateattime else ExchangeRateNew end)), a.WageRateUSD, WageRateUSDTotal, b.ReqFromDate, b.ReqToDate, b.intWorkid, null, (case when ExchangeRateNew is null then b.Exchangerateattime else ExchangeRateNew end), 
                         YEAR(b.Created_dt), a.TotAttendance,   a.intWagesReqBenTransid, a.intWagePaymentReqid
 from td_WageReqBenDet as a inner join td_WorkWageRequest as b on b.intWagePaymentReqid=a.intWagePaymentReqid where intWagesReqBenTransid not in (select isnull(a.trans_id,0) from tr_BenWagesPayment as a)
 and b.Status in ('4','5','6')

 create table #tblHH
(
householdnumber varchar(100),
householdname varchar(240)
)
insert into #tblHH

 select isnull(un.numbers,0),isnull(un.name,'0') from 
  (
  select aa.household_number as numbers,aa.NameofHousehold as name from tbl_BeneficiaryDet as aa where aa.household_number is not null
  union 
   select aa1.alternate_number  as numbers,aa1.Name1stAlternatePayee as name  from tbl_BeneficiaryDet as aa1 where aa1.alternate_number is not null
   union 
   select aa2.Secondalternate_number  as numbers,aa2.Name2ndAlternatePayee as name  from tbl_BeneficiaryDet as aa2 where aa2.Secondalternate_number is not null
      union 
   select aa3.alternate_number  as numbers,aa3.alternate_name as name from tbl_BenfMorethanAlternateDet as aa3 where aa3.alternate_number is not null
   ) as un

   update a set a.paidtoname=b.householdname
   from tr_BenWagesPayment as a inner join #tblHH as b on b.householdnumber=a.paidto where a.paidto is not null and a.paidtoname is null 


   Drop table #tblHH
select 1
return 1
End