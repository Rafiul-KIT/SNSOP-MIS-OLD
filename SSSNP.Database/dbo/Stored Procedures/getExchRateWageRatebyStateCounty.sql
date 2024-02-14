create Procedure [dbo].[getExchRateWageRatebyStateCounty]
(
@intStateid varchar(8),
@intCountieId varchar(8)
)
As

Begin

select top 1 ExchangeRate,WageRate from td_ExchangeRateDet 
where intStateid like @intStateid 
and intCountyid like @intCountieId
order by intExachageid 
End
