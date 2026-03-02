CREATE Procedure [dbo].[TypesofWorkswiseReport] -- exec TypesofWorkswiseReport  '%','%'                        
     ( @intTST varchar(15),                            
@intIP  varchar(15)                    
)        
                      
as begin  

Create table #tblWork
(
intWorkid varchar(8),
intAreaofWork varchar(8),
IPid varchar(8),
intTSTid	varchar(8),
Status varchar(20)
)  
INSERT INTO #tblWork
select     
  tr_ProposedWorkAct.intWorkid, tr_ProposedWorkAct.intAreaofWork,  tbl_WorkMaster.IPid,tbl_WorkMaster.intTSTid, tbl_WorkMaster.Status                     
                            FROM tr_ProposedWorkAct          
                            inner join             
                                          
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId 
							    where   tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST  
							   

create table #tblBendet
(
intBenid varchar(8),
intWorkid varchar(8),
intAreaofWork varchar(8),
IPid varchar(8),
intTSTid varchar(8),
Sex varchar(1)
)
INSERT INTO #tblBendet
SELECT      tbl_BeneficiaryDet.intBenid,tbl_WorkMaster.intWorkid,   tr_ProposedWorkAct.intAreaofWork,tbl_WorkMaster.IPid, tbl_WorkMaster.intTSTid,tbl_BeneficiaryDet.Sex                     
                            FROM tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid         
                            inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid           
                             inner join tr_ProposedWorkAct on  tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid 
							 where   tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST   
							 
							 
							 

create table #tblAttendance
(
IPid varchar(8),
intTSTid varchar(8),
Attendance int
)
INSERT INTO #tblAttendance
SELECT   tbl_WorkMaster.IPid,tbl_WorkMaster.intTSTid,
(sum(isnull(tr_BenWagesPayment.AttendanceDays,0)))                      
                            FROM tr_BenWagesPayment     
                            inner join    tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenWagesPayment.intWorkid    
                                                            
                            WHERE       
                             tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST        
                            group by   tbl_WorkMaster.IPid,tbl_WorkMaster.intTSTid
						                           
                                 
        
SELECT DISTINCT tm_AreaofWork.AreaofWork,tm_AreaofWork.intAreaofWork,              
                                                    
                                    
                                      
                                      
                              (SELECT     COUNT( a.intWorkid) AS Expr1                          
                      from    #tblWork  as a                        
                            WHERE              
                             a.intAreaofWork =tm_AreaofWork.intAreaofWork        
                                     
                             and a.IPid like @intIP 
                             and a.intTSTid like @intTST        
                             )                          
                                     
                                                      
                              AS TotalWorkProposed ,          
                                      
                                      
                                      
                           (SELECT     COUNT( a.intWorkid) AS Expr1                          
                      from    #tblWork  as a                        
                            WHERE              
                             a.intAreaofWork =tm_AreaofWork.intAreaofWork        
                                     
                             and a.IPid like @intIP 
                             and a.intTSTid like @intTST        
                             and a.Status like 'New Proposal'        
                                
                             )                          
                                                      
                              AS TotaltobeApproved ,        
                              
                             (SELECT     COUNT( a.intWorkid) AS Expr1                          
                      from    #tblWork  as a                        
                            WHERE              
                             a.intAreaofWork =tm_AreaofWork.intAreaofWork        
                                     
                             and a.IPid like @intIP 
                             and a.intTSTid like @intTST         
                             and (a.Status like 'Inprogress' or a.Status like 'Invoice Appovals' or  a.Status like 'Invoice Request' or a.Status like 'Approved' )                          
                             and a.IPid like @intIP and a.intTSTid like @intTST        
                             )                          
                                                      
                              AS TotalInprogress ,          
                              
                                      
 --                 (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                          
 --                           FROM tr_ProposedWorkAct          
 --                           inner join             
                                          
 --                              dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                         
 --                           WHERE              
 --                            tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork        
                                     
 --                            and tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST        
 --                            )   
                               
 --                            -              (SELECT     COUNT(distinct tr_ProposedWorkAct.intWorkid) AS Expr1                          
 --                           FROM tr_ProposedWorkAct          
                                    
 --                           inner join             
                                          
 --                              dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkId                         
 --                           WHERE              
 --                            tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork         
 --                            and tbl_WorkMaster.Status like 'Inprogress' and tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST        
 --)                          
                                                      
 --                             AS TotalInprogress ,          
                                         
                                        
                                      
                              
                             (SELECT     COUNT( a.intWorkid) AS Expr1                          
                      from    #tblWork  as a                        
                            WHERE              
                             a.intAreaofWork =tm_AreaofWork.intAreaofWork        
                                            
                             and a.Status like 'Completed'         
                      and a.IPid like @intIP and a.intTSTid like @intTST        
 )                          
                                                      
                              AS TotalClosed ,        
                       
                             (SELECT     COUNT( a.intWorkid) AS Expr1                          
                      from    #tblWork  as a                        
                            WHERE              
                             a.intAreaofWork =tm_AreaofWork.intAreaofWork        
                                          
                             and a.Status like 'Rejected'         
                      and a.IPid like @intIP and a.intTSTid like @intTST        
 )                          
                                                      
                              AS TotalRejected ,        
                        
                        
                        
                               (SELECT     COUNT( tr_FinancialApproval.intWorkId) AS Expr1                          
                            FROM tr_FinancialApproval          
                            inner join             
                                          
                               dbo.tbl_WorkMaster  on tbl_WorkMaster.intWorkid=tr_FinancialApproval.intWorkId                         
                               inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork         
                               and tr_ProposedWorkAct.intWorkid=tbl_WorkMaster.intWorkid    
                               where     
                                tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST        
                                                                   
                            )                          
                                                      
                              AS TotalFundsDisbursed,        
                                      
                              (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM #tblBendet as b  
                            WHERE      (b.intAreaofWork =tm_AreaofWork.intAreaofWork)        
                            and b.IPid like @intIP and b.intTSTid like @intTST        
                                    
                            )                          
                                                      
                              AS TotalBeneficiaries ,        
                                      
                                      
                                (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM #tblBendet as b  
                            WHERE      (b.intAreaofWork =tm_AreaofWork.intAreaofWork)        
                            and b.IPid like @intIP and b.intTSTid like @intTST        
                            and b.Sex like 'F'        
                            and b.IPid like @intIP and b.intTSTid like @intTST        
                                    
                            )                          
                                                      
                              AS TotalFemaleBeneficiaries,  
                                
                                
                             (SELECT     COUNT(distinct b.intBenid) AS Expr1                          
                            FROM #tblBendet as b  
                            WHERE      (b.intAreaofWork =tm_AreaofWork.intAreaofWork)        
                            and b.IPid like @intIP and b.intTSTid like @intTST      and  
                             b.Sex like 'M'        
                            and b.IPid like @intIP and b.intTSTid like @intTST        
                                    
                            )                          
                                                      
                              AS TotalmaleBeneficiaries,        
                                     
                                     
                                  isnull(  ( select sum(c.Attendance)                      
                          from #tblAttendance as c   
                             where c.IPid like @intIP and c.intTSTid like @intTST        
                               
                            ) ,0)AS TotalWorkDays      
                                      
                            --  (SELECT     COUNT(distinct tr_BenAttendanceDet.intBenid) AS Expr1                          
                            --FROM tr_BenAttendanceDet          
                            --  inner join tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid        
                            --  inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tbl_BeneficiaryDet.WorkCode          
                            -- inner join tr_ProposedWorkAct on tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork         
                            --WHERE      (tr_ProposedWorkAct.intAreaofWork =tm_AreaofWork.intAreaofWork)        
                            --and (tr_BenAttendanceDet.Status='Present') and tbl_WorkMaster.IPid like @intIP and tbl_WorkMaster.intTSTid like @intTST        
                            -- )                          
                                                      
                            --  AS TotalWorkDays        
                                        
                                           
                                            
FROM                                   
      dbo.tm_AreaofWork        
                              
                             
                       --where td_IPDetails.intTST like @intTST  and  td_IPDetails.intIP like @intIP       
					   
					   Drop table #tblWork
					   Drop table #tblAttendance
					   Drop table #tblBendet       
  end




