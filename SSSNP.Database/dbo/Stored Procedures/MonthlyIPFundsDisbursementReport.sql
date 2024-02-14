CREATE Procedure [dbo].[MonthlyIPFundsDisbursementReport](@Year int,
@Month int)
as

select ip.intIP as ID ,ip.IPName as Name,isnull(Sum(fa.PaidAmount),0) as FundsDisbursement,
isnull(sum(details.BeneficiaryAttendance ),0) as BeneficiaryAttendance ,
isnull(sum(details.WagesUSD),0) as WagesUSD ,
isnull(sum(details.WagesSSP ),0) as WagesSSP 
from td_IPDetails ip
left join td_ProjectDet pd
on ip.intIP =pd.intIP 
left join tbl_WorkMaster wm
on pd.intPrjid  =wm.intPrjid 
left join tr_FinancialApproval fa
on wm.intWorkid=fa.intWorkId 
left join (select bw.intWorkid,isnull(Sum(bw1.WagesSSP),0) WagesSSP,isnull(Sum(bw1.WagesUSD),0) WagesUSD,
isnull(sum(ba1.BeneficiaryAttendance ) ,0) BeneficiaryAttendance from tr_BenfWorkDet bw 
inner join (select bwp.intBenid,isnull(sum(bwp.PaidAmount),0) as WagesSSP,isnull(sum(bwp.PaidAmountUSD),0) as WagesUSD from
tr_BenWagesPayment bwp
group by bwp.intBenid) bw1
on bw.intBenid =bw1.intBenid 
 inner join 
(select ba.intBenid,isnull(count(ba.AttendanceDate ),0) BeneficiaryAttendance from  
tr_BenAttendanceDet ba
where ba.Status ='Present'  group by ba.intBenid) ba1
on bw.intBenid=ba1.intBenid
group by bw.intWorkid) details
on wm.intWorkid =details.intWorkid 


where   DATEPART(yy, ip.Created_dt ) = @Year AND DATEPART(mm, ip.Created_dt ) = @Month
group by ip.intIP ,ip.IPName



