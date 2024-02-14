CREATE Procedure [dbo].[getWorkActivitybyID]        
(        
@intWorkActivity varchar(10)        
)        
as begin        
        
SELECT tm_WorkActivities.*,dbo.tm_AreaofWork .intAreaofWork as Expr1,WorkActivity,tm_WorkActivities.WorkActivity,tm_WorkActivities.Created_by,tm_WorkActivities.Created_dt    
FROM   dbo.tm_WorkActivities   inner join dbo.tm_AreaofWork on dbo.tm_WorkActivities.intAreaofWork=dbo.tm_AreaofWork.intAreaofWork where dbo.tm_WorkActivities.intWorkActivity like @intWorkActivity        
        
end




