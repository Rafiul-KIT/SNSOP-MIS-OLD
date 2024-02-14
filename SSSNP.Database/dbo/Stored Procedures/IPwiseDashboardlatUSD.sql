-- Modify By :israr
--Modify date :29-07-2017
--purpose  :for total wages in USD 
--
CREATE Procedure [dbo].[IPwiseDashboardlatUSD]-- '%' 
  
@id varchar(8)  
as begin  
  
select  td_IPDetails.IPName,td_IPDetails.intIP,  
  
(select COUNT(distinct intCountieid)   from tbl_BeneficiaryDet  where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP ) as [NoofCounties],  
(select COUNT(distinct intPayamid)   from tbl_BeneficiaryDet  where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP ) as [NoofPayams],  
(select COUNT(distinct intBomasid)   from tbl_BeneficiaryDet  where tbl_BeneficiaryDet.intIP=td_IPDetails.intIP ) as [NoofBomas],  
(select COUNT(distinct WorkCode)   from tbl_WorkMaster  where tbl_WorkMaster.IPid=td_IPDetails.intIP ) as [NoofWorksProposed],  
(select COUNT(distinct tbl_WorkMaster.WorkCode)   from tbl_WorkMaster   where tbl_WorkMaster.IPid=td_IPDetails.intIP and tbl_WorkMaster.Status like 'New Proposal' ) as [NoofWorkstobeapproved],  
  
(select COUNT(distinct tbl_WorkMaster.WorkCode)   from tbl_WorkMaster   where tbl_WorkMaster.IPid=td_IPDetails.intIP and (tbl_WorkMaster.Status like 'InProgress' or  tbl_WorkMaster.Status  like 'Invoice Appovals' or tbl_WorkMaster.Status  like 'Invoice Request' or  tbl_WorkMaster.Status  like 'Approved' )
 ) as [NoofWorksinprogress],  
  
(select COUNT(distinct tbl_WorkMaster.WorkCode)   from tbl_WorkMaster   where tbl_WorkMaster.IPid=td_IPDetails.intIP  and tbl_WorkMaster.Status like 'Completed' ) as [NoofWorksclosed], 
(select COUNT(distinct tbl_WorkMaster.WorkCode)   from tbl_WorkMaster   where tbl_WorkMaster.IPid=td_IPDetails.intIP  and tbl_WorkMaster.Status like 'Rejected' ) as [NoofWorksRejected],  
  
(select SUM(PaidAmount)  from tr_FinancialApproval where intWorkId in (select distinct tbl_BeneficiaryDet.WorkCode from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP))  
 as [Fundsdisbursed],  
  
  
  
  (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                          
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid       
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid           
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP ))
                            
                             as [TotalBenificiaries],  
  
--( select COUNT(tbl_BeneficiaryDet.intBenid)   from tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid  where  tbl_BeneficiaryDet.intIP=td_IPDetails.intIP and tbl_BeneficiaryDet.Sex like 'F' ) 

 (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                          
                            FROM tbl_BeneficiaryDet       inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid    
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid           
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP   ) and tbl_BeneficiaryDet.Sex like 'F'     
                            --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')    
                            )    




as [TotalFemaleBenificiaries],  
--( select COUNT(tbl_BeneficiaryDet.intBenid)   from tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid  where  tbl_BeneficiaryDet.intIP=td_IPDetails.intIP and tbl_BeneficiaryDet.Sex like 'M' ) 

 (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                          
                            FROM tbl_BeneficiaryDet       inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid    
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid           
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP   ) and tbl_BeneficiaryDet.Sex like 'M'     
                            --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')    
                            ) 

as [TotalMaleBenificiaries],  
  
--(select COUNT(tr_BenAttendanceDet.intAttendanceid) from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP ) and Status like 'Present' ) 
  ( select  ISNULL(COUNT(DISTINCT ba.intAttendanceid), 0) AS Cnt
FROM         dbo.tr_BenAttendanceDet AS ba INNER JOIN
                      dbo.tbl_BeneficiaryDet AS a ON a.intBenid = ba.intBenid INNER JOIN
                      dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid
WHERE     (ba.AttendanceDate BETWEEN w.FromDate AND w.ToDate) and a.intIP=td_IPDetails.intIP AND (ba.Status = 'Present') )
as [NoofWorkdaysCreated],  
  
--(select COUNT(tr_BenAttendanceDet.intAttendanceid) from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP ) and Status like 'Present' ) as [NoofWorkdaysCreated],  
  
(select  sum(tr_BenWagesPayment.PaidAmount)   from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP) and statusforUSD <> 'USD' )    
  
as [TotalWageAmount],

(select  sum(tr_BenWagesPayment.PaidAmount)   from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP) and statusforUSD like 'USD' )    
  
as [TotalWageAmountUSd]    
  
  
from   
td_IPDetails where td_IPDetails.intIP like @id  
  
end




