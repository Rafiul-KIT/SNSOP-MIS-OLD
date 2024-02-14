CREATE procedure [dbo].[getPaymentCycle]

AS
Begin
select intPaymentCycleID, WagePaymentCycle from tm_WagePaymentCycle order by intPaymentCycleID asc
End

