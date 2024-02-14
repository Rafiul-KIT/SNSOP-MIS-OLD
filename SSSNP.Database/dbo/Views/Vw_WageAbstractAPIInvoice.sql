


CREATE VIEW [dbo].[Vw_WageAbstractAPIInvoice]
AS
SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, dbo.tbl_WorkMaster.WorkCode,td_WorkWageRequest.ReqNo as RequestNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106) AS ToDate, COUNT(DISTINCT dbo.td_WageReqBenDet.intBenid) AS TotalMembers, SUM(dbo.td_WageReqBenDet.TotAttendance) AS TotalWorkDays, 
                         SUM(dbo.td_WageReqBenDet.WageRateUSDTotal) AS AmountPaidUSD, ip.intIP, s.intStateid, c.intCountieid, p.intPayamid, b.intBomasid,
					case when 	td_WorkWageRequest.Status>=3 and td_WorkWageRequest.Status<=100  then td_WorkWageRequest.Exchangerateattime else '0' end as ExchangeRate,

				isnull(	(select aa.StageName from tm_WageStages as aa where aa.Stageid=td_WorkWageRequest.Status),'Contact Admin') as WageStatus, td_WorkWageRequest.intWagePaymentReqid
FROM            dbo.td_WorkWageRequest 
INNER JOIN td_WageReqBenDet on td_WageReqBenDet.intWagePaymentReqid= td_WorkWageRequest.intWagePaymentReqid INNER JOIN
                         dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.td_WorkWageRequest.intWorkid INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = dbo.tbl_WorkMaster.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = dbo.tbl_WorkMaster.IPid
      
GROUP BY ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, dbo.tbl_WorkMaster.WorkCode,td_WorkWageRequest.ReqNo, CONVERT(varchar(11), dbo.td_WorkWageRequest.ReqFromDate, 106), CONVERT(varchar(11), 
                         dbo.td_WorkWageRequest.ReqToDate, 106), ip.intIP, s.intStateid, c.intCountieid, p.intPayamid, b.intBomasid,td_WorkWageRequest.Exchangerateattime,td_WorkWageRequest.Status,td_WorkWageRequest.intWagePaymentReqid



