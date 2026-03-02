CREATE Procedure [dbo].[IPwiseTypesofWorkreport]                    
                    
(                    
@intTST varchar(15),                    
@intIP  varchar(15)                   
)                    
as begin                    
           
Create table #WorkProposedActTable
(
IPid int,
intWorkId int,
intAreaofWork int

)
Insert into #WorkProposedActTable
select 
tbl_WorkMaster.IPid,tbl_WorkMaster.intWorkid,tr_ProposedWorkAct.intAreaofWork
from tr_ProposedWorkAct    
                            inner join       
                                    
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId 



                    
                    
SELECT DISTINCT td_IPDetails.intIP,td_IPDetails.IPName,        
                                              
                              
                                  
                                  
                              (SELECT     COUNT(*) AS Expr1                    
                            FROM          dbo.tbl_WorkMaster                    
                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP))                    
                                                
                              AS TotalWorkProposed ,    
                                                         
                                   
                              (SELECT     COUNT(distinct intWorkid) AS Expr1                    
                            FROM #WorkProposedActTable                
                            WHERE      (IPid = dbo.td_IPDetails.intIP) and intAreaofWork like '1000')                    
                                                
                              AS Type1 ,    
                                  
                                  
                              (SELECT     COUNT(distinct intWorkid) AS Expr1                    
                            FROM #WorkProposedActTable                
                            WHERE      (IPid = dbo.td_IPDetails.intIP) and intAreaofWork like '1001')                    
                                                
                              AS Type2,    
                                  
                              (SELECT     COUNT(distinct intWorkid) AS Expr1                    
                            FROM #WorkProposedActTable                
                            WHERE      (IPid = dbo.td_IPDetails.intIP) and intAreaofWork like  '1002')                    
                                                
                              AS Type3 ,    
                                  
                              (SELECT     COUNT(distinct intWorkid) AS Expr1                    
                            FROM #WorkProposedActTable                
                            WHERE      (IPid = dbo.td_IPDetails.intIP) and intAreaofWork like  '1003')                    
                                                
                              AS Type4 ,    
                                  
                              (SELECT     COUNT(distinct intWorkid) AS Expr1                    
                             FROM #WorkProposedActTable                
                            WHERE      (IPid = dbo.td_IPDetails.intIP) and intAreaofWork like '1004')                    
                                                
                              AS Type5,    
                                  
                                   
                             (SELECT     COUNT(distinct intWorkid) AS Expr1                    
                             FROM #WorkProposedActTable                
                            WHERE      (IPid = dbo.td_IPDetails.intIP) and intAreaofWork like  '1005')                    
                                                
                              AS Type6     
                                  
                                     
                                      
FROM                             
                      dbo.td_IPDetails   where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP   
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					  
					               
--SELECT DISTINCT td_IPDetails.intIP,td_IPDetails.IPName,        
                                              
                              
                                  
                                  
--                              (SELECT     COUNT(*) AS Expr1                    
--                            FROM          dbo.tbl_WorkMaster                    
--                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP))                    
                                                
--                              AS TotalWorkProposed ,    
                                                         
                                   
--                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                    
--                            FROM tr_ProposedWorkAct    
--                            inner join       
                                    
--                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                   
--                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tr_ProposedWorkAct.intAreaofWork like '1000')                    
                                                
--                              AS Type1 ,    
                                  
                                  
--                              (SELECT     COUNT( tr_ProposedWorkAct.intWorkid) AS Expr1                    
--                            FROM tr_ProposedWorkAct    
--                            inner join       
                                    
--                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                   
--                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tr_ProposedWorkAct.intAreaofWork like '1001')                    
                                                
--                              AS Type2,    
                                  
--                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                    
--                            FROM tr_ProposedWorkAct    
--                            inner join       
                                    
--                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                   
--                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tr_ProposedWorkAct.intAreaofWork like '1002')                    
                                                
--                              AS Type3 ,    
                                  
--                               (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                    
--                            FROM tr_ProposedWorkAct    
--                            inner join       
                                    
--                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                   
--                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tr_ProposedWorkAct.intAreaofWork like '1003')                    
                                                
--                              AS Type4 ,    
                                  
--                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                    
--                            FROM tr_ProposedWorkAct    
--                            inner join       
                                    
--                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                   
--                          WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tr_ProposedWorkAct.intAreaofWork like '1004')                    
                                                
--                              AS Type5,    
                                  
                                   
--                              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                    
--                            FROM tr_ProposedWorkAct    
--                            inner join       
                                    
--                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                   
--                            WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and tr_ProposedWorkAct.intAreaofWork like '1005')                    
                                                
--                              AS Type6     
                                  
                                     
                                      
--FROM                             
--                      dbo.td_IPDetails   where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP        
                                               
                                          
                                          
end




