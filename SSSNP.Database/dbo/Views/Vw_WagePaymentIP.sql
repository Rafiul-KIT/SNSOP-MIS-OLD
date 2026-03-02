

CREATE VIEW [dbo].[Vw_WagePaymentIP]
AS
SELECT     ISNULL(COUNT(DISTINCT ba.intAttendanceid), 0) AS Cnt, a.intCountieid,a.intIP
FROM         dbo.tr_BenAttendanceDet AS ba INNER JOIN
                      dbo.tbl_BeneficiaryDet AS a ON a.intBenid = ba.intBenid INNER JOIN
                      dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid
WHERE     (ba.AttendanceDate BETWEEN w.FromDate AND w.ToDate) AND (ba.Status = 'Present')
GROUP BY a.intCountieid,a.intIP


