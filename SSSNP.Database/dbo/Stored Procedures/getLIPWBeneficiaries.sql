

CREATE Procedure [dbo].[getLIPWBeneficiaries]          
 
as begin          
  
drop table if exists #tempTotAttendance
	select 
		intbenid
		,intWorkActivityId 
		,COUNT(*) as totDays
		,min(attendancedate) as BenStartDate
		,max(attendancedate) as BenEndDate
	into #tempTotAttendance 
	from tr_BenAttendanceDet
	group by intBenid,intWorkActivityId

drop table if exists #tempPaid
	select 
		intbenid
		,intWorkid
		,sum(PaidAmountUSD) as totPaid
	into #tempPaid 
	from tr_BenWagesPayment p
	where intworkid<>999
	group by intBenid, intWorkid
	

drop table if exists #tempUnpaid
		select 
		intbenid
		,intWorkid
		,sum(WageRateUSDTotal) as totUnpaid
	into #tempUnpaid 
	from td_WageReqBenDet
	where intworkid<>999
	and intWagesReqBenTransid not in 
			(
				select 
				trans_id
				from tr_BenWagesPayment p
				where intworkid<>999
			)
	group by intBenid, intWorkid

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
	,AreaofWork as [Type of Work]
	,pwa.WorkName as [Work Activity]
	,NameofHousehold [Beneficiary's name]
	,household_number [HH Number]
	,totDays [Worked days] 
	,paid.totPaid [Paid Amount]
	,ISNULL( unpaid.totUnpaid ,0)[Unpaid Amount]
	,convert(varchar(10),totAttend.BenStartDate,101) [Start date]
	,convert(varchar(10),totAttend.BenEndDate,101)  [End date]
		
from tbl_WorkMaster wm
			inner join td_ProjectDet proj
				on proj.intPrjid=wm.intPrjid
			inner join tr_ProposedWorkAct pwa 
				on pwa.intWorkid=wm.intWorkid
			inner join tm_AreaofWork aw
				on pwa.intAreaofWork=aw.intAreaofWork
			inner join tr_BenfWorkDet bw
				on wm.intWorkid=bw.intWorkid
			inner join tbl_BeneficiaryDet ben
				on bw.intBenid=ben.intBenid
			inner join #tempTotAttendance totAttend 
				on pwa.intWorkid=totAttend.intWorkActivityId
					and ben.intBenid=totAttend.intBenid
			inner join #tempPaid paid
				on pwa.intWorkid=paid.intWorkid
					and ben.intBenid=paid.intBenid 
			left join #tempUnpaid unpaid
				on pwa.intWorkid=paid.intWorkid
					and ben.intBenid=unpaid.intBenid 
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

drop table if exists #tempTotAttendance
drop table if exists #tempPaid
drop table if exists #tempUnpaid

        
end