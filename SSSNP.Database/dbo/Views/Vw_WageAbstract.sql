

CREATE VIEW [dbo].[Vw_WageAbstract]
AS
SELECT DISTINCT 
                         ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, dbo.tbl_WorkMaster.WorkCode, CONVERT(varchar(11), dbo.tr_BenWagesPayment.FromDate, 106) AS FromDate, CONVERT(varchar(11), 
                         dbo.tr_BenWagesPayment.ToDate, 106) AS ToDate, COUNT(DISTINCT dbo.tr_BenWagesPayment.intBenid) AS TotalMembers, SUM(dbo.tr_BenWagesPayment.AttendanceDays) AS TotalWorkDays, 
                         SUM(dbo.tr_BenWagesPayment.PaidAmountUSD) AS AmountPaidUSD, SUM(dbo.tr_BenWagesPayment.PaidAmount) AS AmountPaidSSP, ip.intIP, s.intStateid, c.intCountieid, p.intPayamid, b.intBomasid,Convert(varchar(11), MAx(tr_BenWagesPayment.PaidDate),106) as PaymentDate,
						 dbo.GetWageCoordinatorSenton(tr_BenWagesPayment.uuid,1101) as CoordinatorSenton,
 dbo.GetWageSpecilistSenton(tr_BenWagesPayment.uuid,1102) as SpecilaistSenton,
dbo.GetWageManagerSenton(tr_BenWagesPayment.uuid,1103) as Manageron
FROM            dbo.tr_BenWagesPayment INNER JOIN
                         dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.tr_BenWagesPayment.intWorkid INNER JOIN
                         dbo.tm_Bomas AS b ON b.intBomasid = dbo.tbl_WorkMaster.intBomasid INNER JOIN
                         dbo.tm_Payams AS p ON p.intPayamid = b.intPayamid INNER JOIN
                         dbo.tm_Counties AS c ON c.intCountieid = p.intCountieid INNER JOIN
                         dbo.tm_States AS s ON s.intStateid = c.intStateid INNER JOIN
                         dbo.td_IPDetails AS ip ON ip.intIP = dbo.tbl_WorkMaster.IPid
GROUP BY ip.IPName, s.StateName, c.CountieName, p.PayamName, b.BomaName, dbo.tbl_WorkMaster.WorkCode, CONVERT(varchar(11), dbo.tr_BenWagesPayment.FromDate, 106), CONVERT(varchar(11), 
                         dbo.tr_BenWagesPayment.ToDate, 106), ip.intIP, s.intStateid, c.intCountieid, p.intPayamid, b.intBomasid,dbo.tr_BenWagesPayment.uuid




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
         Begin Table = "tr_BenWagesPayment"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 263
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_WorkMaster"
            Begin Extent = 
               Top = 6
               Left = 301
               Bottom = 136
               Right = 518
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 556
               Bottom = 136
               Right = 742
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 6
               Left = 780
               Bottom = 136
               Right = 966
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 138
               Left = 262
               Bottom = 268
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ip"
            Begin Extent = 
               Top = 138
               Left = 486
               Bottom = 268
               Right = 740
            End
            DisplayFlags = 280
            T', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageAbstract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'opColumn = 0
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
      Begin ColumnWidths = 12
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageAbstract';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageAbstract';

