--exec WorkwiseStatusreport '%','%'  
CREATE Procedure [dbo].[WorkwiseStatusreport]                          
                          
(                          
@intTST varchar(15),                          
@intIP  varchar(15) ,
@intWorkid varchar(8)                        
)                          
as begin                          
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Show Wage payment with USD

-- Working Purpose : Show Wage payment with USD

-- Developer : Srikanth

-- Date: 04-08-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------              
 
 Create table #WorkTable
(
IPid int,
intWorkId int,
Status varchar(20),
intCountieid int,
intPayamid int,
intBomasid int
)
insert into #WorkTable
select IPid, intWorkid,Status,intCountieid,intPayamid,intBomasid from tbl_WorkMaster

create table #BenTable
(
intBenid	int,
intWorkid   int,
intIP int,
Sex varchar(8)
)
 insert into #BenTable
select distinct tbl_BeneficiaryDet.intBenid,tr_BenfWorkDet.intWorkid,tbl_BeneficiaryDet.intIP,tbl_BeneficiaryDet.Sex  FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid       
                            left outer join tbl_WorkMaster on tbl_WorkMaster.intWorkid= tr_BenfWorkDet.intWorkid  
                          
                          
SELECT DISTINCT td_IPDetails.intIP,td_IPDetails.IPName,              
                                                    
                                       
                                                  
                            (SELECT     COUNT(distinct a.intCountieid) AS Expr1                          
                            FROM          #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP))                          
                                                      
                              AS NoofCounties ,                         
                                                  
                                                  
                                    (SELECT     COUNT(distinct a.intPayamid) AS Expr1                          
                            FROM          #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP))                          
                                                      
                              AS NoofPayams ,              
                                            
                              (SELECT     COUNT(distinct a.intBomasid) AS Expr1                          
                            FROM          #WorkTable  as a                         
                            WHERE      (a.IPid  = dbo.td_IPDetails.intIP))                          
                                                      
                              AS NoofBomas,          
                                        
                                        
                              (SELECT     COUNT(*) AS Expr1                          
                            FROM         #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP))                          
                                                      
                              AS TotalWorkProposed ,          
                                            
                                            
                               (SELECT     COUNT(*) AS Expr1                          
                            FROM       #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP) and (a.Status like 'New Proposal'))                          
                                                      
                              AS TotaltobeApproved ,          
                                      
                                      (SELECT     COUNT(*) AS Expr1                          
                            FROM         #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP) and (a.Status like 'Invoice Appovals' or a.Status like 'InProgress' or a.Status like 'Invoice Request' or a.Status like 'Approved'  ))                          
                                                      
                              AS TotalInprogress ,          
                                
                            --    (SELECT     COUNT(*) AS Expr1                          
                            --FROM          dbo.tbl_WorkMaster                          
                            --WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP))     
                            ---    
                                        
                            --  (SELECT     COUNT(*) AS Expr1                          
                            --FROM          dbo.tbl_WorkMaster                          
                            --WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and (tbl_WorkMaster.Status like 'Inprogress'))                          
                                                      
                            --  AS TotalInprogress ,          
                                        
                                         
                              (SELECT     COUNT(*) AS Expr1                          
                 FROM         #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP) and (a.Status like 'Completed'))                          
                                                      
                              AS TotalClosed ,          
                              (SELECT     COUNT(*) AS Expr1                          
                 FROM         #WorkTable  as a                         
                            WHERE      (a.IPid = dbo.td_IPDetails.intIP) and (a.Status like 'Rejected'))                          
                                                      
                              AS TotalRejected ,          
                                        
                                         
          --                    (SELECT     COUNT(distinct tr_FinancialApproval.intWorkId) AS Expr1                          
          --                  FROM tr_FinancialApproval          
          --inner join             
                                          
          --                     dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId                         
          --                  WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP))                          
                                                      
          --                    AS TotalFundsDisbursed ,          
  
  
  --select * from tr_FinancialApproval 
  --where intworkid in 
  --(select distinct tbl_BeneficiaryDet.WorkCode from tbl_BeneficiaryDet 
  --where tbl_BeneficiaryDet.intIP ='1001')
  
           isnull( (select SUM(isnull(PaidAmount,0))   from tr_FinancialApproval where intWorkId in (select Distinct intWorkId  from #WorkTable where #WorkTable.IPid= td_IPDetails.intIP  )),0)
             --(select SUM(PaidAmount)  from tr_FinancialApproval where intWorkId in (select distinct tbl_BeneficiaryDet.WorkCode from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP))    
 as [TotalFundsDisbursed],    
  
  
  
                                
 --                             (select SUM(PaidAmount)  from tr_FinancialApproval where intWorkId in (select distinct tbl_BeneficiaryDet.WorkCode from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP))    
 --as [Fundsdisbursed],    
  
                                
                                
                                        
                              (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM #BenTable as b           
                            WHERE      (b.intIP = dbo.td_IPDetails.intIP and b.intWorkid like @intWorkid))                          
                                                      
                              AS TotalBeneficiaries ,          
                                        
                               (SELECT    COUNT(distinct b.intBenid) AS Expr1                          
                            FROM #BenTable as b           
                            WHERE      (b.intIP = dbo.td_IPDetails.intIP and b.intWorkid like @intWorkid) and b.Sex like 'F'     
                            --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')    
                            )                          
                                                      
                              AS TotalFemaleBeneficiaries,      
                                  
                              (SELECT    COUNT(distinct b.intBenid) AS Expr1                          
                            FROM #BenTable as b           
                            WHERE      (b.intIP = dbo.td_IPDetails.intIP and b.intWorkid like @intWorkid) and b.Sex like 'M'     
                            --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')    
                            )                          
                                                      
                              AS TotalmaleBeneficiaries,      
                                      
                   
                           ( select  ISNULL(SUM(isnull(w.AttendanceDays,0)), 0) AS Cnt
FROM         
                      dbo.tbl_BeneficiaryDet AS a  INNER JOIN
                      dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid
WHERE     a.intIP=td_IPDetails.intIP ) as [NoofWorkdaysCreated],  
                           isnull(   (select  sum(isnull(tr_BenWagesPayment.PaidAmount,0))   from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP)  )      ,0)
    
as [TotalWageAmount]    ,
      isnull((select  sum(isnull(tr_BenWagesPayment.PaidAmountUSD,0))   from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where td_IPDetails.intIP=tbl_BeneficiaryDet.intIP)  )      ,0)
    
as [TotalWageAmount1]  
  
          
                                           
                                            
FROM                                   
                      dbo.td_IPDetails   where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP       
					  
					  
					 
                                                
                                                
end




