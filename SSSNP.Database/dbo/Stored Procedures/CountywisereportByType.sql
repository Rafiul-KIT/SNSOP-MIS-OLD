--exec CountywisereportByType '%','%'

CREATE Procedure [dbo].[CountywisereportByType]                      
                      
(                      
@intTST varchar(15),                      
@intIP  varchar(15)                     
)                      
as begin                      
             
                      
                      
SELECT DISTINCT  c.CountieName,c.intCountieid,ips.intIP,ips.IPName,          
                                                
                                
                                    
                                    
                              (SELECT     COUNT(tbl_WorkMaster.intWorkid) AS Expr1                      
                            FROM          dbo.tbl_WorkMaster                      
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid))                      
                                                  
                              AS TotalWorkProposed ,      
                                                           
                                     
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                      
                            FROM tr_ProposedWorkAct      
                            inner join        
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid) and tr_ProposedWorkAct.intAreaofWork like '1000')                      
                                                  
                              AS Type1 ,      
                                    
                                    
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                      
                            FROM tr_ProposedWorkAct      
                            inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid) and tr_ProposedWorkAct.intAreaofWork like '1001')                      
                                                  
                              AS Type2,      
                                    
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                      
                            FROM tr_ProposedWorkAct      
                            inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid) and tr_ProposedWorkAct.intAreaofWork like '1002')                      
                                                  
                              AS Type3 ,      
                                    
                               (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                      
                            FROM tr_ProposedWorkAct      
                            inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid) and tr_ProposedWorkAct.intAreaofWork like '1003')                      
                                                  
                              AS Type4 ,      
                                    
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                      
                            FROM tr_ProposedWorkAct      
         inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid) and tr_ProposedWorkAct.intAreaofWork like '1004')                      
                                                  
                              AS Type5,      
                                    
                                     
                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                      
                            FROM tr_ProposedWorkAct      
                            inner join         
                                      
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                     
                            WHERE      (tbl_WorkMaster.IPid = ips.intIP and tbl_WorkMaster.intCountieid=c.intCountieid) and tr_ProposedWorkAct.intAreaofWork like '1005')                      
                                                  
                              AS Type6       
                                    
                                       
                                        
FROM                               
                      dbo.tm_Counties  as c    
                      inner join tbl_WorkMaster as w on w.intCountieid=c.intCountieid     
                          
                      inner join td_IPDetails as ips on ips.intIP=w.IPid    
                          
                       where ips.intTST like @intTST  and  ips.intIP like @intIP    
                                            
                                            
end




