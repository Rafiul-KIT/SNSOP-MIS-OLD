
CREATE Procedure [dbo].[getLIPWWorkProposalData]          
 
as begin          
  select 
	st.intStateid as [State ID]
	,st.StateName as [State]
	,c.intCountieid as [County ID]
	,c.CountieName as [County]
	,p.intPayamid as [Payam ID]
	,p.PayamName as [Payam]
	,b.intBomasid as [Boma ID] 
	,b.BomaName as [Boma]
	,PrjName as [Work Name]
	,wm.WorkCode as [Work Activity Code]
	--,pwa.WorkCode as [Work Code]
	,AreaofWork as [Type of Work]
	,pwa.WorkName as [Work Activity]
	,EstManPower as [Estimated Manpower]
	,EstDays as [Estimated Man Days]
	,convert(varchar(10),EstWorkStartDate,101) as [Estimated Start Date]
	,convert(varchar(10),EstWorkEndDate,101) as [Estimated End Date]
	,convert(varchar(10),MeetingDate,101) as [Date submitted to TST]
	,convert(varchar(10),StatusDate,101) as [Status Date]
	,wm.Status as [Status]
	,Remarks as [Remarks]
from tbl_WorkMaster wm
			inner join td_ProjectDet proj
				on proj.intPrjid=wm.intPrjid
			inner join tr_ProposedWorkAct pwa 
				on pwa.intWorkid=wm.intWorkid
			inner join tm_AreaofWork aw
				on pwa.intAreaofWork=aw.intAreaofWork
			inner join tm_States st 
				on wm.intStateid=st.intStateid
			inner join tm_Counties c 
				on wm.intCountieid=c.intCountieid
			inner join tm_Payams p 
				on wm.intPayamid=p.intPayamid
			inner join tm_Bomas b 
				on wm.intBomasid=b.intBomasid
where 
		wm.Remarks not like '%DIS%'
	and wm.Remarks not like '%direct income%'

        
end