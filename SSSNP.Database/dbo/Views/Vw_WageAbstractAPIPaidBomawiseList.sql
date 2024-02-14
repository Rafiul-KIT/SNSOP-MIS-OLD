





CREATE VIEW [dbo].[Vw_WageAbstractAPIPaidBomawiseList]
AS
SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, dbo.tbl_WorkMaster.WorkCode, CONVERT(varchar(11), dbo.tr_BenWagesPayment.FromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.tr_BenWagesPayment.ToDate, 106) AS ToDate, ip.intIP, s.intStateid, c.intCountieid, p.intPayamid, b.intBomasid,wq.ReqNo,wq.intWagePaymentReqid
						 ,tbl_WorkMaster.intWorkid,tr_BenWagesPayment.FromDate as FDate,tr_BenWagesPayment.ToDate as Tdate
FROM            dbo.tr_BenWagesPayment INNER JOIN
                         dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.tr_BenWagesPayment.intWorkid INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = dbo.tbl_WorkMaster.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = dbo.tbl_WorkMaster.IPid
						 INNER JOIN td_WorkWageRequest as wq on wq.intWagePaymentReqid=tr_BenWagesPayment.uuid
WHERE        (dbo.tr_BenWagesPayment.uuid IS NOT NULL)







