CREATE procedure [dbo].[UpdateCashPlusActivitiesDIS]
(
@intWagePaymentReqid varchar(8),
@intBenId varchar(8),
@FLI varchar(1),
@ECD varchar(1),
@WASH varchar(1)
)
AS
Begin


Update td_WageReqBenDet set cashP_FLI=@FLI,cashP_ECD=@ECD, cashP_WASH=@WASH
where intWagePaymentReqid=@intWagePaymentReqid and intBenid=@intBenId

End