
CREATE PROCEDURE [dbo].[GetrptStateWise] (@intIPid varchar(8))
AS
BEGIN
  CREATE TABLE #tblWorksReqDet (
    StateName varchar(180),
    intStateid int,
    intWorkid int
  )
  INSERT INTO #tblWorksReqDet
    SELECT DISTINCT
      tm_States.StateName,
      tm_States.intStateid,
      intWorkid
    FROM tbl_WorkMaster
    INNER JOIN tm_States
      ON tm_States.intStateid = tbl_WorkMaster.intStateid
    WHERE tbl_WorkMaster.IPid LIKE @intIPid
    AND tbl_WorkMaster.intWorkid IN (SELECT
      a.intWorkid
    FROM td_WorkWageRequest AS a
    WHERE a.Status IN (4, 5, 6))
  CREATE TABLE #tblWageReq (
    intWorkid int,
    intWagePaymentReqid int,
    intWagesReqBenTransid int,
    intBenid int,
    TotAttendance int,
    WageRateUSDTotal decimal(18, 2),
    intStateid int
  )
  INSERT INTO #tblWageReq
    SELECT
      td_WorkWageRequest.intWorkid,
      td_WorkWageRequest.intWagePaymentReqid,
      td_WageReqBenDet.intWagesReqBenTransid,
      td_WageReqBenDet.intBenid,
      td_WageReqBenDet.TotAttendance,
      td_WageReqBenDet.WageRateUSDTotal,
      tbl_WorkMaster.intStateid
    FROM td_WageReqBenDet
    INNER JOIN td_WorkWageRequest
      ON td_WorkWageRequest.intWagePaymentReqid = td_WageReqBenDet.intWagePaymentReqid
    INNER JOIN tbl_WorkMaster
      ON tbl_WorkMaster.intWorkid = td_WageReqBenDet.intWorkid
      AND tbl_WorkMaster.intWorkid = td_WorkWageRequest.intWorkid
    WHERE td_WorkWageRequest.Status IN (4, 5, 6)
    AND tbl_WorkMaster.IPid LIKE @intIPid
  CREATE TABLE #tblWageReqPaid (
    intWorkid int,
    intWagePaymentReqid int,
    intWagesReqBenTransid int,
    intBenid int,
    TotAttendance int,
    WageRateUSDTotal decimal(18, 2),
    intStateid int
  )
  INSERT INTO #tblWageReqPaid
    SELECT
      td_WorkWageRequest.intWorkid,
      td_WorkWageRequest.intWagePaymentReqid,
      td_WageReqBenDet.intWagesReqBenTransid,
      td_WageReqBenDet.intBenid,
      tr_BenWagesPayment.AttendanceDays,
      tr_BenWagesPayment.PaidAmountUSD,
      tbl_WorkMaster.intStateid
    FROM td_WageReqBenDet
    INNER JOIN td_WorkWageRequest
      ON td_WorkWageRequest.intWagePaymentReqid = td_WageReqBenDet.intWagePaymentReqid
    INNER JOIN tr_BenWagesPayment
      ON tr_BenWagesPayment.uuid = td_WorkWageRequest.intWagePaymentReqid
      AND tr_BenWagesPayment.trans_id = td_WageReqBenDet.intWagesReqBenTransid
      AND tr_BenWagesPayment.uuid = td_WageReqBenDet.intWagePaymentReqid
    INNER JOIN tbl_WorkMaster
      ON tbl_WorkMaster.intWorkid = td_WageReqBenDet.intWorkid
      AND tbl_WorkMaster.intWorkid = tr_BenWagesPayment.intWorkid
      AND tbl_WorkMaster.intWorkid = td_WorkWageRequest.intWorkid
    WHERE td_WorkWageRequest.Status IN (4, 5, 6)
    AND tbl_WorkMaster.IPid LIKE @intIPid
  SELECT DISTINCT
    intStateid,
    StateName,
    (SELECT
      SUM(p.WageRateUSDTotal)
    FROM #tblWageReqPaid AS p
    WHERE p.intStateid = a.intStateid)
    AS TotalPaidAmount,
    ISNULL((SELECT
      SUM(p.WageRateUSDTotal)
    FROM #tblWageReq AS p
    WHERE p.intStateid = a.intStateid), 0) - ISNULL((SELECT
      SUM(p.WageRateUSDTotal)
    FROM #tblWageReqPaid AS p
    WHERE p.intStateid = a.intStateid), 0) AS TotaUnpaidlAmount,
    (SELECT
      SUM(p.WageRateUSDTotal)
    FROM #tblWageReq AS p
    WHERE p.intStateid = a.intStateid)
    AS TotalAmount,
    (SELECT
      COUNT(p.intBenid)
    FROM #tblWageReqPaid AS p
    WHERE p.intStateid = a.intStateid)
    AS TotalPaidBeneficiries,
    ISNULL((SELECT
      COUNT(p.intBenid)
    FROM #tblWageReq AS p
    WHERE p.intStateid = a.intStateid), 0) - ISNULL((SELECT
      COUNT(p.intBenid)
    FROM #tblWageReqPaid AS p
    WHERE p.intStateid = a.intStateid), 0) AS TotalUnpaidBeneficiries,
    (SELECT
      COUNT(p.intBenid)
    FROM #tblWageReq AS p
    WHERE p.intStateid = a.intStateid)
    AS TotalBeneficiries,
    (SELECT
      SUM(p.TotAttendance)
    FROM #tblWageReqPaid AS p
    WHERE p.intStateid = a.intStateid)
    AS TotalPaidAttendance,
    (SELECT
      SUM(p.TotAttendance)
    FROM #tblWageReq AS p
    WHERE p.intStateid = a.intStateid)
    AS TotalAttendance,
    ISNULL((SELECT
      SUM(p.TotAttendance)
    FROM #tblWageReq AS p
    WHERE p.intStateid = a.intStateid), 0) - ISNULL((SELECT
      SUM(p.TotAttendance)
    FROM #tblWageReqPaid AS p
    WHERE p.intStateid = a.intStateid), 0) AS TotalUnpaidAttendance
  FROM #tblWorksReqDet AS a
  DROP TABLE #tblWorksReqDet
  DROP TABLE #tblWageReq
  DROP TABLE #tblWageReqPaid
END
