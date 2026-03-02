
CREATE PROCEDURE [dbo].[getWageInvoicesDetails] --getWageInvoicesDetails '','','%','%','%','%','%','%' ,'1005'  
(@FromDate datetime,
@ToDate datetime,
@workCode varchar(120),
@RequestNo varchar(120),
@intStateid varchar(8),
@intCountieid varchar(8),
@intPayamid varchar(8),
@intBomasid varchar(8),
@IPid varchar(8))
AS
BEGIN
  SELECT DISTINCT
    intWagePaymentReqid,
    ip.IPName,
    s.StateName,
    c.CountieName,
    p.PayamName,
    b.BomaName,
    w.WorkCode,
    wr.ReqNo AS RequestNo,
    w.intWorkid,
    CONVERT(varchar(11), wr.ReqFromDate, 106) AS FromDate,
    CONVERT(varchar(11),
    wr.ReqToDate, 106) AS ToDate
  FROM dbo.tbl_WorkMaster AS w
  INNER JOIN dbo.tm_Bomas AS b
    ON b.intBomasid = w.intBomasid
  INNER JOIN dbo.tm_Payams AS p
    ON p.intPayamid = b.intPayamid
  INNER JOIN dbo.tm_Counties AS c
    ON c.intCountieid = p.intCountieid
  INNER JOIN dbo.tm_States AS s
    ON s.intStateid = c.intStateid
  INNER JOIN dbo.td_IPDetails AS ip
    ON ip.intIP = w.IPid
  INNER JOIN td_WorkWageRequest AS wr
    ON w.intWorkid = wr.intWorkid

  WHERE intIP LIKE @IPid
  AND w.intStateid LIKE @intStateid
  AND w.intCountieid LIKE @intCountieid
  AND w.intPayamid LIKE @intPayamid
  AND w.intBomasid LIKE @intBomasid
  AND WorkCode LIKE @workCode
  AND Wr.ReqNo LIKE @RequestNo
  ORDER BY IPName, StateName, CountieName, PayamName, BomaName, WorkCode, RequestNo, FromDate, ToDate
 
END
