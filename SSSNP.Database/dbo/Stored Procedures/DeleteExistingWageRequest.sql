
CREATE procedure [dbo].[DeleteExistingWageRequest]
(
@intWagePaymentReqid varchar(8),
@Created_by varchar(8)
)
As
Begin

insert into td_WageReqBenTransDet

( intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt
						 )
						 select 
						intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt from td_WageReqBenDet where intWagePaymentReqid like @intWagePaymentReqid 

						


Delete from td_WageReqBenDet where  intWagePaymentReqid like @intWagePaymentReqid


Update td_WorkWageRequest set Status=1 where intWagePaymentReqid like @intWagePaymentReqid



						 select @intWagePaymentReqid
						 return @intWagePaymentReqid
End

