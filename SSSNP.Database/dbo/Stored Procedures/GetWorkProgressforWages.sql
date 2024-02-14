--created by siva as 13-1-2016              
--Purpose: to get Work Proposals ByID                 
                  
CREATE Procedure [dbo].[GetWorkProgressforWages]                  
(                  
@intWorkActivityId varchar(10)                  
)                  
as begin                  
                  SELECT     tm_Bomas.intBomasid, tm_Bomas.BomaName, tm_WorkActivities.WorkActivity, tm_AreaofWork.AreaofWork, tbl_WorkMaster.WorkCode, 
                      tbl_WorkProgress.WorkStartDate, tr_ProposedWorkAct.WorkName, td_ProjectDet.PrjName
FROM         tm_Bomas INNER JOIN
                      tbl_WorkProgress INNER JOIN
                      tbl_WorkMaster ON tbl_WorkProgress.intWorkid = tbl_WorkMaster.intWorkid ON tm_Bomas.intBomasid = tbl_WorkMaster.intBomasid INNER JOIN
                      tr_ProposedWorkAct ON tbl_WorkMaster.intWorkid = tr_ProposedWorkAct.intWorkid INNER JOIN
                      tm_AreaofWork ON tr_ProposedWorkAct.intAreaofWork = tm_AreaofWork.intAreaofWork INNER JOIN
                      tm_WorkActivities ON tm_AreaofWork.intAreaofWork = tm_WorkActivities.intAreaofWork INNER JOIN
                      td_ProjectDet ON tbl_WorkMaster.intPrjid = td_ProjectDet.intPrjid
                      where  tbl_WorkMaster.intWorkid like @intWorkActivityId             
               
        
                  
end




