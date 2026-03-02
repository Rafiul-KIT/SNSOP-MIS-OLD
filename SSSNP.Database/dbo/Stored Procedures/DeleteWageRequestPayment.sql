
CREATE procedure [dbo].[DeleteWageRequestPayment]
(
@intWagePaymentReqid varchar(8),
@DefinedExchangeRate decimal(18,2),
@Created_by varchar(8),
@intWagePaymentReqPAid varchar(8)
)
As
Begin

insert into td_WageReqBenPADetTrans
(

 intWagePaymentReqPAid, intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, 
                      WageRateUSD, WageRateUSDTotal, WageRateSSP, WageRateSSPTotal, Status, Created_by, Created_dt, Modified_by, Modified_dt

)
select  intWagePaymentReqPAid, intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, 
                      WageRateUSD, WageRateUSDTotal, WageRateSSP, WageRateSSPTotal, Status, Created_by, Created_dt, Modified_by, Modified_dt
 from td_WageReqBenPADet where intWagePaymentReqPAid like @intWagePaymentReqPAid

 Declare @Workid as int
 set @Workid =(select top 1 intWorkid from td_WorkWageRequestPA where intWagePaymentReqPAid like @intWagePaymentReqPAid)

 Declare @formdate datetime

 set @formdate=(select top 1 ReqFromDate from td_WorkWageRequestPA where intWagePaymentReqPAid like @intWagePaymentReqPAid)

  Declare @todate datetime

 set @todate=(select top 1 ReqToDate from td_WorkWageRequestPA where intWagePaymentReqPAid like @intWagePaymentReqPAid)

 Delete from tr_BenWagesPayment where intWorkid like @Workid and Convert(varchar(11),Fromdate,106) like  Convert(varchar(11),@formdate,106) and Convert(varchar(11),ToDate,106) like  Convert(varchar(11),@todate,106) and intBenid in
 (
 select intBenid from td_WageReqBenPADet where intWagePaymentReqPAid like @intWagePaymentReqPAid
 )
--Delete from td_WageReqBenPADet where intWagePaymentReqPAid like @intWagePaymentReqPAid
update td_WorkWageRequestPA set Status='6', DefinedExchangeRate=@DefinedExchangeRate where intWagePaymentReqPAid like @intWagePaymentReqPAid
Delete from td_WageReqBenPADet where intWagePaymentReqPAid like @intWagePaymentReqPAid
						 select @intWagePaymentReqPAid
						 return @intWagePaymentReqPAid
End

