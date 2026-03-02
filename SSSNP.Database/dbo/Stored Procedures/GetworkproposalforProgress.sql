                    
                      
CREATE Procedure [dbo].[GetworkproposalforProgress]                      
(                      
	@intWorkActivityId varchar(10)                      
)                      
as begin                      
                    
SELECT  
	tm_Bomas.intBomasid
	,td_ProjectDet.PrjName
	,tm_States.StateName
	,tm_Counties.CountieName
	,tm_Payams.PayamName
	,tm_Bomas.BomaName
	,tbl_WorkMaster.intTSTid
	,tbl_WorkMaster.WorkCode
	,tbl_WorkMaster.IPid
	,tbl_WorkMaster.TypeofMeeting
	,tbl_WorkMaster.MeetingDate
	,tbl_WorkMaster.PDC_ApprvDate
	,tbl_WorkMaster.Status
	,tr_ProposedWorkAct.WorkName
	,tr_ProposedWorkAct.intWorkActivity
	,tr_ProposedWorkAct.EstDays
	,tr_ProposedWorkAct.EstManPower
	,tr_ProposedWorkAct.EstEquipmnts
	,tr_ProposedWorkAct.EstMaterial
	,tr_ProposedWorkAct.EstSkilLabour
	,tr_ProposedWorkAct.EstWorkStartDate
	,tr_ProposedWorkAct.EstWorkEndDate
	,tr_ProposedWorkAct.EstCost
	,tbl_WorkMaster.intWorkid
	,TDIP.IPName 
	,tm_States.intStateid 
	,tm_Counties.intCountieid    
	,wpc.WagePaymentCycle
FROM         
	tbl_WorkMaster 
	left outer JOIN  tr_ProposedWorkAct 
		ON tbl_WorkMaster.intWorkid = tr_ProposedWorkAct.intWorkid 
	inner join td_ProjectDet 
		on td_ProjectDet.intPrjid=tbl_WorkMaster.intPrjid 
	inner join tm_States 
		on tm_States.intStateid=tbl_WorkMaster.intStateid 
	inner join tm_Counties 
		on tm_Counties.intCountieid=tbl_WorkMaster.intCountieid 
	inner join tm_Payams 
		on tm_Payams.intPayamid=tbl_WorkMaster.intPayamid 
	inner join tm_Bomas 
		on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid  
	join td_IPDetails TDIP
		on  tbl_WorkMaster.IPid = TDIP.intIP
	left join td_WorkWageRequest wwr on tbl_WorkMaster.intWorkid=wwr.intWorkid
	left join tm_WagePaymentCycle wpc on wwr.PaymentCycle=wpc.intPaymentCycleID
where 
	tbl_WorkMaster.intWorkid like @intWorkActivityId  
                        
SELECT
	tm_WorkActivities.WorkActivity
	,tm_AreaofWork.AreaofWork
	,intWorkActivityId
	,intWorkid, WorkName
	,tr_ProposedWorkAct.intAreaofWork
	,tr_ProposedWorkAct.intWorkActivity
	,EstManPower
	,EstDays
	,EstEquipmnts
	,EstMaterial
	,EstSkilLabour
	,CONVERT(VARCHAR(10),tr_ProposedWorkAct.EstWorkStartDate,101) as EstWorkStartDate 
	,CONVERT(VARCHAR(10),tr_ProposedWorkAct.EstWorkEndDate,101) as  EstWorkEndDate
	,EstCost
	,SubWorkCode      
FROM         
		tr_ProposedWorkAct       
	inner join tm_AreaofWork 
		on tm_AreaofWork.intAreaofWork=tr_ProposedWorkAct.intAreaofWork      
	inner join tm_WorkActivities 
		on tm_WorkActivities.intWorkActivity=tr_ProposedWorkAct.intWorkActivity      
where 
	tr_ProposedWorkAct.intWorkid like  @intWorkActivityId     
           
declare  @intWorkProgres int
set @intWorkProgres=
	(
	select intWorkProgres 
	from tbl_WorkProgress 
	where intWorkid=@intWorkActivityId
	)

select  
	a.PhotoFileName as DocumentName
	,a.File_Path as WorkPhoto
	,a.Type as Status
	,a.intTypeId as intWorkid
	,a.IntWorkDetailsTypeId as IntWorkDetailsTypeId
	,a.Created_by as Created_by
	,a.Created_dt as Created_dt from tbl_Attachements a 
where   
	IntWorkDetailsTypeId= @intWorkProgres 
	and Type='WorkProgress'    
end