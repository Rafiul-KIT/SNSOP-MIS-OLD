CREATE Procedure [dbo].[Insert_Update_Wage_RateByCountie]
(
	@WageRate decimal(18,2),
	@Created_by int,
	@intExachageid int,
	@intStateid varchar(8),
	@intCountieid varchar(8),
	@Return_ID int output
)
As
BEGIN
	declare @exist_rate int
	set @exist_rate = (select count(*) from td_ExchangeRateDet)
	 
	 if(@intExachageid!='')
	BEGIN
		Update td_ExchangeRateDet
		Set WageRate = @WageRate,
		intStateid=@intStateid,
		intCountyid=@intCountieid,
			Modified_by = @Created_by,
			Modified_dt = GETDATE()
		Where intExachageid = @intExachageid

		set @Return_ID = 0
	END
	ELSE
	BEGIN
			if(@exist_rate!='')
			BEGIN
				set @Return_ID = 1
			END
			ELSE
			BEGIN
				Insert into td_ExchangeRateDet
			(WageRate,Created_by,Created_dt,intStateid,intCountyid)
			Values
			(@WageRate,@Created_by,GETDATE(),@intStateid,@intCountieid)

			set @Return_ID = 0
			END
	END

END




