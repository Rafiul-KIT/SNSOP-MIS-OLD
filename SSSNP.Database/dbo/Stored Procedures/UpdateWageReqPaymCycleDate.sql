create procedure [dbo].[UpdateWageReqPaymCycleDate]
(
@intWagePaymentReqid varchar(8),
@PaymentCycleId varchar(8),
@dateFrom varchar(10),
@dateTo varchar(10)
)
AS
Begin

Update td_WorkWageRequest 
set 
	PaymentCycle=@PaymentCycleId
	,ReqFromDate=@dateFrom
	,ReqToDate=@dateTo 
where intWagePaymentReqid=@intWagePaymentReqid

select 1
return 1
End