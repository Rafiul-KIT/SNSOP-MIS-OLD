
CREATE  PROCEDURE[dbo].[MonthlyBenefficiaryFundsDisbursementReportAutoAlert](@intBenid varchar(8)
)
as

select  bw.intWorkid as ID,isnull(bd.NameofHousehold,'')   as Name ,
0 as FundsDisbursement,
bw1.AttendanceDays as BeneficiaryAttendance,
bw1.WagesSSP ,
bw1.WagesUSD  from tr_BenfWorkDet bw 
inner join (select bwp.intBenid,isnull(sum(bwp.PaidAmount),0) as WagesSSP,isnull(sum(bwp.PaidAmountUSD),0) as WagesUSD,isnull(sum(bwp.AttendanceDays),0)  as AttendanceDays  from
tr_BenWagesPayment bwp
where MONTH(bwp.PaidDate)=MONTH(getdate()) and YEAR(bwp.PaidDate)=YEAR(getdate())
group by bwp.intBenid) bw1
on bw.intBenid =bw1.intBenid 

left join tbl_BeneficiaryDet bd
 on bw.intBenid =bd.intBenid 
 where bw.intWorkid like @intBenid




