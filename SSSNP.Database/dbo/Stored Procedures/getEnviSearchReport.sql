CREATE Procedure [dbo].[getEnviSearchReport]               
(    
	@WorkCode  varchar(200)
)                    
as begin                    
                    
 if(@WorkCode is null)
 begin
 SELECT   EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.WorkCodeID,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, EnvironmentManagmentPlan.workCodeName, 
tm_States.StateName, EnvironmentManagmentPlan.workCodeName as NoOfReg,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath 



					              
FROM         EnvironmentManagmentPlan INNER JOIN                    
                      tm_States on tm_States.intStateid=EnvironmentManagmentPlan.StateID      
inner join tm_Counties  on tm_Counties.intCountieid=EnvironmentManagmentPlan.CountryID     
inner join tm_Payams on tm_Payams.intPayamid=EnvironmentManagmentPlan.PayamId      
inner join tm_Bomas on tm_Bomas.intBomasid=EnvironmentManagmentPlan.BomaId

  group by EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.workCodeName,EnvironmentManagmentPlan.WorkCodeID
					  ,tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName ,tm_Bomas.intBomasid,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath
 end                                   
 else
 begin                   
SELECT  EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.WorkCodeID,
tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName,  EnvironmentManagmentPlan.workCodeName, 
tm_States.StateName,EnvironmentManagmentPlan.workCodeName as NoOfReg,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath



					              
FROM         EnvironmentManagmentPlan INNER JOIN                    
                      tm_States on tm_States.intStateid=EnvironmentManagmentPlan.StateID      
inner join tm_Counties  on tm_Counties.intCountieid=EnvironmentManagmentPlan.CountryID     
inner join tm_Payams on tm_Payams.intPayamid=EnvironmentManagmentPlan.PayamId      
inner join tm_Bomas on tm_Bomas.intBomasid=EnvironmentManagmentPlan.BomaId
                                          
                      where workCodeName = @WorkCode
					    
					  
					  group by EnvironmentManagmentPlan.BomaId,EnvironmentManagmentPlan.workCodeName,EnvironmentManagmentPlan.WorkCodeID
					  ,tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName,tm_Bomas.intBomasid,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath
                     
					   
 end                   
end




