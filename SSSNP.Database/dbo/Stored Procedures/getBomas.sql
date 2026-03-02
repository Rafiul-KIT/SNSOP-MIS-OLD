--created by siva as 04-1-2016  
--Purpose: to get Bomas      
      
CREATE Procedure [dbo].[getBomas]      
(      
@intPayamid varchar(10)      
)      
as begin      
      
select  intBomasid, intPayamid, BomaCode,UPPER( BomaName) as BomaName, Latitude, Longitude, Created_by from dbo.tm_Bomas where intPayamid like @intPayamid  order by UPPER( BomaName)
      
end




