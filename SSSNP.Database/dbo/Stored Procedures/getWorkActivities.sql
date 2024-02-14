--created by siva as 11-1-2016
--Purpose: to getWorkActivities    
    
CREATE Procedure [dbo].[getWorkActivities]    
(    
@intAreaofWork varchar(10)    
)    
as begin    
    
select * from dbo.tm_WorkActivities where intAreaofWork like @intAreaofWork
    
end




