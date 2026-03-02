CREATE Procedure [dbo].[getExchangeRatebyState]
(
@intStateid varchar(8)
)
As
--Created by Srikanth on 25-10-2017
--Tables Used: td_ExchangeRateDet
Begin
select top 1 ExchangeRate,WageRate from td_ExchangeRateDet 
--where intStateid like @intStateid order by intExachageid 
where intCountyid like @intStateid order by intExachageid desc
End
