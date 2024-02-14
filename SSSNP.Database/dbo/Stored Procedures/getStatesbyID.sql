--created by suresh as on 06-1-2016        
--Purpose : to get States by id        
CREATE Procedure [dbo].[getStatesbyID]        
(        
@intStateid varchar(10)        
)        
as begin        
        
SELECT     NoofCounties, intStateid,StateName,Population, StateCode,Latitude,Longitude,Created_by,Created_dt    
FROM      dbo.tm_States    where intStateid like @intStateid        
        
end




