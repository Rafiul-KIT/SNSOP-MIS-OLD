CREATE procedure [dbo].[insertWageRequestPaymentService]
(
@intWagePaymentReqid varchar(8),
@DefinedExchangeRate decimal(18,2),
@Created_by varchar(8)
)
As
Begin
 Declare @ids as int
if((select count(*) from td_WorkWageRequestPA where intWagePaymentReqid like @intWagePaymentReqid)=0)
Begin
insert into td_WorkWageRequestPA

( intWagePaymentReqid, intWorkid, WageRateUSD, ReqFromDate, ReqToDate, ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid, Exchangerateattime, 
                         ReqNo, Status, Remarks, DefinedExchangeRate, Created_by, Created_dt
						 )
						 select 
						 intWagePaymentReqid, intWorkid, WageRateUSD, ReqFromDate, ReqToDate, ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid, Exchangerateattime, 
                         ReqNo, '6', Remarks, @DefinedExchangeRate, @Created_by , getdate() from td_WorkWageRequest where intWagePaymentReqid like @intWagePaymentReqid 


						
						 set @ids=@@IDENTITY

						 insert into td_WageReqPATrans
						 (
						 intWagePaymentReqPAid,  intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
						 )

						 select   intWagePaymentReqPAid,  intWagePaymentReqid, intWorkid,intPayingAgentid, intCoOrdinatorid,'1',getdate(),'Payment Request Sent',intPayingAgentid,getdate()  from td_WorkWageRequestPA where intWagePaymentReqPAid like @ids


						 Update td_WorkWageRequest set Status='6', Modified_by=@Created_by, Modified_dt=getdate() where intWagePaymentReqid like @intWagePaymentReqid
						  insert into td_WageReqTrans
						 (
						   intWagePaymentReqid, intWorkid, intFromid, intToid, Status, TransDate, Remarks, Created_by, Created_dt
						 )
						  select     intWagePaymentReqid, intWorkid,intPayingAgentid, intCoOrdinatorid,'5',getdate(),'Payment Request Sent Final',intPayingAgentid,getdate()  from td_WorkWageRequestPA where intWagePaymentReqPAid like @ids

						 
						 select @ids
						 return @ids
						 End
						 else

						Begin
						
						 set @ids=(select MAX(intWagePaymentReqPAid)   from td_WorkWageRequestPA where intWagePaymentReqid like @intWagePaymentReqid)

						  return @ids
						End
End

