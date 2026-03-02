
CREATE procedure [dbo].[RetriveWageRequestPersonsPA]
(
@intBenid varchar(8),
@FromDate datetime,
@ToDate datetime,
@intWagePaymentReqid varchar(8),
@intWagePaymentReqPAid varchar(8)
)
as
Begin
select * from td_WageReqBenPADet where intWagePaymentReqid like @intWagePaymentReqid and intWagePaymentReqPAid like @intWagePaymentReqPAid
End

