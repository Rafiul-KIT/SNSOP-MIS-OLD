CREATE procedure [dbo].[InsUpdBeneficiaryWagesRequestAlternate]    
(   
    
@intBenid int,       
@PaidAmount decimal(18, 2),    
@Created_by int,
@Wagerate decimal(18, 2),  
 @intWorkid varchar(8),
 @intWagePaymentReqid varchar(8),
 @TotAttendance varchar(8),
 @trineealternate varchar(100)
    
     
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
    
       
      
        
insert into td_WageReqBenDet(intBenid,  WageRateUSDTotal, Created_by, Created_dt,WageRateUSD,intWorkid,intWagePaymentReqid,TotAttendance,Status,traineealternate)        
values(@intBenid,@PaidAmount, @Created_by,  GETDATE(),@Wagerate,@intWorkid,@intWagePaymentReqid,@TotAttendance,'Request',@trineealternate)        
select 1000        
return 1000        
        
        
    
    
end


