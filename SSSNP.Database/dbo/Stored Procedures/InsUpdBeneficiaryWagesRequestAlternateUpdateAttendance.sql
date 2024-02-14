CREATE procedure [dbo].[InsUpdBeneficiaryWagesRequestAlternateUpdateAttendance]    
(   
    
@intBenid int,       
@PaidAmount decimal(18, 2),    
@Created_by int,
@Wagerate decimal(18, 2),  
 @intWorkid varchar(8),
 @intWagePaymentReqid varchar(8),
 @TotAttendance varchar(8),
 @trineealternate varchar(100),
 @FromDate datetime,
 @ToDate datetime
    
     
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
    
       
      
        


Update tr_BenAttendanceDet set tr_BenAttendanceDet.WageApplyFlag='1' ,tr_BenAttendanceDet.intWageReqid=@intWagePaymentReqid
from tr_BenAttendanceDet  

where tr_BenAttendanceDet.AttendanceDate between @FromDate and @ToDate and tr_BenAttendanceDet.intBenid=@intBenid and tr_BenAttendanceDet.intWorkActivityId=@intWorkid



       
select 1000        
return 1000        
        
        
    
    
end


