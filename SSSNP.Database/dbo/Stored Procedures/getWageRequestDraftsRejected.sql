

CREATE procedure [dbo].[getWageRequestDraftsRejected]
(   
@draftCreatorId varchar(10),
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8)  ,
@ReqNo varchar(30)
)  
as  
begin  

select distinct
		wwr.intwagepaymentreqid
		, statename
		, countieName
		, payamName
		, BomaName
		, wwr.intworkid
		, wwr.reqno
		, wwr.reqfromdate	 
		, wwr.reqtodate
		, wwr.reqyear
		, wwr.reqmonth
		, wwr.status
		, wwr.created_by
		, wwr.created_dt
		, wwr.modified_by
		, wwr.modified_dt
		, wwr.component
		, pc.wagepaymentCycle as PaymentCycle
		, wm.workcode
from td_WorkWageRequest wwr 
	inner join tbl_WorkMaster wm
		on wwr.intWorkid=wm.intWorkid
	inner join td_WageReqBenDet wrbt 
		on wwr.intwagepaymentreqid= wrbt.intwagepaymentreqid
	inner join tbl_beneficiarydet ben
		on wrbt.intbenid=ben.intbenid
	inner join tm_states st
		on st.intstateid=ben.intstateid
		inner join tm_Counties co
		on ben.intcountieid=co.intcountieid
		inner join tm_payams p
		on ben.intpayamid=p.intpayamid
		inner join tm_bomas b
		on ben.intbomasid=b.intbomasid	
	left join tm_WagePaymentCycle pc
		on wwr.status=pc.intpaymentcycleid
where 
wwr.created_by = @draftCreatorId
and wwr.status=105
and wwr.ReqNo like @reqno
and wm.workcode like @workCode
and ben.intStateId like @intStateid
and ben.intip  like @IPid
and ben.intCountieId like @intCountieid
and ben.intPayamId like @intPayamid
and ben.intBomasId like @intBomasid

end