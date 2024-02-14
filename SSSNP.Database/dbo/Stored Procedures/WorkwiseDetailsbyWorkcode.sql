CREATE Procedure [dbo].[WorkwiseDetailsbyWorkcode]  
@Workcode varchar(200)  
as begin  
  
  
select   tbl_WorkMaster.intWorkid,tbl_WorkProgress.WorkStartDate,'' as Workenddate,  
  
(select  COUNT(distinct tbl_BeneficiaryDet.intBenid) from   
  
 tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid  ) as [NoofBenificiaries],  
  
(select  COUNT(distinct tbl_BeneficiaryDet.intBenid) from   
  
 tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid  and tbl_BeneficiaryDet.Sex like 'F' ) as [NoofFemaleBenificiaries],  
   
 (select  COUNT(distinct tbl_BeneficiaryDet.intBenid) from   
  
 tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid  and tbl_BeneficiaryDet.Sex like 'M' ) as [NoofMaleBenificiaries],  
   
    
    
  (select COUNT (intAttendanceid)  from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid  from tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid) and Status like 'Present') as [NoofWorkingdays],  
  
  (select COUNT (intAttendanceid)  from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid  from tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid and Sex='M') and Status like 'Present') as [NoofWorkingMaledays],  
  (select COUNT (intAttendanceid)  from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid  from tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid and Sex='F') and Status like 'Present') as [NoofWorkingFeMaledays],  
   
   
 (select SUM(PaidAmount)  from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid   from   
 tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid)) as [TotalWageAmount],
 
 (select SUM(PaidAmount)  from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid   from   
 tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid and Sex='M')) as [TotalMaleWageAmount]    ,
  
  (select SUM(PaidAmount)  from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid   from   
 tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid   where tr_BenfWorkDet.intWorkid=tbl_WorkMaster.intWorkid and Sex='F')) as [TotalFeMaleWageAmount]    
  
  
from tbl_WorkMaster left outer join tbl_WorkProgress 
on tbl_WorkMaster.intWorkid=tbl_WorkProgress.intWorkid  
where tbl_WorkMaster.WorkCode like @Workcode  
  
end  
  
--select * from tbl_WorkProgress




