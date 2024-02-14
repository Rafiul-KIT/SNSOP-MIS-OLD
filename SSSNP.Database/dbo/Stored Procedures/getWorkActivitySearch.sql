CREATE Procedure [dbo].[getWorkActivitySearch]      
(      
@intAreaofWork varchar(50),      
@WorkActivity varchar(50)     
)      
as begin      
      
SELECT     tm_AreaofWork.intAreaofWork,tm_AreaofWork.AreaofWork, tm_WorkActivities.WorkActivity,  tm_WorkActivities.intWorkActivity   
FROM         tm_AreaofWork INNER JOIN      
                      tm_WorkActivities ON tm_AreaofWork.intAreaofWork = tm_WorkActivities.intAreaofWork   
                       where tm_AreaofWork.intAreaofWork like @intAreaofWork     
                       and  tm_WorkActivities.WorkActivity like '%'+@WorkActivity +'%'      
                            
                            
end




