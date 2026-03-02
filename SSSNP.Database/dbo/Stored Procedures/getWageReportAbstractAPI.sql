
CREATE procedure [dbo].[getWageReportAbstractAPI] -- '05/23/2016','05/30/2016',1015
(  
	@FromDate datetime,  
	@ToDate datetime,  
	@workCode varchar(120),
	@intStateid varchar(8),
	@intCountieid varchar(8),
	@intPayamid varchar(8),
	@intBomasid varchar(8) ,
	@IPid varchar(8)  
)  
as  
begin  
  
select      
	vw.IPName
	,vw.StateName
	,vw.CountieName
	,vw.PayamName
	,vw.BomaName
	,vw.WorkCode
	,vw.FromDate
	,vw.ToDate
	,vw.TotalMembers
	,vw.TotalWorkDays
	,vw.AmountPaidUSD
	,vw.AmountPaidSSP 
	,wpc.WagePaymentCycle as 'Payment Cycle'
from 
	Vw_WageAbstractAPINew vw
	inner join tbl_workmaster wm
		on vw.workcode= wm.WorkCode
	inner join td_workwagerequest wwr 
		on wwr.intWorkid=wm.intWorkid
	left join tm_WagePaymentCycle wpc 
		on wpc.intPaymentCycleID = wwr.PaymentCycle
where
	vw.intIP like @IPid 
	and vw.intStateid like @intStateid 
	and vw.intCountieid like @intCountieid 
	and vw.intPayamid like @intPayamid 
	and vw.intBomasid like @intBomasid 

order by 
	vw.IPName
	,vw.StateName
	,vw.CountieName
	,vw.PayamName
	,vw.BomaName
	,vw.WorkCode
	,vw.FromDate
	,vw.ToDate
	
select    
	count(distinct tr_BenWagesPayment.intBenid) as TotalMembers
	,sum(tr_BenWagesPayment.AttendanceDays) as TotalWorkDays
	,sum(tr_BenWagesPayment.PaidAmountUSD)  as AmountPaidUSD
	,sum(tr_BenWagesPayment.PaidAmount)  as AmountPaidSSP 
from 
	tr_BenWagesPayment 
	inner join tbl_WorkMaster 
		on tbl_WorkMaster.intWorkid=tr_BenWagesPayment.intWorkid
	inner join tm_Bomas as b 
		on b.intBomasid=tbl_WorkMaster.intBomasid
	inner join tm_Payams as p 
		on p.intPayamid=b.intPayamid
	inner join tm_Counties as c 
		on c.intCountieid=p.intCountieid
	inner join tm_States as s 
		on s.intStateid=c.intStateid
	inner join td_IPDetails as ip 
		on ip.intIP=tbl_WorkMaster.IPid
where 
	ip.intIP like @IPid 
	and s.intStateid like @intStateid 
	and c.intCountieid like @intCountieid 
	and p.intPayamid like @intPayamid 
	and b.intBomasid like @intBomasid 
	and tr_BenWagesPayment.uuid is not null
end