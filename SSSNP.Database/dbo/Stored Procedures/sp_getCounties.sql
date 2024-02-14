--created by siva as 30-11-2015    
--Purpose: to get Counties    
    
CREATE Procedure [dbo].[sp_getCounties]    
(    
@intStateid varchar(10)    
)    
as begin    
    
select  intCountieid, intStateid, CountieCode,upper( CountieName) as CountieName, Latitude, Longitude, Created_by, Created_dt from dbo.tm_Counties where intStateid like @intStateid    
    
end




