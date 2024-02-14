CREATE procedure [dbo].[Updatewagedate]
(
@WageRate decimal(18,4),
@intWagePaymentReqid varchar(8),
@Created_by varchar(8),
@amountInSSP decimal(18,2)
)
AS
Begin
Declare @OLDWageRate as decimal(18,4)
set @OLDWageRate=(select top 1 Exchangerateattime from td_WorkWageRequest where intWagePaymentReqid=@intWagePaymentReqid)

Update td_WorkWageRequest set AmountInSSP = @amountInSSP,  Exchangerateattime=@WageRate,ExchangeRateOLD=@OLDWageRate,SApproveDate=getdate(),Modified_by=@Created_by,Modified_dt=getdate() where intWagePaymentReqid=@intWagePaymentReqid


Update td_WageReqBenDet set  ExchangeRateOLD=@OLDWageRate,ExchangeRateNew=@WageRate where intWagePaymentReqid=@intWagePaymentReqid

select 1
return 1
End