
CREATE procedure [dbo].[InsUpdBeneficiaryWagesFromToDateLatestUSDSSPNewwithSupervisoidlatlong]    
(   
    
@intBenid int,    
--@FromDate datetime,    
--@ToDate datetime,    
@PaidDate datetime,    
@PaidAmount decimal(18, 4),    
@Created_by int,
@Wagerate varchar(10) ,
 @intWorkid varchar(8),
   @ExchangeRate decimal(18, 4),
   @PaidAmountUSD decimal(18, 2),
   @WagerateUSD decimal(18,2),
   @uuid varchar(8),
   @attendance varchar(8)
    ,
   @PaidGenerateddate datetime,
   @Paidto varchar(500),
   @trans_id varchar(8),
   @matched_fp varchar(MAX),
   @supervisor_id varchar(8),
   @latitude varchar(30),
   @longitude varchar(30)
   
    
     
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
    Begin tran
	Declare @FromDate as datetime
	Declare @ToDate as datetime
	--Declare @PaidDate as datetime
	set @FromDate=(select ReqFromDate from td_WorkWageRequest where intWagePaymentReqid=@uuid)
	set @ToDate=(select ReqToDate from td_WorkWageRequest where intWagePaymentReqid=@uuid)
       --set @PaidDate=getdate()
if((select COUNT(*) from tr_BenWagesPayment where intWorkid=@intWorkid and intBenid=@intBenid and FromDate=@FromDate and ToDate=@ToDate)>0)        
begin  

select 1000        
commit
return 1000        

end        
else        
begin 

 insert into tr_BenWagesPayment(intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,Wagerate,FromDate,ToDate,intWorkid,PaidAmountUSD,WageRateUSD,ExchangeRate,AttendanceDays,PaidGenerateddate,Paidto,trans_id,uuid,matched_fp,supervisor_id,latitude,longitude)        
values(@intBenid, YEAR(@FromDate), MONTH(@ToDate), @PaidDate, @PaidAmount, @Created_by,  GETDATE(),@Wagerate,@FromDate,@ToDate,@intWorkid,@PaidAmountUSD,@WagerateUSD,@ExchangeRate,@attendance,@PaidGenerateddate,@Paidto,@trans_id,@uuid,@matched_fp,@supervisor_id,@latitude,@longitude)        
commit
select 1000        
return 1000        
end        
        
    
    
end




