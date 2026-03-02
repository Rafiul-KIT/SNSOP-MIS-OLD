

CREATE procedure [dbo].[DeleteWageRequestDraft]
(
@intWagePaymentReqid varchar(8),
@Created_by varchar(8)
)
As
Begin

declare @WRStatus as int 
declare @Creator as int 
set @WRStatus = (select status from td_WorkWageRequest where intWagePaymentReqid = @intWagePaymentReqid)
set @Creator =  (select created_by from td_WorkWageRequest where intWagePaymentReqid = @intWagePaymentReqid)

if (@WRStatus=105 AND @Creator=@Created_by)
	begin 
		-------
		delete from td_WageReqTrans
		where intWagePaymentReqid = @intWagePaymentReqid


		delete from td_WageReqBenDet
		where intWagePaymentReqid = @intWagePaymentReqid

		update tr_BenAttendanceDet set WageApplyFlag=NULL, intWageReqid=NULL where intWageReqid = @intWagePaymentReqid

		delete from td_WorkWageRequest
		where intWagePaymentReqid  = @intWagePaymentReqid

		select 1		------------------------
	end
else
	begin
		select 0
	end
End