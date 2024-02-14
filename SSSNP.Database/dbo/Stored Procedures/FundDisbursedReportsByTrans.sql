--exec FundDisbursedReportsByTrans '%','%'
CREATE Procedure [dbo].[FundDisbursedReportsByTrans]                      
                      
(                      
@intTST varchar(15),                      
@intIP  varchar(15)                     
)                      
as begin                      
             
                      
                      
SELECT DISTINCT td_IPDetails.intIP,td_IPDetails.IPName,          
                                                
                                   
                                              
                                
                                    
                              (SELECT     COUNT(*) AS Expr1                      
                            FROM          dbo.tbl_WorkMaster                      
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP))                      
                                                  
                              AS TotalWorkProposed ,      
                                        
                                        
                            (SELECT     COUNT(*) AS Expr1                      
                            FROM          dbo.tbl_WorkMaster                      
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and (tbl_WorkMaster.Status <> 'New Proposal' and tbl_WorkMaster.Status <>'Rejected'))                      
                                                  
                              AS TotalApproved ,      
                                    
                                  
                                   (SELECT     COUNT(*) AS Expr1                      
                            FROM          dbo.tbl_WorkMaster                      
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and (tbl_WorkMaster.Status like 'New Proposal' ))                      
                                                  
                              AS TotalTobeApproved ,      
                                    
                               isnull( (SELECT     sum(tr_FinancialApproval.PaidAmount) AS Expr1                      
                            FROM tr_FinancialApproval      
                            inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)    
                                
                            )   ,0)                   
                                                  
                              AS TotalReleaseFundsAmt   ,
                              
                                
                           --isnull(
                            (SELECT     count(tr_FinancialApproval.intFundApproval) AS Expr1                      
                            FROM tr_FinancialApproval      
                            inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)    
                                
                            )   
                            --,0)                   
                                                  
                              AS TotalFundsTrans             
                                     
                                  
                                        
FROM                               
                      dbo.td_IPDetails   where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP          
                                            
                                            
end




