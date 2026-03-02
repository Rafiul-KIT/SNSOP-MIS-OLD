
CREATE  PROCEDURE[dbo].[MonthlyWorkFundsDisbursementReportAutoAlert](@intWorkid varchar(8)
)
as

select wm.intWorkid as ID ,
--dbo.GetWorkNameByWorkCode(wm.intWorkid ) as Name  ,
wm.WorkCode as Name,isnull(Sum(fa.PaidAmount),0) as FundsDisbursement,
isnull(sum(details.BeneficiaryAttendance ),0) as BeneficiaryAttendance ,
isnull(sum(details.WagesUSD),0) as WagesUSD ,
isnull(sum(details.WagesSSP ),0) as WagesSSP 
from tbl_WorkMaster wm

left join tr_FinancialApproval fa
on wm.intWorkid=fa.intWorkId 
left join (select bw.intWorkid,isnull(Sum(bw1.WagesSSP),0) WagesSSP,isnull(Sum(bw1.WagesUSD),0) WagesUSD,
isnull(sum(bw1.AttendanceDays ) ,0) BeneficiaryAttendance from tr_BenfWorkDet bw 
inner join (select bwp.intBenid,isnull(sum(bwp.PaidAmount),0) as WagesSSP,isnull(sum(bwp.PaidAmountUSD),0) as WagesUSD,isnull(sum(bwp.AttendanceDays),0) as AttendanceDays from
tr_BenWagesPayment bwp
where MONTH(bwp.PaidDate)=MONTH(getdate()) and YEAR(bwp.PaidDate)=YEAR(getdate())
group by bwp.intBenid) bw1
on bw.intBenid =bw1.intBenid 
 
group by bw.intWorkid) details
on wm.intWorkid =details.intWorkid 
where wm.intPrjid like @intWorkid
group by wm.intWorkid ,wm.WorkCode




