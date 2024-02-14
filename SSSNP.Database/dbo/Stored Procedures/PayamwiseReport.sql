CREATE Procedure [dbo].[PayamwiseReport]                        
                        
(                        
@intTST varchar(15),                        
@intIP  varchar(15)                       
)                        
as begin      
      
      
SELECT DISTINCT tm_Payams.PayamName,tm_Payams.intPayamid,td_IPDetails.intIP,td_IPDetails.IPName,            
                                                  
                                      
                                          
                              (SELECT     COUNT(distinct intBomasid) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                        
                                                    
                              AS NoofBomas,        
                                      
                                      
                              (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                        
                                                    
                              AS TotalWorkProposed ,        
                                          
                                          
                               (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and (tbl_WorkMaster.Status like 'New Proposal'))                        
                                                    
                              AS TotaltobeApproved ,        
                               (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or  tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' ))                          
                          AS TotalInprogress ,        
                                      
                       --(SELECT     COUNT(*) AS Expr1                        
                       --     FROM          dbo.tbl_WorkMaster                        
                       --     WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                        
                       --          -        (SELECT     COUNT(*) AS Expr1                        
                       --     FROM          dbo.tbl_WorkMaster                        
                       --     WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and (tbl_WorkMaster.Status like 'Inprogress'))                        
                                                    
                       --       AS TotalInprogress ,        
                                      
                                       
                              (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and (tbl_WorkMaster.Status like 'Completed'))                        
                                                    
                              AS TotalClosed ,        
                              
                                (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and (tbl_WorkMaster.Status like 'Rejected'))                        
                                                    
                              AS TotalRejected ,        
                                      
                                       
                              (SELECT     COUNT(distinct tr_FinancialApproval.intWorkId) AS Expr1                        
                            FROM tr_FinancialApproval        
                            inner join           
                                        
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId                       
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                        
                                                    
                              AS TotalFundsDisbursed ,        
                                      
                              (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                        
                            FROM tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid         
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)                        
                                                    
                              AS TotalBeneficiaries ,        
                        
                               (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                
                            FROM tbl_BeneficiaryDet   inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid        
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and tbl_BeneficiaryDet.Sex like 'F'  
                             --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')  
                             )                        
                                                    
                              AS TotalFemaleBeneficiaries,   
                                
                                
                                
                               (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                
                            FROM tbl_BeneficiaryDet     inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid         
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) and tbl_WorkMaster.intPayamid=tm_Payams.intPayamid and tbl_BeneficiaryDet.Sex like 'M'  
                             --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Closed')  
                             )                        
                                                    
                              AS TotalmaleBeneficiaries,        
                                      
                            --  (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                        
                            --FROM tbl_BeneficiaryDet        
                                    
                            --WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP) )                        
                                                    
                            --  AS TotalWorkDays        
                                
                            isnull(  ( select MAX(a.c) from ( (SELECT    (COUNT( distinct tr_BenAttendanceDet.intAttendanceid))  AS c                        
                            FROM tr_BenAttendanceDet        
                              inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid     
                                  
                            WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP     
                            and tbl_BeneficiaryDet.intPayamid=tm_Payams.intPayamid)     
                            and tr_BenAttendanceDet.status like 'Present'    
                            group by tr_BenAttendanceDet.intBenid      
                            )  ) as a                       
                                                    
                              ),0)AS TotalWorkDays     ,
                              
                               (select COUNT(tr_BenAttendanceDet.intAttendanceid) from tr_BenAttendanceDet where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid ) 
and Status like 'Present' ) as [NoofWorkdaysCreated],  
                              (select  sum(tr_BenWagesPayment.PaidAmount)   from tr_BenWagesPayment where intBenid in (select tbl_BeneficiaryDet.intBenid from tbl_BeneficiaryDet where tm_Payams.intPayamid=tbl_BeneficiaryDet.intPayamid  )  )      
    
as [TotalWageAmount]            
                                         
                                          
FROM                                 
                      dbo.tm_Payams  inner join tbl_WorkMaster on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid       
                            
                      inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid      
                            
                       where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP            
  end




