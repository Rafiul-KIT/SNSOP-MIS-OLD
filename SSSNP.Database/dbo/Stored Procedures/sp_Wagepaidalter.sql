CREATE procedure [dbo].[sp_Wagepaidalter]
@intIP varchar(10),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@intWorkid varchar(10),
@Household varchar(80),
@Enrollment varchar(30)
as
begin
select  ip.IPname, states.StateName, countie.CountieName,payam.PayamName,bom.BomaName,bom.intBomasid, w.intWorkid,w.WorkCode,wr.ReqNo,   b.EnrollmentNo, b.NameofHousehold,b.household_number, wp.uuid, wp.FromDate,  wp.intBenid,
 
 wp.AttendanceDays,wp.WageRateUSD,wp.PaidAmountUSD,wp.PaidAmount,wp.PaidTo
 
 
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
  select isnull(un.numbers,0) from 
  (
  select aa.household_number as numbers from tbl_BeneficiaryDet as aa 
  union 
   select aa1.alternate_number  as numbers from tbl_BeneficiaryDet as aa1 where aa1.alternate_number is not null
   union 
   select aa2.Secondalternate_number  as numbers from tbl_BeneficiaryDet as aa2 where aa2.Secondalternate_number is not null
   ) as un

  )
  --and b.household_number in (select dumpalternate.household_number from tbl_BenAlternateDump as dumpalternate group by dumpalternate.household_number having count(dumpalternate.household_number)>2)

  and ip.intIP like @intIP and states.intStateid like @intStateid and countie.intCountieid like @intCountieid and  payam.intPayamid like @intPayamid and bom.intBomasid like @intBomasid and w.intWorkid like @intWorkid 
  
  and wp.Paidto like '%'+@Household+'%' and  b.EnrollmentNo like '%'+@Enrollment+'%'
  end