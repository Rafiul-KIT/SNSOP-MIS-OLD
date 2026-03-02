CREATE Procedure [dbo].[CountiesWagespaidReport]                        
                        
(                        
@intTST varchar(15),                        
@intIP  varchar(15)                       
)                        
as begin                        
               
                        
                        
SELECT DISTINCT tm_Counties.intCountieid,tm_Counties.CountieName,            
                                                  
                                     
                                                
                                  
                                      
                              (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid))                        
                                                    
                              AS TotalWorkProposed ,        
                              
                              
                              (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid)and  (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or tbl_WorkMaster.Status like 'Invoice Request' or tbl_WorkMaster.Status like 'Approved' ) )                        
                                                    
                              AS TotalInprogress ,        
                                          
                                          
                                     
                                      
                                 
                            --  (SELECT     COUNT(*) AS Expr1                        
                            --FROM          dbo.tbl_WorkMaster                        
                            --WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid))    -     (SELECT     COUNT(*) AS Expr1                        
                            --FROM          dbo.tbl_WorkMaster                        
                            --WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid) and (tbl_WorkMaster.Status like 'Inprogress'))                        
                                                    
                            --  AS TotalInprogress ,        
                                    
                              (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                        
                            FROM tbl_BeneficiaryDet inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid        
                              
                            WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid )      
                                 
                            )                        
                                                    
                              AS TotalBeneficiaries ,      
                                   
                                   
                                   
                               isnull(  ( select MAX(a.c) from ( (SELECT    (COUNT( distinct tr_BenAttendanceDet.intAttendanceid))  AS c                        
                            FROM tr_BenAttendanceDet        
                              inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid     
                                  
                            WHERE      (tbl_BeneficiaryDet.intCountieid = dbo.tm_Counties.intCountieid)     
                            and tr_BenAttendanceDet.status like 'Present'    
                            group by tr_BenAttendanceDet.intBenid      
                            )  ) as a                       
                                                    
                              ),0)AS TotalWorkDays ,     
                                    
                            --  (SELECT     COUNT(distinct tr_BenAttendanceDet.intBenid) AS Expr1                        
                            --FROM tr_BenAttendanceDet      
                            --inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid      
                                    
                            --WHERE      (tbl_BeneficiaryDet.intCountieid = dbo.tm_Counties.intCountieid)      
                            --and tr_BenAttendanceDet.Status like 'Present'      
                                  
                            -- )                        
                                                    
                            --  AS TotalWorkDays ,      
                                    
                              isnull((SELECT     sum(tr_BenWagesPayment.PaidAmount) AS Expr1                        
                            FROM tr_BenWagesPayment        
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid      
                            inner join           
                                        
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tbl_BeneficiaryDet.WorkCode                       
                            WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid)      
                                  
                            ),0)                        
                                                    
                              AS TotalWageAmt  ,
                              isnull((SELECT     sum(tr_BenWagesPayment.PaidAmountUSD) AS Expr1                        
                            FROM tr_BenWagesPayment        
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid      
                            inner join           
                                        
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tbl_BeneficiaryDet.WorkCode                       
                            WHERE      (tbl_WorkMaster.intCountieid = dbo.tm_Counties.intCountieid)      
                                  
                            ),0)                        
                                                    
                              AS TotalWageAmt1      
                                    
                                          
FROM                                 
                      dbo.tm_Counties    
                        
                      where tm_Counties.intCountieid in (SELECT    w.intCountieid                     
                            FROM          dbo.tbl_WorkMaster as w)  
                                              
                                              
end




