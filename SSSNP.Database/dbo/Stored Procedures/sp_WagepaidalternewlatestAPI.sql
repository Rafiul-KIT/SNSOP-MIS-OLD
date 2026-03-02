CREATE procedure [dbo].[sp_WagepaidalternewlatestAPI]

as
begin

create table #tblHH
(
householdnumber varchar(100)
)
insert into #tblHH

 select isnull(un.numbers,0) from 
  (
  select aa.household_number as numbers from tbl_BeneficiaryDet as aa 
  union 
   select aa1.alternate_number  as numbers from tbl_BeneficiaryDet as aa1 where aa1.alternate_number is not null
   union 
   select aa2.Secondalternate_number  as numbers from tbl_BeneficiaryDet as aa2 where aa2.Secondalternate_number is not null
   ) as un


create table #tblTotalHH
(
Benid int
)
insert into #tblTotalHH
  select cc.intBenid from tbl_BeneficiaryDet as cc where  cc.alternate_number is not null and cc.Secondalternate_number is not null








select  ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid, w.intWorkid,w.WorkCode,wr.ReqNo,   b.EnrollmentNo, b.NameofHousehold,b.household_number, wp.uuid,CONVERT(char(10), wp.FromDate,126)  as FromDate,  wp.intBenid,
 
 wp.AttendanceDays,wp.WageRateUSD,wp.PaidAmountUSD,wp.PaidAmount,wp.PaidTo,
 'Alternate 3 not registered in MIS' as Remarks
 
 
  from tr_BenWagesPayment as wp inner join tbl_BeneficiaryDet as b on wp.intBenid=b.intBenid
 inner join tbl_WorkMaster as w on w.intWorkid=wp.intWorkid 
 inner join tm_Bomas as bom on bom.intBomasid=w.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=w.IPid
 inner join td_WorkWageRequest as wr on wr.intWagePaymentReqid=wp.uuid
 --where ip.intIP=1005
  where w.WorkCode like '2018%' 
  and wp.PaidTo not in
  (
 select householdnumber from #tblHH as p

  )
  --and b.household_number in (select dumpalternate.household_number from tbl_BenAlternateDump as dumpalternate group by dumpalternate.household_number having count(dumpalternate.household_number)>2)

   and wp.intBenid  in
  (
 select tt.Benid from  #tblTotalHH as tt

  )
  union
  select  ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid, w.intWorkid,w.WorkCode,wr.ReqNo,   b.EnrollmentNo, b.NameofHousehold,b.household_number, wp.uuid, CONVERT(char(10), wp.FromDate,126)  as FromDate,  wp.intBenid,
 
 wp.AttendanceDays,wp.WageRateUSD,wp.PaidAmountUSD,wp.PaidAmount,wp.PaidTo,
 'Alternate not registered in MIS' as Remarks
 
 
  from tr_BenWagesPayment as wp inner join tbl_BeneficiaryDet as b on wp.intBenid=b.intBenid
 inner join tbl_WorkMaster as w on w.intWorkid=wp.intWorkid 
 inner join tm_Bomas as bom on bom.intBomasid=w.intBomasid
 inner join tm_Payams as payam on payam.intPayamid=bom.intPayamid
 inner join tm_Counties as countie on countie.intCountieid=payam.intCountieid
  inner join tm_States as states on states.intStateid=countie.intStateid
 inner join td_IPDetails as ip on ip.intIP=w.IPid
 inner join td_WorkWageRequest as wr on wr.intWagePaymentReqid=wp.uuid
 --where ip.intIP=1005
  where w.WorkCode like '2018%' 
  and wp.PaidTo not in
  (
 select householdnumber from #tblHH as p

  )
  --and b.household_number in (select dumpalternate.household_number from tbl_BenAlternateDump as dumpalternate group by dumpalternate.household_number having count(dumpalternate.household_number)>2)

 
  
  
   and wp.intBenid  not in
  (
 select tt.Benid from  #tblTotalHH as tt

  )

Drop table #tblHH
Drop table #tblTotalHH

  end