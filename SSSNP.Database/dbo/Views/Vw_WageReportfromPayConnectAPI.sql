








CREATE VIEW [dbo].[Vw_WageReportfromPayConnectAPI]
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

						 --dbo.GetPaidto(ba.Paidto,b.intBenid) as PaidToName,
						case when b.household_number=ba.Paidto then   b.NameofHousehold else case when b.alternate_number=ba.Paidto then b.Name1stAlternatePayee else case when b.Secondalternate_number=ba.Paidto then b.Name2ndAlternatePayee else b.NameofHousehold end end end as PaidToName,
						 
						 'Paid' as PayStatus, ba.Paidto,b.household_number,
						 --dbo.GetRequestnumber(ba.uuid)
						 	Convert(varchar(4),Year(getdate()))+'-SNSDPWR-'+ ba.uuid
						 
						 as WageRequestNumber

FROM            dbo.tr_BenWagesPayment AS ba INNER JOIN
                         dbo.tbl_BeneficiaryDet AS b ON b.intBenid = ba.intBenid INNER JOIN
                         dbo.tr_BenfWorkDet AS bw ON bw.intBenid = b.intBenid AND ba.intBenid = bw.intBenid AND bw.intWorkid = ba.intWorkid INNER JOIN
                         dbo.tbl_WorkMaster AS w ON w.intWorkid = ba.intWorkid AND w.intWorkid = bw.intWorkid INNER JOIN
                         dbo.tm_Bomas AS bom ON bom.intBomasid = w.intBomasid INNER JOIN
                         dbo.tm_Payams AS paym ON paym.intPayamid = bom.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = paym.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = w.IPid
WHERE        (b.household_number IS NOT NULL)










GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ba"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 223
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 261
               Bottom = 136
               Right = 533
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "bw"
            Begin Extent = 
               Top = 6
               Left = 571
               Bottom = 136
               Right = 756
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "w"
            Begin Extent = 
               Top = 6
               Left = 794
               Bottom = 136
               Right = 995
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "bom"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "paym"
            Begin Extent = 
               Top = 138
               Left = 246
               Bottom = 268
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 138
               Left = 454
               Bottom = 268
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         E', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageReportfromPayConnectAPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'nd
         Begin Table = "s"
            Begin Extent = 
               Top = 138
               Left = 662
               Bottom = 268
               Right = 832
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ip"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 276
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageReportfromPayConnectAPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageReportfromPayConnectAPI';

