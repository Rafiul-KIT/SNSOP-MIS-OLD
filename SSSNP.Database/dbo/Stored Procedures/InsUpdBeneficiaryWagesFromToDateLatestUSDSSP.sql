
CREATE procedure [dbo].[InsUpdBeneficiaryWagesFromToDateLatestUSDSSP]    
(   
    
@intBenid int,    
@FromDate datetime,    
@ToDate datetime,    
@PaidDate datetime,    
@PaidAmount decimal(18, 2),    
@Created_by int,
@Wagerate varchar(10) ,
 @intWorkid varchar(8),
   @ExchangeRate decimal(18, 2),
   @PaidAmountUSD decimal(18, 2),
   @WagerateUSD decimal(18,2)
    
     
)    
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Insert Wage Payment

-- Working Purpose : Insert Wage payment with USD adding Exchange Rate and USD Entry Field

-- Developer : Srikanth

-- Date: 04-10-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
----------------------------------------------------------------------------- 
as begin    
    
       
if((select COUNT(*) from tr_BenWagesPayment where intWorkid=@intWorkid and intBenid=@intBenid and FromDate=@FromDate and ToDate=@ToDate)>0)        
begin  

Delete from  tr_BenWagesPayment where intWorkid=@intWorkid and intBenid=@intBenid and FromDate=@FromDate and ToDate=@ToDate
  insert into tr_BenWagesPayment(intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,Wagerate,FromDate,ToDate,intWorkid,PaidAmountUSD,WageRateUSD,ExchangeRate)        
values(@intBenid, YEAR(@FromDate), MONTH(@ToDate), getdate(), @PaidAmount, @Created_by,  GETDATE(),@Wagerate,@FromDate,@ToDate,@intWorkid,@PaidAmountUSD,@WagerateUSD,@ExchangeRate)        
update a set a.AttendanceDays=c.TotAttendance,a.uuid=c.intWagePaymentReqid,a.trans_id=c.intWagesReqBenTransid,a.PaidGenerateddate=a.PaidDate,a.upd='1',a.Paidto=d.household_number  from 
tr_BenWagesPayment as a inner join td_WorkWageRequest as b on b.intWorkid=a.intWorkid and convert(varchar(11), b.ReqFromDate,106)=convert(varchar(11),a.FromDate,106) and convert(varchar(11), b.ReqToDate,106)=convert(varchar(11),a.ToDate,106) inner join td_WageReqBenDet as c on c.intWagePaymentReqid=b.intWagePaymentReqid and c.intWorkid=b.intWorkid
and a.intbenid=c.intbenid and a.intworkid=c.intWorkid inner join tbl_BeneficiaryDet as d on d.intBenid=a.intBenid and d.intBenid=c.intBenid  where a.AttendanceDays is null and a.intBenid like @intBenid and a.intWorkid like @intWorkid

--update  tr_BenWagesPayment set Year=YEAR(@FromDate), Month=MONTH(@ToDate), PaidDate=@PaidDate,  PaidAmount=((@PaidAmount/@Wagerate)*(@ExchangeRate*@Wagerate)),Wagerate=(@ExchangeRate*@Wagerate) ,  PaidAmountUSD=@PaidAmount,WageRateUSD=@Wagerate,Modified_by=@Created_by,Modified_dt=GETDATE() ,intWorkid=@intWorkid,ExchangeRate=@ExchangeRate  where intBenid like @intBenid  and FromDate=@FromDate and ToDate=@ToDate


select 1000        
return 1000        
end        
else        
begin        
 insert into tr_BenWagesPayment(intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,Wagerate,FromDate,ToDate,intWorkid,PaidAmountUSD,WageRateUSD,ExchangeRate)        
values(@intBenid, YEAR(@FromDate), MONTH(@ToDate),getdate(), @PaidAmount, @Created_by,  GETDATE(),@Wagerate,@FromDate,@ToDate,@intWorkid,@PaidAmountUSD,@WagerateUSD,@ExchangeRate)  


update a set a.AttendanceDays=c.TotAttendance,a.uuid=c.intWagePaymentReqid,a.trans_id=c.intWagesReqBenTransid,a.PaidGenerateddate=a.PaidDate,a.upd='1',a.Paidto=d.household_number  from 
tr_BenWagesPayment as a inner join td_WorkWageRequest as b on b.intWorkid=a.intWorkid and convert(varchar(11), b.ReqFromDate,106)=convert(varchar(11),a.FromDate,106) and convert(varchar(11), b.ReqToDate,106)=convert(varchar(11),a.ToDate,106) inner join td_WageReqBenDet as c on c.intWagePaymentReqid=b.intWagePaymentReqid and c.intWorkid=b.intWorkid
and a.intbenid=c.intbenid and a.intworkid=c.intWorkid inner join tbl_BeneficiaryDet as d on d.intBenid=a.intBenid and d.intBenid=c.intBenid  where a.AttendanceDays is null and a.intBenid like @intBenid and a.intWorkid like @intWorkid      
select 1000        
return 1000        
end        
        
    
    
end




