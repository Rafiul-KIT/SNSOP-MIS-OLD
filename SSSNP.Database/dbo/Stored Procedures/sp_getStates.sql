--created by siva as 30-11-2015    
--Purpose: to get states    
    
CREATE Procedure [dbo].[sp_getStates]    
    
as begin    
    
select intStateid, UPPER( StateName) as StateName, StateCode, Latitude, Longitude, Created_by, Created_dt from dbo.tm_States    
    
end




