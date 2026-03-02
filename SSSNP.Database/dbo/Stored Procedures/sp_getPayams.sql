--created by siva as 30-11-2015    
--Purpose: to get Payams    
    
CREATE Procedure [dbo].[sp_getPayams]    
(    
@intCountieid varchar(10)    
)    
as begin    
    
select  intPayamid, intCountieid, PayamCode, upper(PayamName) as PayamName, Latitude, Longitude, Created_by, Created_dt from dbo.tm_Payams where intCountieid like @intCountieid    
    
end




