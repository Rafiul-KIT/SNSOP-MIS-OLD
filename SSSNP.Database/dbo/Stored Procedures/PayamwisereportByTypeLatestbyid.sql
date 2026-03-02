CREATE Procedure [dbo].[PayamwisereportByTypeLatestbyid]                  
                  
(                  
@intTST varchar(15),                  
@intIP  varchar(15),
@intCountieid varchar(8)                  
)                  
as begin                  
         
                  
                  
SELECT DISTINCT  tm_Payams.PayamName,tm_Payams.intPayamid,td_IPDetails.intIP,td_IPDetails.IPName,      
                                            
                            
                                
                                
                              (SELECT     COUNT(*) AS Expr1                  
                            FROM          dbo.tbl_WorkMaster                  
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                            and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                            )                  
                                              
                              AS TotalWorkProposed ,  
                                                       
                                 
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                  
                            FROM tr_ProposedWorkAct  
                            inner join     
                                  
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                 
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                             and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                             and tr_ProposedWorkAct.intAreaofWork like '1000')                  
                                              
                              AS Type1 ,  
                                
                                
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                  
                            FROM tr_ProposedWorkAct  
                            inner join     
                                  
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                 
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                             and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                             and tr_ProposedWorkAct.intAreaofWork like '1001')                  
                                              
                              AS Type2,  
                                
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                  
                            FROM tr_ProposedWorkAct  
                            inner join     
                                  
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                 
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                             and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                            
                             and tr_ProposedWorkAct.intAreaofWork like '1002')                  
                                              
                              AS Type3 ,  
                                
                               (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                  
                            FROM tr_ProposedWorkAct  
                            inner join     
                                  
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                 
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                             and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                             and tr_ProposedWorkAct.intAreaofWork like '1003')                  
                                              
                              AS Type4 ,  
                                
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                  
                            FROM tr_ProposedWorkAct  
                            inner join     
                                  
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                 
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                             and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                             and tr_ProposedWorkAct.intAreaofWork like '1004')                  
                                              
                              AS Type5,  
                                
                                 
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                  
                            FROM tr_ProposedWorkAct  
                            inner join     
                                  
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                 
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
                             and  (tbl_WorkMaster.intPayamid=tm_Payams.intPayamid)
                            
                             and tr_ProposedWorkAct.intAreaofWork like '1005')                  
                                              
                              AS Type6   
                                
                                   
                                    
FROM                           
                      dbo.tm_Payams  
                      inner join tbl_WorkMaster on tbl_WorkMaster.intPayamid=tm_Payams.intPayamid 
                      
                      inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid
                      
                       where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP and tm_Payams.intCountieid like @intCountieid
                                        
                                        
end




