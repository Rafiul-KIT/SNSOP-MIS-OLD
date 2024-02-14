CREATE procedure [dbo].[InsUpdBeneficiaryWagesRequestPayingAgent]
(

@intWagesReqBenTransid	varchar(8),
@intWagePaymentReqid	varchar(8),
@intWorkid	varchar(8),
@intBenid	varchar(8),
@TotAttendance	varchar(8),
@WageRateUSD	decimal(18, 2),
@WageRateUSDTotal	decimal(18, 2),
@WageRateSSP	decimal(18, 2),
@WageRateSSPTotal	decimal(18, 2),
@Created_by	varchar(8),
@intWagePaymentReqPAid varchar(8)
)
AS
-- By Srikanth on 01-02-2018 -Insert Paying agent Selection
Begin
insert into td_WageReqBenPADet
(
intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance, WageRateUSD, WageRateUSDTotal, WageRateSSP, WageRateSSPTotal, Status, Created_by, 
                         Created_dt,intWagePaymentReqPAid
)
values
(
@intWagesReqBenTransid, @intWagePaymentReqid, @intWorkid, @intBenid, @TotAttendance, @WageRateUSD, @WageRateUSDTotal, @WageRateSSP, @WageRateSSPTotal, 'Y', @Created_by, 
                         getdate(),@intWagePaymentReqPAid
)

select 1
return 1
End

