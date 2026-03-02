CREATE Procedure [dbo].[getwageCumupativeReportbyBoma] --getwageCumupativeReport  '1001','2016'
@workcode varchar(8),
@Year varchar(8)

as begin

select distinct

tbl_WorkMaster.WorkCode,tr_BenWagesPayment.Year, UPPER(CASE tr_BenWagesPayment.Month
     WHEN 1 THEN 'January'
     WHEN 2 THEN 'February'
     WHEN 3 THEN 'March'
     WHEN 4 THEN 'April'
     WHEN 5 THEN 'May'
     WHEN 6 THEN 'June'
     WHEN 7 THEN 'July'
     WHEN 8 THEN 'August'
     WHEN 9 THEN 'September'
     WHEN 10 THEN 'October'
     WHEN 11 THEN 'November'
     WHEN 12 THEN 'December'
     
  END) as Month,tr_BenWagesPayment.Month,

(select COUNT(intAttendanceid) from tr_BenAttendanceDet  where intBenid in (select b.intBenid  from tbl_BeneficiaryDet as b inner join tr_BenfWorkDet as a on a.intBenid=b.intBenid  where  a.intWorkid=tbl_WorkMaster.intWorkid and  Sex like 'M')) as [Male],

(select COUNT(intAttendanceid) from tr_BenAttendanceDet  where intBenid in (select b.intBenid  from tbl_BeneficiaryDet as b inner join tr_BenfWorkDet as a on a.intBenid=b.intBenid  where  a.intWorkid=tbl_WorkMaster.intWorkid and  Sex like 'F')) as [FeMale],

(select SUM(PaidAmount) from tr_BenWagesPayment  where intBenid in (select b.intBenid  from tbl_BeneficiaryDet as b inner join tr_BenfWorkDet as a on a.intBenid=b.intBenid  where  a.intWorkid=tbl_WorkMaster.intWorkid and  Sex like 'M')) as [Male Wages],

(select SUM(PaidAmount) from tr_BenWagesPayment  where intBenid in (select b.intBenid  from tbl_BeneficiaryDet as b inner join tr_BenfWorkDet as a on a.intBenid=b.intBenid  where  a.intWorkid=tbl_WorkMaster.intWorkid and  Sex like 'F')) as [FeMale Wages]

  from   tbl_WorkMaster  inner join tr_BenfWorkDet on tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenfWorkDet.intBenid

inner join tr_BenWagesPayment on tr_BenWagesPayment.intBenid=tbl_BeneficiaryDet.intBenid

where tbl_WorkMaster.intBomasid like @workcode 
--and tr_BenWagesPayment.Year like @Year 
order by tr_BenWagesPayment.Month
end



