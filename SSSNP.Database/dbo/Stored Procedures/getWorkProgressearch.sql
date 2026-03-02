CREATE Procedure [dbo].[getWorkProgressearch]                          
(                          
@intStateid varchar(10),                          
@intCountieid varchar(10),                          
@intPayamid varchar(10),                          
@intBomasid varchar(10),                        
@WorkCode varchar(50),                      
@IPid varchar(50),                  
@intTSTid  varchar(50)
                       
)                          
as begin                          
                        
        
SELECT distinct td_IPDetails.IPName, tbl_WorkMaster.Status,  tbl_WorkMaster.intWorkid, 
tm_States.StateName, tm_Counties.CountieName, tm_Bomas.BomaName, tm_Payams.PayamName, tbl_WorkMaster.WorkCode, 
td_ProjectDet.PrjName,td_ProjectDet.intIP          
FROM         td_ProjectDet INNER JOIN              
                      tbl_WorkMaster ON td_ProjectDet.intPrjid = tbl_WorkMaster.intPrjid INNER JOIN              
                      tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid inner join         
                      tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid inner join         
                      tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid inner join         
                      tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid     inner join    
                      td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid    
                          left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid     
						  

                       where 
					   tbl_WorkMaster.intWorkid <> 999 -- to exclude DIS project
					   and tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid                          
                      and tbl_WorkMaster.intBomasid  like @intBomasid and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid        
    --and (tbl_WorkMaster.Status like 'Inprogress' or tbl_WorkMaster.Status like 'Invoice Appovals' or tbl_WorkMaster.Status like 'Closed')            
    and (tbl_WorkMaster.Status not in( 'New Proposal','Rejected')) 
    and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%') 
    --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%') 
	 
    end




