CREATE Procedure [dbo].[YearwiseDashboard]  
(  
@Year varchar(10)  
)  
as begin  
select 'No. of Implementing Partners' as [Profession],COUNT(distinct intIP) as cnt  from td_IPDetails  
where (YEAR(Created_dt) = @Year)   
  
  
Union  
  
select 'No. of Implementing Counties' as [Profession],COUNT(distinct intCountieid) as cnt  from tbl_WorkMaster   
where (YEAR(Created_dt) = @Year)   
  
  
Union  
  
select 'No. of Implementing Payams' as [Profession],COUNT(intPayamid) as cnt  from tbl_WorkMaster  
where (YEAR(Created_dt) = @Year)   
  
  
Union  
  
select 'Number of Implementing Bomas' as [Profession],COUNT(intBomasid) as cnt  from tbl_WorkMaster  
where (YEAR(Created_dt) = @Year)  
  
Union  
  
select 'Total no. of works proposed' as [Profession],COUNT(distinct intWorkid) as cnt  from tbl_WorkMaster  
where (YEAR(Created_dt) = @Year)   
  
  
  
Union  
  
select 'Total no.of works approved' as [Profession],COUNT(distinct intWorkid) as cnt  from tbl_WorkMaster  
where (YEAR(Created_dt) = @Year) and (Status like 'Approved' or Status like 'Inprogress' or Status like 'Invoice Request' or Status like 'Invoice Appovals' or Status like 'Closed' )  
  
  
Union  
  
select 'No. of works in progress' as [Profession],COUNT(distinct intWorkid) as cnt  from tbl_WorkMaster  
where (YEAR(Created_dt) = @Year) and Status like 'Inprogress'   
  
  
Union  
  
select 'No. of works completed' as [Profession],COUNT(distinct intWorkid) as cnt  from tbl_WorkMaster  
where (YEAR(Created_dt) = @Year) and Status like 'Closed'   
  
--Union  
  
--select 'Funds disbursed' as [Profession],sum(distinct tr_FinancialApproval.PaidAmount) as cnt  from tr_FinancialApproval  
--where (YEAR(Created_dt) = @Year)  
  
   
 Union  
  
select 'Direct Beneficiaries' as [Profession],COUNT(distinct tbl_BeneficiaryDet.intBenid) as cnt  from tbl_BeneficiaryDet  
where (YEAR(Created_dt) = @Year)  
  
   
  
  
Union  
  
select 'Pecentage of Female Beneficiaries' as [Profession],COUNT(distinct tbl_BeneficiaryDet.intBenid) as cnt  from tbl_BeneficiaryDet  
where (YEAR(Created_dt) = @Year) and Sex like 'F'  
  
  Union  
  
select 'Pecentage of Male Beneficiaries' as [Profession],COUNT(distinct tbl_BeneficiaryDet.intBenid) as cnt  from tbl_BeneficiaryDet  
where (YEAR(Created_dt) = @Year) and Sex like 'M'  
  
  
Union  
  
select 'No. of work days created' as [Profession],COUNT(distinct tr_BenAttendanceDet.intBenid) as cnt  from tr_BenAttendanceDet  
where (YEAR(Created_dt) = @Year)   
end




