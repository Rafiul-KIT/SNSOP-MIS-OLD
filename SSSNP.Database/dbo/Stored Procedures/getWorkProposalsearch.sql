CREATE procedure [dbo].[getWorkProposalsearch]                    
(                    
@intStateid varchar(10),                    
@intCountieid varchar(10),                    
@intPayamid varchar(10),                    
@intBomasid varchar(10),                  
@WorkCode varchar(50),                
@IPid varchar(50),            
@intTSTid  varchar(50) ,          
@Status varchar(50) 
      
)                    
as begin                    
                    
                    
                    
                    
SELECT distinct td_IPDetails.IPName ,tbl_WorkMaster.Status, tbl_WorkMaster.intWorkid, tbl_WorkMaster.WorkCode,  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, tm_States.StateName, tbl_WorkMaster.intWorkid, tbl_WorkMaster.MeetingDate,    


        
         
                      tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status                    
FROM         tbl_WorkMaster INNER JOIN                    
                      tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid      
inner join tm_Counties  on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid      
inner join tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid      
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  inner join   
td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid     
left outer join  tr_ProposedWorkAct on tbl_WorkMaster.intWorkid=tr_ProposedWorkAct.intWorkid 
         
                                          
                      where tbl_WorkMaster.intStateid like @intStateid and tbl_WorkMaster.intCountieid like @intCountieid and tbl_WorkMaster.intPayamid like @intPayamid                    
                      and tbl_WorkMaster.intBomasid  like @intBomasid   and tbl_WorkMaster.intTSTid like @intTSTid and tbl_WorkMaster.IPid like @IPid  and tbl_WorkMaster.Status like @Status 
                      and (tbl_WorkMaster.WorkCode like '%'+@WorkCode+'%' )
					
                      --or tr_ProposedWorkAct.WorkCode like  '%'+@WorkCode+'%') 
             
                    
end



