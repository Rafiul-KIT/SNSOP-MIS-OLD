CREATE procedure [dbo].[UpdateWageRequestCApprovedate]
(
@intWagePaymentReqid varchar(8),
@Created_by varchar(8)
)
AS
Begin

Update 
	td_WorkWageRequest 
set 
	CApproveDate=getdate()
	,Modified_by=@Created_by
	,Modified_dt=getdate() 
where 
	intWagePaymentReqid=@intWagePaymentReqid

select 1
return 1
End