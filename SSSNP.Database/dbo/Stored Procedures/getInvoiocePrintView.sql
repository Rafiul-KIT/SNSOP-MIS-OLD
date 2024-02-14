
CREATE procedure [dbo].[getInvoiocePrintView] 
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

SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName,tbl_WorkMaster.intWorkid, dbo.tbl_WorkMaster.WorkCode, CONVERT(varchar(11), @FromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         @ToDate, 106) AS ToDate, ip.intIP, s.intStateid, c.intCountieid, p.intPayamid, b.intBomasid
						 ,tbl_WorkMaster.intWorkid
FROM            dbo.tr_BenWagesPayment INNER JOIN
                         dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.tr_BenWagesPayment.intWorkid INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = dbo.tbl_WorkMaster.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = dbo.tbl_WorkMaster.IPid
						
WHERE        (dbo.tr_BenWagesPayment.uuid IS NOT NULL)
and intIP like @IPid and s.intStateid like @intStateid and c.intCountieid like @intCountieid and p.intPayamid like @intPayamid and b.intBomasid like @intBomasid 
and WorkCode like @workCode
and tr_BenWagesPayment.PaidDate between @FromDate and @ToDate+1

order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode








 
--select      IPName,StateName, CountieName, PayamName, BomaName, WorkCode, intWagePaymentReqid, ReqNo, FromDate, ToDate from Vw_WageAbstractAPIPaidList
--where intIP like @IPid and intStateid like @intStateid and intCountieid like @intCountieid and intPayamid like @intPayamid and intBomasid like @intBomasid 
--and WorkCode like @workCode and ReqNo like @ReqNo
--order by IPName,StateName, CountieName, PayamName, BomaName, WorkCode,ReqNo, FromDate, ToDate




end



