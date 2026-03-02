CREATE Procedure [dbo].[getEnviSearchReportDetails]        -- exec [getEnviSearchReportDetails] '2826','1007'       
(                    
@intBomasid  varchar(10),
@workCode varchar(200)
	
)                    
as begin                    
                    
  if(@workCode is null)
 begin
SELECT  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath



					              
FROM        EnvironmentManagmentPlan  INNER JOIN                    
                      tm_States on tm_States.intStateid=EnvironmentManagmentPlan.StateID      
inner join tm_Counties  on tm_Counties.intCountieid=EnvironmentManagmentPlan.CountryID      
inner join tm_Payams on tm_Payams.intPayamid=EnvironmentManagmentPlan.PayamId      
inner join tm_Bomas on tm_Bomas.intBomasid=EnvironmentManagmentPlan.BomaId 


where EnvironmentManagmentPlan.BomaId like @intBomasid
 end                                   
 else
 begin                    
                    
                    
SELECT  tm_Payams.PayamName, tm_Bomas.BomaName, tm_Counties.CountieName, 
tm_States.StateName,EnvironmentSocialImpact,ProposedMitigationMeasure,MitigationMeasureImplemented,PerformanceIndicators,FileName,FilePath



					              
FROM        EnvironmentManagmentPlan  INNER JOIN                    
                      tm_States on tm_States.intStateid=EnvironmentManagmentPlan.StateID      
inner join tm_Counties  on tm_Counties.intCountieid=EnvironmentManagmentPlan.CountryID      
inner join tm_Payams on tm_Payams.intPayamid=EnvironmentManagmentPlan.PayamId      
inner join tm_Bomas on tm_Bomas.intBomasid=EnvironmentManagmentPlan.BomaId 
                                          
                      where EnvironmentManagmentPlan.WorkCodeID like @workCode and EnvironmentManagmentPlan.BomaId = @intBomasid
					    
					  
					
					   
  end                  
end




