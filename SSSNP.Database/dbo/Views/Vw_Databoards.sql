
CREATE VIEW [dbo].[Vw_Databoards]
AS
SELECT        IPName, intIP,
                             (SELECT        COUNT(DISTINCT intCountieid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet
                               WHERE        (intIP = dbo.td_IPDetails.intIP)) AS NoofCounties,
                             (SELECT        COUNT(DISTINCT intPayamid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_19
                               WHERE        (intIP = dbo.td_IPDetails.intIP)) AS NoofPayams,
                             (SELECT        COUNT(DISTINCT intBomasid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_18
                               WHERE        (intIP = dbo.td_IPDetails.intIP)) AS NoofBomas,
                             (SELECT        COUNT(DISTINCT intWorkid) AS Expr1
                               FROM            dbo.tbl_WorkMaster
                               WHERE        (IPid = dbo.td_IPDetails.intIP)) AS NoofWorksProposed,
                             (SELECT        COUNT(DISTINCT intWorkid) AS Expr1
                               FROM            dbo.tbl_WorkMaster AS tbl_WorkMaster_14
                               WHERE        (IPid = dbo.td_IPDetails.intIP) AND (Status LIKE 'New Proposal')) AS NoofWorkstobeapproved,
                             (SELECT        COUNT(DISTINCT intWorkid) AS Expr1
                               FROM            dbo.tbl_WorkMaster AS tbl_WorkMaster_13
                               WHERE        (IPid = dbo.td_IPDetails.intIP) AND (Status LIKE 'InProgress' OR
                                                         Status LIKE 'Invoice Appovals' OR
                                                         Status LIKE 'Invoice Request' OR
                                                         Status LIKE 'Approved')) AS NoofWorksinprogress,
                             (SELECT        COUNT(DISTINCT intWorkid) AS Expr1
                               FROM            dbo.tbl_WorkMaster AS tbl_WorkMaster_12
                               WHERE        (IPid = dbo.td_IPDetails.intIP) AND (Status LIKE 'Completed')) AS NoofWorksclosed,
                             (SELECT        COUNT(DISTINCT intWorkid) AS Expr1
                               FROM            dbo.tbl_WorkMaster AS tbl_WorkMaster_11
                               WHERE        (IPid = dbo.td_IPDetails.intIP) AND (Status LIKE 'Rejected')) AS NoofWorksRejected,
                             (SELECT        ISNULL(SUM(PaidAmount), 0) AS Expr1
                               FROM            dbo.tr_FinancialApproval
                               WHERE        (intWorkId IN
                                                             (SELECT DISTINCT WorkCode
                                                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_17
                                                               WHERE        (dbo.td_IPDetails.intIP = intIP)))) AS Fundsdisbursed,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_16.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_16 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_15 ON tr_BenfWorkDet_15.intBenid = tbl_BeneficiaryDet_16.intBenid
                               WHERE        (tbl_BeneficiaryDet_16.intIP = dbo.td_IPDetails.intIP)) AS TotalBenificiaries,
                             (SELECT        COUNT(tbl_BeneficiaryDet_15.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_15 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_14 ON tr_BenfWorkDet_14.intBenid = tbl_BeneficiaryDet_15.intBenid 
                               WHERE        (tbl_BeneficiaryDet_15.intIP = dbo.td_IPDetails.intIP)) AS TotalBenificiariesWMW,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_14.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_14 INNER JOIN
                                                             (SELECT DISTINCT intBenid, MAX(intWorkid) AS intWorkid
                                                               FROM            dbo.tr_BenfWorkDet AS tr_BenfWorkDet_13
                                                               GROUP BY intBenid) AS tr_BenfWorkDet ON tr_BenfWorkDet.intBenid = tbl_BeneficiaryDet_14.intBenid
                               WHERE        (tbl_BeneficiaryDet_14.intIP = dbo.td_IPDetails.intIP) AND (tbl_BeneficiaryDet_14.Sex LIKE 'F')) AS TotalFemaleBenificiaries,
                             (SELECT        COUNT(tbl_BeneficiaryDet_13.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_13 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_12 ON tr_BenfWorkDet_12.intBenid = tbl_BeneficiaryDet_13.intBenid 
                               WHERE        (tbl_BeneficiaryDet_13.intIP = dbo.td_IPDetails.intIP) AND (tbl_BeneficiaryDet_13.Sex LIKE 'F')) AS TotalFemaleBenificiariesWMW,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_12.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_12 INNER JOIN
                                                             (SELECT DISTINCT intBenid, MAX(intWorkid) AS intWorkid
                                                               FROM            dbo.tr_BenfWorkDet AS tr_BenfWorkDet_11
                                                               GROUP BY intBenid) AS tr_BenfWorkDet_16 ON tr_BenfWorkDet_16.intBenid = tbl_BeneficiaryDet_12.intBenid 
                               WHERE        (tbl_BeneficiaryDet_12.intIP = dbo.td_IPDetails.intIP) AND (tbl_BeneficiaryDet_12.Sex LIKE 'M')) AS TotalMaleBenificiaries,
                             (SELECT        COUNT(tbl_BeneficiaryDet_11.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_11 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_10 ON tr_BenfWorkDet_10.intBenid = tbl_BeneficiaryDet_11.intBenid 
                               WHERE        (tbl_BeneficiaryDet_11.intIP = dbo.td_IPDetails.intIP) AND (tbl_BeneficiaryDet_11.Sex LIKE 'M')) AS TotalMaleBenificiariesWMW,
                             (SELECT        ISNULL(COUNT(DISTINCT ba.intAttendanceid), 0) AS Cnt
                               FROM            dbo.tr_BenAttendanceDet AS ba INNER JOIN
                                                         dbo.tbl_BeneficiaryDet AS a ON a.intBenid = ba.intBenid INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_9 ON tr_BenfWorkDet_9.intBenid = a.intBenid 
														 --INNER JOIN
               --                                          dbo.tr_BenWagesPayment AS w ON w.intBenid = a.intBenid AND w.intWorkid = tr_BenfWorkDet_9.intWorkid
                               WHERE       (a.intIP = dbo.td_IPDetails.intIP) AND (ba.Status = 'Present')) AS NoofWorkdaysCreated,
                             (SELECT        ISNULL(SUM(PaidAmount), 0) AS Expr1
                               FROM            dbo.tr_BenWagesPayment
                               WHERE        (intBenid IN
                                                             (SELECT        intBenid
                                                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_10
                                                               WHERE        (dbo.td_IPDetails.intIP = intIP)))) AS TotalWageAmount,
                             (SELECT        ISNULL(SUM(PaidAmountUSD), 0) AS Expr1
                               FROM            dbo.tr_BenWagesPayment AS tr_BenWagesPayment_5
                               WHERE        (intBenid IN
                                                             (SELECT        intBenid
                                                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_9
                                                               WHERE        (dbo.td_IPDetails.intIP = intIP)))) AS TotalWageAmountUSD,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_8.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_8 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_8 ON tr_BenfWorkDet_8.intBenid = tbl_BeneficiaryDet_8.intBenid 
                               WHERE        (tbl_BeneficiaryDet_8.WorkCode IS NOT NULL) AND (tbl_BeneficiaryDet_8.intIP = dbo.td_IPDetails.intIP) AND (tbl_BeneficiaryDet_8.BeneficiaryType = 'Group')) AS Group_cnt,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_7.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_7 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_7 ON tr_BenfWorkDet_7.intBenid = tbl_BeneficiaryDet_7.intBenid 
                               WHERE        (tbl_BeneficiaryDet_7.WorkCode IS NOT NULL) AND (tbl_BeneficiaryDet_7.intIP = dbo.td_IPDetails.intIP) AND (tbl_BeneficiaryDet_7.BeneficiaryType = 'CST')) AS cst_cnt,
                             (SELECT        Param_Value
                               FROM            dbo.td_param) AS param_value,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_6.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_6 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_6 ON tr_BenfWorkDet_6.intBenid = tbl_BeneficiaryDet_6.intBenid 
                               WHERE        (tbl_BeneficiaryDet_6.WorkCode IS NOT NULL) AND (tbl_BeneficiaryDet_6.intIP = dbo.td_IPDetails.intIP) AND (YEAR(tbl_BeneficiaryDet_6.Created_dt) = 2016) AND (tbl_BeneficiaryDet_6.BeneficiaryType = 'Group')) 
                         AS Group_2016,
                             (SELECT        COUNT(DISTINCT tbl_BeneficiaryDet_5.intBenid) AS Expr1
                               FROM            dbo.tbl_BeneficiaryDet AS tbl_BeneficiaryDet_5 INNER JOIN
                                                         dbo.tr_BenfWorkDet AS tr_BenfWorkDet_5 ON tr_BenfWorkDet_5.intBenid = tbl_BeneficiaryDet_5.intBenid 
                               WHERE        (tbl_BeneficiaryDet_5.WorkCode IS NOT NULL) AND (tbl_BeneficiaryDet_5.intIP = dbo.td_IPDetails.intIP) AND (YEAR(tbl_BeneficiaryDet_5.Created_dt) = 2016) AND (tbl_BeneficiaryDet_5.BeneficiaryType = 'CST')) 
                         AS cst_2016,
                             (SELECT        Param_Value
                               FROM            dbo.td_param AS td_param_1) AS Expr1,
                             (SELECT        ISNULL(SUM(ProjectCost), 0) AS Expr1
                               FROM            dbo.td_ProjectDet
                               WHERE        (intIP = dbo.td_IPDetails.intIP)) AS ProjectCost,
                            (select AVG(isnull(WageRateUSD,0)) from tr_BenWagesPayment where intWorkid in (select w.intWorkid from tbl_WorkMaster as w where w.IPid=td_IPDetails.intIP )) AS Wage_Rate,
                           ( select AVG(isnull(tr_BenWagesPayment.ExchangeRate,0)) from tr_BenWagesPayment where intWorkid in (select w.intWorkid from tbl_WorkMaster as w where w.IPid=td_IPDetails.intIP))
                                AS Exchange_Rate,
                             (SELECT        ISNULL(SUM(f.Amount), 0) AS Expr1
                               FROM            dbo.tbl_FundsUtilization AS f INNER JOIN
                                                         dbo.tbl_WorkMaster AS w ON f.intWorkid = w.intWorkid
                               WHERE        (f.FundsUtilizationFor IN ('Skilled labour', 'Tools', 'Materials')) AND (w.IPid = dbo.td_IPDetails.intIP)) AS Total_Funds
FROM            dbo.td_IPDetails

