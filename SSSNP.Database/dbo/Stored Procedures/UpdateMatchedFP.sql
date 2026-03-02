
CREATE procedure [dbo].[UpdateMatchedFP]    
(   
    
@intBenid int,    
--@FromDate datetime,    
--@ToDate datetime,    
@PaidDate datetime,    
@PaidAmount decimal(18, 2),    
@Created_by int,
@Wagerate varchar(10) ,
 @intWorkid varchar(8),
   @ExchangeRate decimal(18, 2),
   @PaidAmountUSD decimal(18, 2),
   @WagerateUSD decimal(18,2),
   @uuid varchar(8),
   @attendance varchar(8)
    ,
   @PaidGenerateddate datetime,
   @Paidto varchar(500),
   @trans_id varchar(8),
   @matched_fp varchar(MAX),
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
    
	Declare @FromDate as datetime
	Declare @ToDate as datetime
	--Declare @PaidDate as datetime
	set @FromDate=(select ReqFromDate from td_WorkWageRequest where intWagePaymentReqid=@uuid)
	set @ToDate=(select ReqToDate from td_WorkWageRequest where intWagePaymentReqid=@uuid)
       --set @PaidDate=getdate()
       
update tr_BenWagesPayment set matched_fp=@matched_fp,latitude=@latitude,longitude=@longitude where intBenid=@intBenid and intWorkid=@intWorkid and Convert(varchar(11),FromDate,106)= Convert(varchar(11),@FromDate,106)   and Convert(varchar(11),ToDate,106)= Convert(varchar(11),@ToDate,106)  and uuid=@uuid  
        
    
    select 1
	return 1
end




