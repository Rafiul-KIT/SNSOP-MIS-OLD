

CREATE procedure [dbo].[InsUpdBeneficiaryWagesFromToDateLatestUSDSSPNewChecking]    
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
   @matched_fp varchar(MAX)
   
    
     
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
Declare @household_number as varchar(180)
Declare @household_number1 as varchar(180)
Declare @household_number2 as varchar(180)   
set @household_number= (select household_number from tbl_BeneficiaryDet where intBenid=@intBenid)
set @household_number1= (select alternate_number from tbl_BeneficiaryDet where intBenid=@intBenid)
set @household_number2= (select Secondalternate_number from tbl_BeneficiaryDet where intBenid=@intBenid)
--set @household_number2= (select Secondalternate_number from tbl_BeneficiaryDet where intBenid=@intBenid)
    
	if(@Paidto = @household_number)
	Begin
	select 1
	return 1
	End
	else if(@Paidto = @household_number1)
	Begin
	select 1
	return 1
	End
	else if(@Paidto = @household_number2)
	Begin
	select 1
	return 1
	End
	else
	Begin
       --set @PaidDate=getdate()
if((select COUNT(*) from tr_BenWagesPaymentNotPaidAlternatemore where intWorkid=@intWorkid and intBenid=@intBenid and FromDate=@FromDate and ToDate=@ToDate)>0)        
begin 
select 0
	return 0
End
Else
Begin
	insert into tr_BenWagesPaymentNotPaidAlternatemore(intBenid, Year, Month, PaidDate, PaidAmount, Created_by, Created_dt,Wagerate,FromDate,ToDate,intWorkid,PaidAmountUSD,WageRateUSD,ExchangeRate,AttendanceDays,PaidGenerateddate,Paidto,trans_id,uuid)        
values(@intBenid, YEAR(@FromDate), MONTH(@ToDate), @PaidDate, @PaidAmount, @Created_by,  GETDATE(),@Wagerate,@FromDate,@ToDate,@intWorkid,@PaidAmountUSD,@WagerateUSD,@ExchangeRate,@attendance,@PaidGenerateddate,@Paidto,@trans_id,@uuid)        
	
	insert into Beneficiary_API_Failure_Payments
	(
	Household_Number, work_id, trans_id, uuid, Failure_Message, PullDate, Create_Date, Transfer_Date


	)
	values
	(
	@household_number, @intWorkid, @trans_id, @uuid, 'Trying to pull more than 2 alternate payment or not registered in mis', getdate(), getdate(), getdate()
	)
	
	
	
	select 0
	return 0
	End
	End
        
    
    
end





