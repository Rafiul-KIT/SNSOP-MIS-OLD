
CREATE procedure [dbo].[InsUpdBeneficiaryWagesRequestPayingAgentImport]
(


@intWagePaymentReqid	varchar(8),
@intWorkid	varchar(8),
@intBenid	varchar(8),
@Exchangerate	decimal(18, 2),
@Created_by	varchar(8),
@intWagePaymentReqPAid varchar(8)
)
AS
-- By Srikanth on 01-02-2018 -Insert Paying agent Selection
Begin

Declare @intWagesReqBenTransid as int

set @intWagesReqBenTransid=(select top 1 intWagesReqBenTransid from td_WageReqBenDet where intBenid=@intBenid and intWorkid=@intWorkid and intWagePaymentReqid=@intWagePaymentReqid)



Declare @TotAttendance as int
set @TotAttendance=(select top 1 TotAttendance from td_WageReqBenDet where intBenid=@intBenid and intWorkid=@intWorkid and intWagePaymentReqid=@intWagePaymentReqid)

Declare @WageRateUSD as decimal(18,2)
set @WageRateUSD=(select top 1 WageRateUSD from td_WageReqBenDet where intBenid=@intBenid and intWorkid=@intWorkid and intWagePaymentReqid=@intWagePaymentReqid)



Declare @WageRateUSDTotal  as decimal(18,2)
set @WageRateUSDTotal=(select top 1 WageRateUSDTotal from td_WageReqBenDet where intBenid=@intBenid and intWorkid=@intWorkid and intWagePaymentReqid=@intWagePaymentReqid)




insert into td_WageReqBenPADet
(
intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, WageRateSSP, WageRateSSPTotal, Status, Created_by, 
                         Created_dt,intWagePaymentReqPAid
)
values
(
@intWagesReqBenTransid, @intWagePaymentReqid, @intWorkid, @intBenid, @TotAttendance, @WageRateUSD, @WageRateUSDTotal,@WageRateUSD*@Exchangerate , (@WageRateUSD*@Exchangerate)*@TotAttendance, 'Y', @Created_by, 
                         getdate(),@intWagePaymentReqPAid
)

select 1
return 1
End

