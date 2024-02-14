CREATE Procedure [dbo].[TSTDashboard]                  
                  
(                  
@intTST varchar(15)                 
)                  
as begin                  
         
                  
                  
SELECT DISTINCT td_TstTeamDet.intTST,td_TstTeamDet.TstName,      
                                            
                               
                                          
                            (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster       
                                        
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST))                  
                                              
                              AS TotalWorkProposed ,                 
                                    (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST) and (tbl_WorkMaster.Status like 'New Proposal'))                  
                                              
                              AS TotaltobeApproved ,      
                                    
                                    
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST) and (tbl_WorkMaster.Status like 'Fund Allocation'))                  
                                              
                              AS TotaltobeFinApproved ,      
                                    
                                          
                                    (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST) and (tbl_WorkMaster.Status like 'Approved'))                  
                                              
                              AS TotalApproved ,      
                                    
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST) and (tbl_WorkMaster.Status like 'Inprogress'))                  
                                              
                              AS TotalInprogress,     
                                  
                                  
                                                                  
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST) and (tbl_WorkMaster.Status like 'Fund Released'))                  
                                              
                              AS FundReleased,      
                                    
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.intTSTid = dbo.td_TstTeamDet.intTST) and (tbl_WorkMaster.Status like 'Closed'))                  
                                              
                              AS TotalCompleted,      
                                    
                                    
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.td_IPDetails                  
                            WHERE      (td_IPDetails.intTST = dbo.td_TstTeamDet.intTST) )                  
                             
                              AS ToatlIPS,      
                                    
            (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.td_CountyAdmDet        
                            inner join tbl_Users on tbl_Users.intUserid=td_CountyAdmDet.Created_by      
                                            
                            WHERE      (tbl_Users.User_code=td_TstTeamDet.intTST) )                  
                                              
                              AS ToatlCAs,  
                                
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.td_PDCDet        
                            inner join tbl_Users on tbl_Users.intUserid=td_PDCDet.Created_by      
                                            
                            WHERE      (tbl_Users.User_code=td_TstTeamDet.intTST) )                  
                                              
                              AS ToatlPDC ,  
                                
                               (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.td_BDCDet        
                            inner join tbl_Users on tbl_Users.intUserid=td_BDCDet.Created_by      
                                            
                            WHERE      (tbl_Users.User_code=td_TstTeamDet.intTST) )                  
                                              
                              AS ToatlBDC      
                                    
                                    
FROM                           
             dbo.td_TstTeamDet   where td_TstTeamDet.intTST like @intTST        
                                        
                                        
end




