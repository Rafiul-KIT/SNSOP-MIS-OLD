
CREATE procedure [dbo].[getWagePaidReportAbstractAPIBomaCyclewise] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
@workCode varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8) ,
@IPid varchar(8),
@ReqNo  varchar(20)
)  
as  
begin  
  
 -- Developer : Srikanth
-- Date : 14-03-2018
-- Purpose: Attendance Report New Format   



select    distinct  IPName,StateName, CountieName, PayamName, BomaName, FromDate, ToDate,FDate,TDate,intBomasid from Vw_WageAbstractAPIPaidBomawiseList
where intIP like @IPid and intStateid like @intStateid and intCountieid like @intCountieid and intPayamid like @intPayamid and intBomasid like @intBomasid 
--and WorkCode like @workCode 
order by IPName,StateName, CountieName, PayamName, BomaName, FromDate, ToDate




end



