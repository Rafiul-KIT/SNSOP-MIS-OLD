CREATE Procedure [dbo].[getExchangeRate]
As
--Created by Srikanth on 25-10-2017
--Tables Used: td_ExchangeRateDet
Begin
select top 1 ExchangeRate,WageRate from td_ExchangeRateDet order by intExachageid desc
End
