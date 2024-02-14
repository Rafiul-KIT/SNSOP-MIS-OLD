
CREATE VIEW [dbo].[Vw_CountrywiseReportLat]
AS
SELECT DISTINCT c.CountieName, c.intCountieid, ips.intIP, ips.IPName,
                          COUNT(DISTINCT w.intPayamid)  AS NoofPayams,
                           COUNT(DISTINCT w.intBomasid) AS NoofBomas,
                           COUNT(distinct w.intWorkid) AS TotalWorkProposed,
                          (SELECT     COUNT(tbl_WorkMaster_8.intWorkid) AS Expr1
                            FROM          dbo.tbl_WorkMaster AS tbl_WorkMaster_8
                            WHERE      (IPid = ips.intIP) AND (intCountieid = c.intCountieid) AND (Status LIKE 'New Proposal')) AS TotaltobeApproved,
                          (SELECT     COUNT(tbl_WorkMaster_7.intWorkid) AS Expr1
                            FROM          dbo.tbl_WorkMaster AS tbl_WorkMaster_7
                            WHERE      (IPid = ips.intIP) AND (intCountieid = c.intCountieid) AND (Status in ( 'Inprogress',
                                                  'Invoice Appovals' ,
                                                  'Invoice Request','Approved'))) AS TotalInprogress,
                          (SELECT     COUNT(tbl_WorkMaster_6.intWorkid) AS Expr1
                            FROM          dbo.tbl_WorkMaster AS tbl_WorkMaster_6
                            WHERE      (IPid = ips.intIP) AND (intCountieid = c.intCountieid) AND (Status LIKE 'Completed')) AS TotalClosed,
                          (SELECT     COUNT(tbl_WorkMaster_5.intWorkid) AS Expr1
                            FROM          dbo.tbl_WorkMaster AS tbl_WorkMaster_5
                            WHERE      (IPid = ips.intIP) AND (intCountieid = c.intCountieid) AND (Status LIKE 'Rejected')) AS TotalRejected,
                          (SELECT     COUNT(DISTINCT dbo.tr_FinancialApproval.intWorkId) AS Expr1
                            FROM          dbo.tr_FinancialApproval INNER JOIN
                                                   dbo.tbl_WorkMaster AS tbl_WorkMaster_4 ON tbl_WorkMaster_4.intWorkid = dbo.tr_FinancialApproval.intWorkId
                            WHERE      (tbl_WorkMaster_4.IPid = ips.intIP) AND (tbl_WorkMaster_4.intCountieid = c.intCountieid)) AS TotalFundsDisbursed,
                          (SELECT     COUNT(DISTINCT dbo.tbl_BeneficiaryDet.intBenid) AS Expr1
                            FROM          dbo.tbl_BeneficiaryDet INNER JOIN
                                                   dbo.tr_BenfWorkDet ON dbo.tr_BenfWorkDet.intBenid = dbo.tbl_BeneficiaryDet.intBenid INNER JOIN
                                                   dbo.tbl_WorkMaster AS tbl_WorkMaster_3 ON tbl_WorkMaster_3.intWorkid = dbo.tr_BenfWorkDet.intWorkid
                            WHERE      (dbo.tbl_BeneficiaryDet.intIP = ips.intIP) AND (dbo.tbl_BeneficiaryDet.intCountieid = c.intCountieid)) AS TotalBeneficiaries,
                          (SELECT     COUNT(DISTINCT tbl_BeneficiaryDet_2.intBenid) AS Expr1
                            FROM          dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_2 INNER JOIN
                                                   dbo.tr_BenfWorkDet AS tr_BenfWorkDet_2 ON tr_BenfWorkDet_2.intBenid = tbl_BeneficiaryDet_2.intBenid INNER JOIN
                                                   dbo.tbl_WorkMaster AS tbl_WorkMaster_2 ON tbl_WorkMaster_2.intWorkid = tr_BenfWorkDet_2.intWorkid
                            WHERE      (tbl_BeneficiaryDet_2.intIP = ips.intIP) AND (tbl_BeneficiaryDet_2.intCountieid = c.intCountieid) AND (tbl_BeneficiaryDet_2.Sex LIKE 'F')) AS TotalFemaleBeneficiaries,
                          (SELECT     COUNT(DISTINCT tbl_BeneficiaryDet_1.intBenid) AS Expr1
                            FROM          dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_1 INNER JOIN
                                                   dbo.tr_BenfWorkDet AS tr_BenfWorkDet_1 ON tr_BenfWorkDet_1.intBenid = tbl_BeneficiaryDet_1.intBenid INNER JOIN
                                                   dbo.tbl_WorkMaster AS tbl_WorkMaster_1 ON tbl_WorkMaster_1.intWorkid = tr_BenfWorkDet_1.intWorkid
                            WHERE      (tbl_BeneficiaryDet_1.intIP = ips.intIP) AND (tbl_BeneficiaryDet_1.intCountieid = c.intCountieid) AND (tbl_BeneficiaryDet_1.Sex LIKE 'M')) AS TotalmaleBeneficiaries, ISNULL
                          ((SELECT     wg.Cnt
                              FROM         dbo.Vw_WagePaymentIP AS wg
                              WHERE     (wg.intCountieid = c.intCountieid) AND (wg.intIP = ips.intIP)), 0) AS TotalWorkDays, ISNULL
                          ((SELECT     wp.cnt
                              FROM         dbo.Vw_WagesPaidIP AS wp
                              WHERE     (wp.intCountieid = c.intCountieid) AND (wp.intIP = ips.intIP)), 0) AS TotalWageAmt, ISNULL
                          ((SELECT     wg.Cnt
                              FROM         dbo.Vw_WagePayment AS wg
                              WHERE     (wg.intCountieid = c.intCountieid)), 0) AS NoofWorkdaysCreated, ISNULL
                          ((SELECT     wp1.cnt
                              FROM         dbo.Vw_WagesPaid AS wp1
                              WHERE     (wp1.intCountieid = c.intCountieid)), 0) AS TotalWageAmount
FROM         dbo.tm_Counties AS c INNER JOIN
                      dbo.tbl_WorkMaster AS w ON w.intCountieid = c.intCountieid INNER JOIN
                      dbo.td_IPDetails AS ips ON ips.intIP = w.IPid group by c.CountieName, c.intCountieid, ips.intIP, ips.IPName

