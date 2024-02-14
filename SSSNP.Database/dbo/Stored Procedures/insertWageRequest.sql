CREATE procedure [dbo].[insertWageRequest]
(

@intWorkid	int,
@WageRateUSD	decimal(18, 2),
@ReqFromDate	datetime,
@ReqToDate	datetime,
@ReqYear	varchar(5),
@ReqMonth	varchar(10),
@intPayingAgentid	int,
@intCoOrdinatorid	int,
@intSpecialistid	int,
@intManagerid	int,
@intPaymentCycleID int,
@Exchangerateattime	decimal(18, 2),
@Created_by	int	
,@Component varchar(200)
)

As
Begin
insert into td_WorkWageRequest
(
 intWorkid, WageRateUSD, ReqFromDate, ReqToDate, ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid,PaymentCycle ,Exchangerateattime,  Status, Remarks, 
                         Created_by, Created_dt,component 
)
values
(
@intWorkid,@WageRateUSD, @ReqFromDate, @ReqToDate, @ReqYear, @ReqMonth, @intPayingAgentid, @intCoOrdinatorid, @intSpecialistid, @intManagerid, @intPaymentCycleID,@Exchangerateattime,  '1', 'Request Sent', 
                         @Created_by, getdate(),@component

)

Declare @ids as int
set @ids=@@IDENTITY

Update td_WorkWageRequest set ReqNo= convert(varchar,YEAR(getdate()))+'-SNSOPWR-'+convert(varchar,@ids) where intWagePaymentReqid like @ids

insert into td_WageReqTrans
(
intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
)
values
(
@ids,@intWorkid,@Created_by,@intCoOrdinatorid,'Pending',getdate(), 'Request Sent',@Created_by,getdate()
)
select @ids
return @ids
End

