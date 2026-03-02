CREATE Procedure[dbo].[MonthlyBenefficiaryFundsDisbursementReport](@intBenid int
)
as

select  bw.intWorkid as ID,isnull(bd.NameofHousehold,'')   as Name ,
0 as FundsDisbursement,
ba1.BeneficiaryAttendance as BeneficiaryAttendance,
bw1.WagesSSP ,
bw1.WagesUSD  from tr_BenfWorkDet bw 
inner join (select bwp.intBenid,isnull(sum(bwp.PaidAmount),0) as WagesSSP,isnull(sum(bwp.PaidAmountUSD),0) as WagesUSD from
tr_BenWagesPayment bwp
group by bwp.intBenid) bw1
on bw.intBenid =bw1.intBenid 
 inner join 
(select ba.intBenid,isnull(count(ba.AttendanceDate ),0) BeneficiaryAttendance from  
tr_BenAttendanceDet ba
where ba.Status ='Present'  group by ba.intBenid) ba1
on bw.intBenid=ba1.intBenid
left join tbl_BeneficiaryDet bd
 on bw.intBenid =bd.intBenid 
 where bw.intWorkid  =@intBenid 



