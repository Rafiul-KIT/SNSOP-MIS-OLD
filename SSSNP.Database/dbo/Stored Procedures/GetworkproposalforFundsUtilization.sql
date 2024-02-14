--created by siva as 13-1-2016                  
--Purpose: to get Work Proposals ByID                     
                      
CREATE Procedure  [dbo].[GetworkproposalforFundsUtilization]                     
(                      
@intWorkActivityId varchar(10)                      
)                      
as begin                      
                      
   
                        
                      SELECT  tm_Bomas.intBomasid, td_ProjectDet.PrjName,tm_States.StateName,tm_Counties.CountieName,tm_Payams.PayamName,tm_Bomas.BomaName,  tbl_WorkMaster.intTSTid, tbl_WorkMaster.WorkCode, tbl_WorkMaster.IPid, tbl_WorkMaster.TypeofMeeting, tbl_WorkMaster.MeetingDate,   
                      tbl_WorkMaster.PDC_ApprvDate, tbl_WorkMaster.Status, tr_ProposedWorkAct.WorkName, tr_ProposedWorkAct.intWorkActivity, tr_ProposedWorkAct.EstDays,   
                      tr_ProposedWorkAct.EstManPower, tr_ProposedWorkAct.EstEquipmnts, tr_ProposedWorkAct.EstMaterial, tr_ProposedWorkAct.EstSkilLabour,   
                      tr_ProposedWorkAct.EstWorkStartDate, tr_ProposedWorkAct.EstWorkEndDate, tr_ProposedWorkAct.EstCost,tbl_WorkMaster.intWorkid,
					  TDIP.IPName  
                      
FROM         tbl_WorkMaster left outer JOIN  
                      tr_ProposedWorkAct ON tbl_WorkMaster.intWorkid = tr_ProposedWorkAct.intWorkid inner join  
                      td_ProjectDet on td_ProjectDet.intPrjid=tbl_WorkMaster.intPrjid inner join   
                      tm_States on tm_States.intStateid=tbl_WorkMaster.intStateid inner join   
                      tm_Counties on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid inner join   
                      tm_Payams on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid inner join   
                      tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  
					  join td_IPDetails TDIP
					  on  tbl_WorkMaster.IPid = TDIP.intIP
                      where tbl_WorkMaster.intWorkid like @intWorkActivityId  
                        
                 
		   select  a.FundsUtilizationDate as FundsUtilizationDate,a.FundsUtilizationFor as FundsUtilizationFor,a.Amount as Amount,a.Remarks as Remarks from tbl_FundsUtilization a where   intWorkid like @intWorkActivityId   
end




