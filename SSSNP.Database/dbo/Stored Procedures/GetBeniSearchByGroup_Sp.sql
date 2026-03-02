--created by Saubick as 13-1-2016                  
--Purpose: to get Work Proposals ByID                     
                      
CREATE Procedure [dbo].[GetBeniSearchByGroup_Sp]                      
(                      
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100)                       
)                      
as begin                      
                      
   
                        
                      SELECT  tm_Bomas.intBomasid, td_ProjectDet.PrjName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,
					  tm_Bomas.BomaName,  tbl_WorkMaster.intTSTid, tbl_WorkMaster.WorkCode, tbl_WorkMaster.IPid, tbl_WorkMaster.TypeofMeeting, 
					  tbl_WorkMaster.MeetingDate,   
                      tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status, tr_ProposedWorkAct.WorkName, tr_ProposedWorkAct.intWorkActivity, tr_ProposedWorkAct.EstDays,   
                      tr_ProposedWorkAct.EstManPower, tr_ProposedWorkAct.EstEquipmnts, tr_ProposedWorkAct.EstMaterial, tr_ProposedWorkAct.EstSkilLabour,   
                      tr_ProposedWorkAct.EstWorkStartDate, tr_ProposedWorkAct.EstWorkEndDate, tr_ProposedWorkAct.EstCost,tbl_WorkMaster.intWorkid,
					  tbl_BeneficiaryDet.GroupId,tbl_BeneficiaryDet.BeneficiaryType  
                      
FROM         tbl_WorkMaster left outer JOIN  
                      tr_ProposedWorkAct ON tbl_WorkMaster.intWorkid = tr_ProposedWorkAct.intWorkid inner join  
                      td_ProjectDet on td_ProjectDet.intPrjid=tbl_WorkMaster.intPrjid inner join   
                      tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid inner join   
                      tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid inner join   
                      tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid inner join   
                      tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid   
					 left join tbl_BeneficiaryDet on
						 tbl_WorkMaster.intTSTid = tbl_BeneficiaryDet.intIP  
                      where tbl_WorkMaster.intWorkid like @intWorkActivityId  
					  and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
	and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)

                        
                        
                        
                   
        SELECT  tm_WorkActivities.WorkActivity,  tm_AreaofWork.AreaofWork, intWorkActivityId, intWorkid, WorkName, 
		tr_ProposedWorkAct.intAreaofWork, tr_ProposedWorkAct.intWorkActivity, EstManPower, EstDays, EstEquipmnts, EstMaterial, EstSkilLabour,       

 
                      CONVERT(VARCHAR(10),tr_ProposedWorkAct.EstWorkStartDate,101) as EstWorkStartDate ,             
                     CONVERT(VARCHAR(10),tr_ProposedWorkAct.EstWorkEndDate,101) as  EstWorkEndDate, EstCost, SubWorkCode      
FROM         tr_ProposedWorkAct       
inner join tm_AreaofWork on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork      
inner join tm_WorkActivities on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity 
left join tbl_BeneficiaryDet on
						 tr_ProposedWorkAct.intWorkActivityId = tbl_BeneficiaryDet.intIP      
       where tr_ProposedWorkAct.intWorkid like  @intWorkActivityId 
	   and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
	and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
    
                      
end




