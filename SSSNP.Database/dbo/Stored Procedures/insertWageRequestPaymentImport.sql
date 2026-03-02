CREATE procedure [dbo].[insertWageRequestPaymentImport]
(
@WageReqNo varchar(120),
@DefinedExchangeRate decimal(18,2),
@Created_by varchar(8)
)
As
Begin

Declare @intWagePaymentReqid as varchar(8)
set @intWagePaymentReqid=(select  intWagePaymentReqid from td_WorkWageRequest where ReqNo like @WageReqNo)
insert into td_WorkWageRequestPA

( intWagePaymentReqid, intWorkid, WageRateUSD, ReqFromDate, ReqToDate, ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid, Exchangerateattime, 
                         ReqNo, Status, Remarks, DefinedExchangeRate, Created_by, Created_dt
						 )
						 select 
						 intWagePaymentReqid, intWorkid, WageRateUSD, ReqFromDate, ReqToDate, ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid, Exchangerateattime, 
                         ReqNo, '6', Remarks, @DefinedExchangeRate, @Created_by , getdate() from td_WorkWageRequest where intWagePaymentReqid like @intWagePaymentReqid 


						 Declare @ids as int
						 set @ids=@@IDENTITY

						 insert into td_WageReqPATrans
						 (
						 intWagePaymentReqPAid,  intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
						 )

						 select   intWagePaymentReqPAid,  intWagePaymentReqid, intWorkid,intPayingAgentid, intCoOrdinatorid,'1',getdate(),'Payment Request Sent',intPayingAgentid,getdate()  from td_WorkWageRequestPA where intWagePaymentReqPAid like @ids


						 Update td_WorkWageRequest set Status='5', Modified_by=@Created_by, Modified_dt=getdate() where intWagePaymentReqid like @intWagePaymentReqid
						  insert into td_WageReqTrans
						 (
						   intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
						 )
						  select     intWagePaymentReqid, intWorkid,intPayingAgentid, intCoOrdinatorid,'5',getdate(),'Payment Request Sent Final',intPayingAgentid,getdate()  from td_WorkWageRequestPA where intWagePaymentReqPAid like @ids

						 
						 select @ids
						 return @ids
End
