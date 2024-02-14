--created by siva as 30-11-2015    
--Purpose: to get Counties    
    
CREATE Procedure [dbo].[sp_getCountiesbyProjectid]    
(    
@intStateid varchar(10)  ,
@intProjectid varchar(10)  
)    
as begin    
    
select  intCountieid, intStateid, CountieCode,upper( CountieName) as CountieName, Latitude, Longitude, Created_by, Created_dt from dbo.tm_Counties where intStateid like @intStateid and intCountieid in
(
select a.intCountieid from tr_ProjectTarget as a where a.intPrjid like @intProjectid
)    
    
end




