
CREATE VIEW [dbo].[Vw_WageInvoice]
AS
SELECT DISTINCT 
                      dbo.td_WageReqBenDet.intWagesReqBenTransid, dbo.td_WorkWageRequestPA.intWagePaymentReqPAid, dbo.td_WageReqBenDet.intWagePaymentReqid, 
                      dbo.tbl_BeneficiaryDet.intBenid, dbo.tbl_BeneficiaryDet.EnrollmentNo, CASE WHEN td_WageReqBenDet.traineealternate IS NOT NULL 
                      THEN td_WageReqBenDet.traineealternate ELSE tbl_BeneficiaryDet.NameofHousehold END AS NameofHousehold, dbo.tbl_BeneficiaryDet.Sex, 
                      dbo.tbl_BeneficiaryDet.ContactNo, dbo.tbl_BeneficiaryDet.Email, dbo.tbl_BeneficiaryDet.FamilyIncome, dbo.tm_Bomas.BomaName, 
                      dbo.td_WageReqBenDet.TotAttendance AS cnt, dbo.td_WageReqBenDet.TotAttendance * 70 AS Amt,tr_BenfWorkDet.intWorkid
FROM         dbo.tbl_BeneficiaryDet INNER JOIN
                      dbo.tr_BenfWorkDet ON dbo.tr_BenfWorkDet.intBenid = dbo.tbl_BeneficiaryDet.intBenid INNER JOIN
                      dbo.tbl_WorkMaster ON dbo.tbl_WorkMaster.intWorkid = dbo.tr_BenfWorkDet.intWorkid LEFT OUTER JOIN
                      dbo.tm_Bomas ON dbo.tm_Bomas.intBomasid = dbo.tbl_BeneficiaryDet.intBomasid INNER JOIN
                      dbo.td_WageReqBenDet ON dbo.td_WageReqBenDet.intBenid = dbo.tr_BenfWorkDet.intBenid AND 
                      dbo.td_WageReqBenDet.intWorkid = dbo.tr_BenfWorkDet.intWorkid INNER JOIN
                      dbo.td_WageReqBenPADet ON dbo.td_WageReqBenPADet.intWagePaymentReqid = dbo.td_WageReqBenDet.intWagePaymentReqid AND 
                      dbo.td_WageReqBenPADet.intBenid = dbo.td_WageReqBenDet.intBenid AND dbo.td_WageReqBenDet.intWorkid = dbo.td_WageReqBenPADet.intWorkid AND 
                      dbo.td_WageReqBenPADet.intBenid = dbo.tr_BenfWorkDet.intBenid AND dbo.td_WageReqBenPADet.intWorkid = dbo.tr_BenfWorkDet.intWorkid INNER JOIN
                      dbo.td_WorkWageRequestPA ON dbo.td_WorkWageRequestPA.intWagePaymentReqid = dbo.td_WageReqBenDet.intWagePaymentReqid


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
         Begin Table = "tbl_BeneficiaryDet"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 283
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tr_BenfWorkDet"
            Begin Extent = 
               Top = 6
               Left = 321
               Bottom = 114
               Right = 488
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tbl_WorkMaster"
            Begin Extent = 
               Top = 6
               Left = 526
               Bottom = 114
               Right = 712
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tm_Bomas"
            Begin Extent = 
               Top = 6
               Left = 750
               Bottom = 114
               Right = 901
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "td_WageReqBenDet"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 235
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "td_WageReqBenPADet"
            Begin Extent = 
               Top = 114
               Left = 273
               Bottom = 222
               Right = 483
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "td_WorkWageRequestPA"
            Begin Extent = 
               Top = 114
               Left = 521
               Bottom = 222
               Right', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 723
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WageInvoice';

