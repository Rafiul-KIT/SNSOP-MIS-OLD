CREATE Procedure [dbo].[GetBenPaidAmtFromToDate]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intBenid varchar(10)  
)  
----------------------------------------------------------------------------
-----------------------------------------------------------------------------
-- Project : SNSDP

-- Purpose: Entry Updation with 3 USD

-- Working Purpose : As per Updation by SSP, now through this USD - Search

-- Developer : Srikanth

-- Date: 04-08-2017

-- Modifeid Tables Used: tr_BenWagesPayment
-----------------------------------------------------------------------------
-----------------------------------------------------------------------------
as begin  
select isnull( SUM(PaidAmountUSD),0) as Amt from tr_BenWagesPayment where intBenid like @intBenid 
--and Year =@Year and Month=@Month  
  
end




