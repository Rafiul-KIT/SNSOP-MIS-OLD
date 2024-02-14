CREATE Procedure [dbo].[BomaWagespaidReportbylatestidby]                        
                        
(                        
@intTST varchar(15),                        
@intIP  varchar(15)                       
)                        
as begin                        
               
                        
                        
SELECT DISTINCT tm_Payams.intPayamid,tm_Payams.PayamName,tm_Counties.CountieName ,tm_Bomas.intBomasid,tm_Bomas.BomaName,           
                                                  
                                     
                                                
                                  
                                      
                              (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.intBomasid = dbo.tm_Bomas.intBomasid))                        
                                                    
                              AS TotalWorkProposed ,        
                              
                                                                 
                              (SELECT     COUNT(*) AS Expr1                        
                            FROM          dbo.tbl_WorkMaster                        
                            WHERE      (tbl_WorkMaster.intBomasid = dbo.tm_Bomas.intBomasid) and (Status like 'Inprogress' or Status like 'Invoice Appovals' or Status like 'Invoice Request'  or Status like 'Approved')  )                        
                                                    
                              AS TotalInprogress ,        
                                          
                                          
                                     
                                      
                      --(SELECT     COUNT(*) AS Expr1                        
                      --      FROM          dbo.tbl_WorkMaster                        
                      --      WHERE      (tbl_WorkMaster.intPayamid = dbo.tm_Payams.intPayamid))  -          (SELECT     COUNT(*) AS Expr1                        
                      --      FROM          dbo.tbl_WorkMaster                        
                      --      WHERE      (tbl_WorkMaster.intPayamid = dbo.tm_Payams.intPayamid) and (tbl_WorkMaster.Status like 'Inprogress'))                        
                                                    
                      --        AS TotalInprogress ,        
                                    
                              (SELECT     COUNT(distinct tbl_BeneficiaryDet.intBenid) AS Expr1                        
                            FROM tbl_BeneficiaryDet    inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid      
                              
                            WHERE      (tbl_WorkMaster.intBomasid = dbo.tm_Bomas.intBomasid)      
                                 
                            )                        
                                                    
                              AS TotalBeneficiaries ,      
                                   
                                   
                                   
                               isnull(  ( select MAX(a.c) from ( (SELECT    (COUNT( distinct tr_BenAttendanceDet.intAttendanceid))  AS c                        
                            FROM tr_BenAttendanceDet        
                              inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid     
                                  
                            WHERE      (tbl_BeneficiaryDet.intBomasid = dbo.tm_Bomas.intBomasid)     
                            and tr_BenAttendanceDet.status like 'Present'    
                            group by tr_BenAttendanceDet.intBenid      
                            )  ) as a                       
                                                    
                              ),0)AS TotalWorkDays ,     
                                    
                            --  (SELECT     COUNT(distinct tr_BenAttendanceDet.intBenid) AS Expr1                        
                            --FROM tr_BenAttendanceDet      
                            --inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid      
                                    
                            --WHERE      (tbl_BeneficiaryDet.intPayamid = dbo.tm_Payams.intPayamid)      
                            --and tr_BenAttendanceDet.Status like 'Present'      
                                  
                    -- )                        
                                                    
                            --  AS TotalWorkDays ,      
                                    
                              isnull((SELECT     sum(tr_BenWagesPayment.PaidAmount) AS Expr1                        
                            FROM tr_BenWagesPayment        
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid      
                             inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                      
                            WHERE      (tbl_WorkMaster.intBomasid = dbo.tm_Bomas.intBomasid)      
                                  
                            ),0)                        
                                                    
                              AS TotalWageAmt    ,
                                       
                              isnull((SELECT     sum(tr_BenWagesPayment.PaidAmountUSD) AS Expr1                        
                            FROM tr_BenWagesPayment        
                            inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenWagesPayment.intBenid      
                             inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid                      
                            WHERE      (tbl_WorkMaster.intBomasid = dbo.tm_Bomas.intBomasid)      
                                  
                            ),0)                        
                                                    
                              AS TotalWageAmt1      
                                    
                                          
FROM                                 
                      dbo.tm_Payams  inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid  
                      inner join tm_Bomas on tm_Bomas.intPayamid =tm_Payams.intPayamid
                        
                      where tm_Bomas.intBomasid in (SELECT    w.intBomasid                     
                            FROM          dbo.tbl_WorkMaster as w)  
                                              
                                              
end




