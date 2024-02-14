
                    
                      
CREATE Procedure [dbo].[GetworkproposalforProgressDIS]                      
(                      
	@intWorkActivityId varchar(10)                      
)                      
as begin                      
                    
SELECT  distinct
	td_ProjectDet.PrjName,tbl_WorkMaster.intTSTid
	,tbl_WorkMaster.WorkCode
	,tbl_WorkMaster.IPid
	,tbl_WorkMaster.TypeofMeeting
	,tbl_WorkMaster.MeetingDate
	,tbl_WorkMaster.PDC_ApprvDate
	,tbl_WorkMaster.Status
	,tbl_WorkMaster.intWorkid
	,TDIP.IPName 
	,wpc.WagePaymentCycle
FROM         
	tbl_WorkMaster 
	inner join td_ProjectDet 
		on td_ProjectDet.intPrjid=tbl_WorkMaster.intPrjid 
	inner join td_IPDetails TDIP
		on  tbl_WorkMaster.IPid = TDIP.intIP
	inner join td_WorkWageRequest wwr on tbl_WorkMaster.intWorkid=wwr.intWorkid
	inner join tm_WagePaymentCycle wpc on wwr.PaymentCycle=wpc.intPaymentCycleID
where 
	tbl_WorkMaster.intWorkid like @intWorkActivityId  
end