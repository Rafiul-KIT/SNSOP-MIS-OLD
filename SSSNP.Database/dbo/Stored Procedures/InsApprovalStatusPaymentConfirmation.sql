CREATE procedure [dbo].[InsApprovalStatusPaymentConfirmation]
(
@intWagePaymentReqPAid varchar(8),
@ReqStatus varchar(8),
@Created_by varchar(8),
@Remarks varchar(250)
)
As
Begin
Update td_WorkWageRequestPA set Status=@ReqStatus,Remarks=@Remarks,Modified_by=@Created_by,Modified_dt=getdate() where intWagePaymentReqPAid=@intWagePaymentReqPAid

Declare @intWorkid as int
Declare @intFromid as int
Declare @intToid as int
set @intWorkid=(select a.intWorkid from td_WorkWageRequestPA as a where a.intWagePaymentReqPAid like @intWagePaymentReqPAid)

set @intFromid= (select  c.intFromid from   td_WageReqPATrans as c where c.intWagesReqTransPAid in (select top 1 t.intWagesReqTransPAid from td_WageReqPATrans as t where t.intWagePaymentReqPAid like @intWagePaymentReqPAid order by t.intWagesReqTransPAid desc))

set @intToid= (select  c.intToid from   td_WageReqPATrans as c where c.intWagesReqTransPAid in (select top 1 t.intWagesReqTransPAid from td_WageReqPATrans as t where t.intWagePaymentReqPAid like @intWagePaymentReqPAid order by t.intWagesReqTransPAid desc))


insert into td_WageReqPATrans
(
 intWagePaymentReqPAid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
)
values
(
@intWagePaymentReqPAid,@intWorkid,@intFromid,@intToid,@ReqStatus,getdate(),@Remarks,@Created_by,getdate()
)

select 1
return 1
End

