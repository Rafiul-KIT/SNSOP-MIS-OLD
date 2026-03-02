CREATE procedure [dbo].[insertWageRequestPaymentintoWages]
(
@intWagePaymentReqPAid varchar(8),
@DefinedExchangeRate decimal(18,2),
@Created_by varchar(8),
@UploadSignedDoc varchar(450)
)
As
Begin


Update td_WorkWageRequest set Status=6 where intWagePaymentReqid in (select intWagePaymentReqid from td_WorkWageRequestPA where intWagePaymentReqPAid like @intWagePaymentReqPAid)

Update td_WorkWageRequestPA set Status=6 , Modified_by=@Created_by,Modified_dt=getdate(),UploadSignedDoc=@UploadSignedDoc where intWagePaymentReqPAid=@intWagePaymentReqPAid


						 insert into td_WageReqPATrans
						 (
						 intWagePaymentReqPAid,  intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
						 )

						 select   intWagePaymentReqPAid,  intWagePaymentReqid, intWorkid,intPayingAgentid, intCoOrdinatorid,'1',getdate(),'Payment Request Sent and paid',intPayingAgentid,getdate()  from td_WorkWageRequestPA where intWagePaymentReqPAid like @intWagePaymentReqPAid


						
						 
						 select @intWagePaymentReqPAid
						 return @intWagePaymentReqPAid
End

