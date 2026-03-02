










CREATE VIEW [dbo].[Vw_WageReportfromPayConnectAPIUnPaid]
AS
SELECT DISTINCT 
                         ip.IPName AS IP, s.StateName AS [State Name], c.CountieName AS [County Name], paym.PayamName AS [Payam Name], bom.BomaName AS [Boma Name], w.WorkCode, b.EnrollmentNo AS [Enrollment Number], 
                         b.NameofHousehold AS [Benficiary Name], b.ContactNo AS [Mobile Number], CONVERT(varchar(11), ba.FromDate, 106) AS [From Date], CONVERT(varchar(11), ba.ToDate, 106) AS [To Date], 
                         ba.WageRateUSD AS [Wage Rate (In USD)], ba.PaidAmountUSD AS [Paid Amount (In USD)], ISNULL(CASE WHEN ba.AttendanceDays IS NOT NULL THEN ba.AttendanceDays ELSE
                             (SELECT        ISNULL(MAX(td_WageReqBenDet.TotAttendance), 0) AS Cnt
                               FROM            td_WageReqBenDet INNER JOIN
                                                         td_WorkWageRequest ON td_WorkWageRequest.intWagePaymentReqid = td_WageReqBenDet.intWagePaymentReqid
                               WHERE        CONVERT(varchar(11), td_WorkWageRequest.ReqFromDate, 106) = CONVERT(varchar(11), ba.FromDate, 106) AND CONVERT(varchar(11), td_WorkWageRequest.ReqToDate, 106) = CONVERT(varchar(11), 
                                                         ba.ToDate, 106) AND td_WorkWageRequest.intWorkid = ba.intWorkid AND td_WageReqBenDet.intWorkid = ba.intWorkid AND td_WageReqBenDet.intBenid = ba.intBenid) END, 0) AS AttendanceDays, 
                         CASE WHEN ba.ExchangeRate IS NULL THEN CONVERT(decimal(18, 2), ba.Wagerate / ba.WageRateUSD) ELSE ba.ExchangeRate END AS ExchangeRateInSSP, ba.PaidAmount AS [Paid Amount (In SSP)], w.WorkCode AS Expr1, 
                         bom.intBomasid, paym.intPayamid, c.intCountieid, s.intStateid, w.IPid,
						 Convert(varchar(11),ba.PaidDate,106) as WagePaiddate  ,  Convert(varchar(11),ba.PaidGenerateddate,106) as PaymentGenreatedDate, 

									--case when ba.Paidto is null then b.NameofHousehold else 	case when b.household_number=ba.Paidto then   b.NameofHousehold else case when b.alternate_number=ba.Paidto then b.Name1stAlternatePayee else case when b.Secondalternate_number=ba.Paidto then b.Name2ndAlternatePayee else b.NameofHousehold end end end end 
									
								''	as PaidToName,'UnPaid' as PayStatus,'' as Paidto,b.household_number,
								
								--dbo.GetRequestnumber(ba.uuid) 
								
									Convert(varchar(4),Year(getdate()))+'-SNSDPWR-'+ ba.uuid
								as WageRequestNumber

FROM            dbo.tr_BenWagesPaymentNotPaid AS ba INNER JOIN
                         dbo.tbl_BeneficiaryDet AS b ON b.intBenid = ba.intBenid INNER JOIN
                         dbo.tr_BenfWorkDet AS bw ON bw.intBenid = b.intBenid AND ba.intBenid = bw.intBenid AND bw.intWorkid = ba.intWorkid INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid AND w.intWorkid = bw.intWorkid INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
WHERE        (b.household_number IS NOT NULL)











