CREATE Procedure [dbo].[getEnviSearchReportDetailsNew]               
(    
	@WorkCode  varchar(200),
	@FromDate datetime,
	@ToDate datetime,
	@IPid varchar(8),
	@intBomaid varchar(8)
)                    
as begin                    
        if(@Fromdate<>'' and @ToDate<>'')
Begin            
 
 SELECT   EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.WorkCodeID,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, EnvironmentManagmentPlan.workCodeName, 
tm_States.StateName, EnvironmentManagmentPlan.workCodeName as NoOfReg,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath 



					              
FROM         EnvironmentManagmentPlan
inner join tbl_WorkMaster
on tbl_WorkMaster.intWorkid=Convert(int, EnvironmentManagmentPlan.WorkCodeID) inner join td_IPDetails on tbl_WorkMaster.IPid=td_IPDetails.intIP
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid
inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid

 where workCodeName like @WorkCode and tm_Bomas.intBomasid like @intBomaid and td_IPDetails.intIP like @IPid
 and EnvironmentManagmentPlan.Created_dt between @Fromdate and @ToDate+1

 and WorkCodeID is not null
  group by EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.workCodeName,EnvironmentManagmentPlan.WorkCodeID
					  ,tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName ,tm_Bomas.intBomasid,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath
 end                                   
 else
 begin                   
SELECT  EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.WorkCodeID,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName,  EnvironmentManagmentPlan.workCodeName, 
tm_States.StateName,EnvironmentManagmentPlan.workCodeName as NoOfReg,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath



					              
FROM         EnvironmentManagmentPlan inner join tbl_WorkMaster
on tbl_WorkMaster.intWorkid=Convert(int, EnvironmentManagmentPlan.WorkCodeID) inner join td_IPDetails on tbl_WorkMaster.IPid=td_IPDetails.intIP
inner join tm_Bomas on tm_Bomas.intBomasid=tbl_WorkMaster.intBomasid
inner join tm_Payams on tm_Payams.intPayamid=tm_Bomas.intPayamid
inner join tm_Counties on tm_Counties.intCountieid=tm_Payams.intCountieid
inner join tm_States on tm_States.intStateid=tm_Counties.intStateid
                                     
                      where workCodeName like @WorkCode and tm_Bomas.intBomasid like @intBomaid and td_IPDetails.intIP like @IPid
 
					     and WorkCodeID is not null
					  
					  group by EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.workCodeName,EnvironmentManagmentPlan.WorkCodeID
					  ,tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName,tm_Bomas.intBomasid,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath
                     
					   
 end                   
end




