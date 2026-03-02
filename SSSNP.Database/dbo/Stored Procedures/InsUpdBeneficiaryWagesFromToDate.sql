CREATE Procedure [dbo].[InsUpdBeneficiaryWagesFromToDate]    
(   
    
@intBenid int,    
@FromDate datetime,    
@ToDate datetime,    
@PaidDate datetime,    
@PaidAmount decimal(18, 2),    
@Created_by int,
@Wagerate varchar(10) ,
 @intWorkid varchar(8)  
    
     
)    
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Insert Wage Payment

-- Working Purpose : Insert Wage payment with USD

-- Developer : Srikanth

-- Date: 04-08-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
----------------------------------------------------------------------------- 
as begin    
    
       
if((select COUNT(*) from tr_BenWagesPayment where intBenid=@intBenid and FromDate=@FromDate and ToDate=@ToDate)>0)        
begin    
  
update  tr_BenWagesPayment set Year=YEAR(@FromDate), Month=MONTH(@ToDate), PaidDate=@PaidDate,  PaidAmount=(@PaidAmount/@Wagerate)*70,Wagerate='70' ,  PaidAmountUSD=@PaidAmount,WageRateUSD=@Wagerate,Modified_by=@Created_by,Modified_dt=GETDATE() ,intWorkid=@intWorkid  where intBenid like @intBenid  and FromDate=@FromDate and ToDate=@ToDate
select 1000        
return 1000        
end        
else        
begin        
insert into tr_BenWagesPayment(intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,Wagerate,FromDate,ToDate,intWorkid,PaidAmountUSD,WageRateUSD)        
values(@intBenid, YEAR(@FromDate), MONTH(@ToDate), @PaidDate, (@PaidAmount/@Wagerate)*70, @Created_by,  GETDATE(),'70',@FromDate,@ToDate,@intWorkid,@PaidAmount,@Wagerate)        
select 1000        
return 1000        
end        
        
    
    
end




