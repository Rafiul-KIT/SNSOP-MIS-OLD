CREATE procedure [dbo].[Insert_Update_EnviMan]
(
@intIP varchar(50),
@IPName varchar(200),
@projectID varchar(10),
@projectName varchar(200),
@StateID varchar(10),
@StateName varchar(200),
@CountryID varchar(10),
@CountryName varchar(200),
@PayamId varchar(10),
@PayamName varchar(200),
@BomaId varchar(10),
@BomaName varchar(200),
@WorkCodeID varchar(10),
@workCodeName varchar(200),
@EnvironmentSocialImpact varchar(500),
@ProposedMitigationMeasure varchar(500),
@MitigationMeasureImplemented varchar(500),
@PerformanceIndicators varchar(500),
@FilePath varchar(200),
@FileName varchar(200)
)
As
BEGIN
	if((select COUNT(*) from EnvironmentManagmentPlan where WorkCodeID = @WorkCodeID)>0)
	 begin
		update EnvironmentManagmentPlan
		set projectID = @projectID,
		projectName = @projectName,
		StateID = @StateID,
		StateName = @StateName,
		CountryID = @CountryID,
		CountryName	=	@CountryName,
		PayamId =	@PayamId,
		PayamName =	@PayamName,
		BomaId = @BomaId,
		BomaName  = @BomaName,
		WorkCodeID =	@WorkCodeID,
		workCodeName=	@workCodeName,
		EnvironmentSocialImpact =	@EnvironmentSocialImpact,
		ProposedMitigationMeasure =	@ProposedMitigationMeasure,
		MitigationMeasureImplemented =	@MitigationMeasureImplemented,
		PerformanceIndicators =	@PerformanceIndicators,
		FilePath = @FilePath,
		FileName = @FileName
		where WorkCodeID =	@WorkCodeID

		select  @WorkCodeID
		return @WorkCodeID    
	 end
	 else
	 begin
		insert into EnvironmentManagmentPlan(projectID,projectName,StateID,StateName,CountryID,CountryName,
		PayamId,PayamName,BomaId,BomaName,WorkCodeID,workCodeName,EnvironmentSocialImpact,ProposedMitigationMeasure,
		MitigationMeasureImplemented,PerformanceIndicators,FilePath,FileName)
		values(@projectID,@projectName,@StateID,@StateName,@CountryID,@CountryName,@PayamId,@PayamName,@BomaId,@BomaName,
		@WorkCodeID,@workCodeName,@EnvironmentSocialImpact,@ProposedMitigationMeasure,
		@MitigationMeasureImplemented,@PerformanceIndicators,@FilePath,@FileName)

		select  @@IDENTITY
		return @@IDENTITY    
	 end

END



