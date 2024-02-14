CREATE procedure [dbo].[RetriveWageRequestPersons]
(
@intBenid varchar(8),
@FromDate datetime,
@ToDate datetime,
@intWagePaymentReqid varchar(8)
)
as
Begin
select * from td_WageReqBenDet where intWagePaymentReqid like @intWagePaymentReqid
End

