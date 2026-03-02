CREATE Procedure [dbo].[getdeletewagepaybyid]

@intBenid varchar(8),
@paymentdate datetime,
@DeletedBy varchar(8)
as begin


insert into tr_BenWagesPaymentTrans
(
 intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, 
                      PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays,DeletedBy,DeletedDt

)

select  intWageid, intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt, Modified_by, Modified_dt, Wagerate, WageRateUSD, 
                      PaidAmountUSD, FromDate, ToDate, intWorkid, statusforUSD, ExchangeRate, WageYear, SheetNo, PortYear, PortPhase, PortedSheetName, AttendanceDays,@DeletedBy,getdate()
from  tr_BenWagesPayment where intBenid like @intBenid and (Convert(varchar(11), PaidDate,106) like Convert(varchar(11), @paymentdate,106))

delete   from tr_BenWagesPayment where intBenid like @intBenid and (Convert(varchar(11), PaidDate,106) like Convert(varchar(11), @paymentdate,106))

select 1
return 1
end



