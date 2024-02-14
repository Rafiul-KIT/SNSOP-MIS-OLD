CREATE procedure [dbo].[InsUpdBeneficiaryWagesRequestUpdateAttendancebyID]    
(   
@intWorkid  varchar(8),
 @intWagePaymentReqid varchar(8),
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
from tr_BenAttendanceDet  where tr_BenAttendanceDet.AttendanceDate between @FromDate and @ToDate  and tr_BenAttendanceDet.intWorkActivityId=@intWorkid
and tr_BenAttendanceDet.intBenid in (select aa.intBenid from td_WageReqBenDet as aa where aa.intWagePaymentReqid =@intWagePaymentReqid)



       
select 1000        
return 1000        
        
        
    
    
end


