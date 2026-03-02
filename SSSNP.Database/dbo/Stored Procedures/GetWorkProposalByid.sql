--created by siva as 04-1-2016            
--Purpose: to get Work Proposals                
                
CREATE procedure [dbo].[GetWorkProposalByid]                
(                
@intWorkid varchar(10)                
)                
as begin                
                
select *,td_TstTeamDet.intTST,td_TstTeamDet.TstName,td_TstTeamDet.Email as TSTemail,td_IPDetails.Email as IPemaill 
		
from dbo.tbl_WorkMaster
inner join td_TstTeamDet on td_TstTeamDet.intTST=tbl_WorkMaster.intTSTid     
inner join td_IPDetails on td_IPDetails.intIP=tbl_WorkMaster.IPid    
where intWorkid like @intWorkid            
            
            
         
         
   SELECT  tr_ProposedWorkAct.EquipmntQuantity,tr_ProposedWorkAct.MaterialQuantity, tm_WorkActivities.WorkActivity,  tm_AreaofWork.AreaofWork, intWorkActivityId, intWorkid, WorkName, tr_ProposedWorkAct.intAreaofWork, tr_ProposedWorkAct.intWorkActivity,
    EstManPower, EstDays, EstEquipmnts, EstMaterial, EstSkilLabour,        
                      CONVERT(VARCHAR(10),tr_ProposedWorkAct.EstWorkStartDate,101) as EstWorkStartDate ,             
                     CONVERT(VARCHAR(10),tr_ProposedWorkAct.EstWorkEndDate,101) as  EstWorkEndDate, EstCost, SubWorkCode 
                     ,tr_ProposedWorkAct.WorkCode,tr_ProposedWorkAct.Latitude,tr_ProposedWorkAct.Longitude,
					 tr_ProposedWorkAct.intWorkActivityId
					 --,tbl_Attachements.PhotoFileName
					 
FROM         tr_ProposedWorkAct    
 
inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork      
inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity
--left join tbl_Attachements on tbl_Attachements.intTypeId=tr_ProposedWorkAct.intWorkid
   
       where tr_ProposedWorkAct.intWorkid like  @intWorkid
	         
             
   select * from dbo.tr_MemsInvolved where MemType like 'TST' and intWorkid like @intWorkid          
             
   select * from dbo.tr_MemsInvolved where MemType like 'CA' and intWorkid like @intWorkid          
             
   select * from dbo.tr_MemsInvolved where MemType like 'PDC' and intWorkid like @intWorkid          
   select * from dbo.tr_MemsInvolved where MemType like 'BDC' and intWorkid like @intWorkid  
   
    SELECT  tbl_Attachements.PhotoFileName
					 --tbl_Attachements.PhotoFileName as EquipmentPhoto,
					 --tbl_Attachements.PhotoFileName as MaterialsPhoto,tbl_Attachements.PhotoFileName as WorkPhoto
FROM         tr_ProposedWorkAct    
 

left join tbl_Attachements on tbl_Attachements.intTypeId=tr_ProposedWorkAct.intWorkid
   
       where tr_ProposedWorkAct.intWorkid like  @intWorkid
           
                
end



