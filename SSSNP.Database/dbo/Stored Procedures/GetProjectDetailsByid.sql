--created by siva as 11-1-2016            
--Purpose: to GetProjectDetails by ID      
                
CREATE Procedure [dbo].[GetProjectDetailsByid]                
(                
@intPrjid varchar(10)                
)                
as begin                
                
select * from dbo.td_ProjectDet where intPrjid like @intPrjid            
        
      
      
      
  SELECT     tm_WorkActivities.WorkActivity, tm_Bomas.BomaName, tm_Payams.PayamName, tm_States.StateName, tm_Counties.CountieName, tm_AreaofWork.AreaofWork,     
                      tr_ProjectTarget.*    
FROM         tr_ProjectTarget inner JOIN   
  
tm_States on tm_States.intStateid=tr_ProjectTarget.intStateid inner join  
tm_Counties on tm_Counties.intCountieid=tr_ProjectTarget.intCountieid left outer join   
tm_Payams on tm_Payams.intPayamid=tr_ProjectTarget.intPayamid left outer join   
tm_Bomas on tm_Bomas.intBomasid=tr_ProjectTarget.intBomasid left outer join   
tm_WorkActivities ON tr_ProjectTarget.intWorkActivity = tm_WorkActivities.intWorkActivity left outer JOIN    
                      tm_AreaofWork ON tm_WorkActivities.intAreaofWork = tm_AreaofWork.intAreaofWork    
            where tr_ProjectTarget.intPrjid  like @intPrjid            
end




