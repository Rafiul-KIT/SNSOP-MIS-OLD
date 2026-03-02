
CREATE  PROCEDURE [dbo].[MonthlyIPFundsDisbursementReportAutoAlert](@Year int,
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
isnull(sum(bw1.AttendanceDays ) ,0) BeneficiaryAttendance from tr_BenfWorkDet bw 
inner join (select bwp.intBenid,isnull(sum(bwp.PaidAmount),0) as WagesSSP,isnull(sum(bwp.PaidAmountUSD),0) as WagesUSD,isnull(sum(bwp.AttendanceDays),0) as AttendanceDays from
tr_BenWagesPayment bwp
where MONTH(bwp.PaidDate)=12

--MONTH(getdate()) 

and 

YEAR(bwp.PaidDate)=2018



--YEAR(getdate())
group by bwp.intBenid) bw1
on bw.intBenid =bw1.intBenid 
 
group by bw.intWorkid) details
on wm.intWorkid =details.intWorkid 


where  


 DATEPART(yy, ip.Created_dt ) = 2018
 --@Year 
 
 AND DATEPART(mm, ip.Created_dt ) = 12
 --@Month
group by ip.intIP ,ip.IPName




