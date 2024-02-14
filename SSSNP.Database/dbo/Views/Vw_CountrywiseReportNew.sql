
CREATE VIEW [dbo].[Vw_CountrywiseReportNew]
AS
SELECT DISTINCT c.CountieName, c.intCountieid, ips.intIP, ips.IPName,
                          (SELECT     COUNT(DISTINCT intPayamid) AS Expr1
                            FROM          dbo.tbl_WorkMaster as z
                            WHERE      z.intWorkid=w.intWorkid) AS NoofPayams,
                          (SELECT     COUNT(DISTINCT intBomasid) as Exp2
                            FROM          dbo.tbl_WorkMaster  as z
                            WHERE      z.intWorkid=w.intWorkid) AS NoofBomas,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.tbl_WorkMaster  as z
                            WHERE      z.intWorkid=w.intWorkid) AS TotalWorkProposed,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.tbl_WorkMaster as z
                            WHERE      z.intWorkid=w.intWorkid  AND (z.Status LIKE 'New Proposal')) AS TotaltobeApproved,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.tbl_WorkMaster as z
                            WHERE      z.intWorkid=w.intWorkid  AND (Status LIKE 'Inprogress' OR
                                                   Status LIKE 'Invoice Appovals' OR
                                                   Status LIKE 'Invoice Request' OR
                                                   Status LIKE 'Approved')) AS TotalInprogress,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.tbl_WorkMaster as z
                            WHERE      z.intWorkid=w.intWorkid  AND (Status LIKE 'Completed')) AS TotalClosed,
                          (SELECT     COUNT(*) AS Expr1
                            FROM          dbo.tbl_WorkMaster as z
                            WHERE      z.intWorkid=w.intWorkid  AND (Status LIKE 'Rejected')) AS TotalRejected,
                          (SELECT     COUNT(DISTINCT z.intWorkId) AS Expr1
                            FROM          dbo.tr_FinancialApproval as z
                            WHERE      z.intWorkid=w.intWorkid ) AS TotalFundsDisbursed,
                          (SELECT     COUNT(DISTINCT dbo.tbl_BeneficiaryDet.intBenid) AS Expr1
                            FROM          dbo.tbl_BeneficiaryDet INNER JOIN
                                                   dbo.tr_BenfWorkDet ON dbo.tr_BenfWorkDet.intBenid = dbo.tbl_BeneficiaryDet.intBenid   and    tr_BenfWorkDet.intWorkid=w.intWorkid      ) AS TotalBeneficiaries,
                          (SELECT     COUNT(DISTINCT b1.intBenid) AS Expr1
                            FROM          dbo.tbl_BeneficiaryDet AS b1 INNER JOIN
                                                   dbo.tr_BenfWorkDet AS tr_BenfWorkDet_2 ON tr_BenfWorkDet_2.intBenid = b1.intBenid 
                                                 and   tr_BenfWorkDet_2.intWorkid=w.intWorkid  AND (b1.Sex LIKE 'F')) AS TotalFemaleBeneficiaries,
                          (SELECT     COUNT(DISTINCT b2.intBenid) AS Expr1
                            FROM          dbo.tbl_BeneficiaryDet AS b2 INNER JOIN
                                                   dbo.tr_BenfWorkDet AS tr_BenfWorkDet_1 ON tr_BenfWorkDet_1.intBenid = b2.intBenid and tr_BenfWorkDet_1.intWorkid=w.intWorkid AND (b2.Sex LIKE 'M')) AS TotalmaleBeneficiaries, ISNULL
                          ((SELECT     Cnt
                              FROM         dbo.Vw_WagePaymentIP AS wg
                              WHERE     (intCountieid = c.intCountieid) AND (intIP = ips.intIP)), 0) AS TotalWorkDays, ISNULL
                          ((SELECT     cnt
                              FROM         dbo.Vw_WagesPaidIP AS wp
                              WHERE     (intCountieid = c.intCountieid) AND (intIP = ips.intIP)), 0) AS TotalWageAmt, ISNULL
                          ((SELECT     Cnt
                              FROM         dbo.Vw_WagePayment AS wg
                              WHERE     (intCountieid = c.intCountieid)), 0) AS NoofWorkdaysCreated, ISNULL
                          ((SELECT     cnt
                              FROM         dbo.Vw_WagesPaid AS wp1
                              WHERE     (intCountieid = c.intCountieid)), 0) AS TotalWageAmount
FROM         dbo.tm_Counties AS c INNER JOIN
                      dbo.tbl_WorkMaster AS w ON w.intCountieid = c.intCountieid INNER JOIN
                      dbo.td_IPDetails AS ips ON ips.intIP = w.IPid

