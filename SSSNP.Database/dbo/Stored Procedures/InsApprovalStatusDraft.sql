
CREATE procedure [dbo].[InsApprovalStatusDraft]
(
@intWagePaymentReqid varchar(8),
@ReqStatus varchar(8),
@Created_by varchar(8),
@intPayingaAgent varchar(10),
@intCoordinatorId varchar(10),
@intSpecialistId  varchar(10),
@intManagerId  varchar(10),
@Comment nvarchar(max)
)
As
Begin
Update td_WorkWageRequest set Status=@ReqStatus,intPayingAgentid=@intPayingaAgent,intCoOrdinatorid=@intCoordinatorId, intSpecialistid=@intSpecialistId,intManagerid=@intManagerId, Remarks=@Comment
,Modified_by=@Created_by,Modified_dt=getdate() where intWagePaymentReqid=@intWagePaymentReqid

Declare @intWorkid as int
Declare @intFromid as int
Declare @intToid as int
set @intWorkid=(select a.intWorkid from td_WorkWageRequest as a where a.intWagePaymentReqid like @intWagePaymentReqid)

set @intFromid= (select  c.intFromid from   td_WageReqTrans as c where c.intWagesReqTransid in (select top 1 t.intWagesReqTransid from td_WageReqTrans as t where t.intWagePaymentReqid like @intWagePaymentReqid order by t.intWagesReqTransid desc))

set @intToid= (select  c.intToid from   td_WageReqTrans as c where c.intWagesReqTransid in (select top 1 t.intWagesReqTransid from td_WageReqTrans as t where t.intWagePaymentReqid like @intWagePaymentReqid order by t.intWagesReqTransid desc))


insert into td_WageReqTrans
(
 intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Created_by, Created_dt
)
values
(
@intWagePaymentReqid,@intWorkid,@intFromid,@intToid,@ReqStatus,getdate(),@Created_by,getdate()
)

select 1
return 1
End