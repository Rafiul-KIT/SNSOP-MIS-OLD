Create Procedure [dbo].[Get_Wage_RateStateCountieById]
(@intExachageid int) 
 AS                                
BEGIN
	 
	 Select td_ExchangeRateDet.*, tm_States.StateName,tm_Counties.CountieName 
	 from td_ExchangeRateDet inner join tm_States on tm_States.intStateid=td_ExchangeRateDet.intStateid 
	 inner join tm_Counties on tm_Counties.intCountieid=td_ExchangeRateDet.intCountyid
		 where td_ExchangeRateDet.intExachageid = @intExachageid 

END
