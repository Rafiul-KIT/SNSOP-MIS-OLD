


CREATE VIEW [dbo].[Vw_WagepaidData]
AS
select  wr.intWagePaymentReqid, CONVERT(char(10), wr.ReqFromDate,126) as ReqFromDate,CONVERT(char(10), wr.ReqToDate,126) as ReqToDate,wr.Exchangerateattime,wr.WageRateUSD, wr.ReqNo, bw.intWagesReqBenTransid,  b.intBenid, b.EnrollmentNo,b.household_number,   w.intWorkid,w.WorkCode, bw.TotAttendance, bw.WageRateUSDTotal,'1' as Status,  CONVERT(char(10), wr.Created_dt,126)  as DateofCreation from td_WageReqBenDet as bw inner join tbl_BeneficiaryDet as b on b.intBenid=bw.intBenid inner join tbl_WorkMaster as w on w.intWorkid=bw.intWorkid

inner join td_WorkWageRequest as wr on wr.intWorkid=w.intWorkid 
inner join tr_BenWagesPayment as wp on wp.intBenid=b.intBenid and wp.intWorkid=w.intWorkid and Convert(varchar(11),wp.FromDate,106)=Convert(varchar(11),wr.ReqFromDate,106)
and Convert(varchar(11),wp.ToDate,106)=Convert(varchar(11),wr.ReqToDate,106)
where  b.household_number is not null





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
         Begin Table = "bw"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 255
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 6
               Left = 293
               Bottom = 136
               Right = 565
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "w"
            Begin Extent = 
               Top = 6
               Left = 603
               Bottom = 136
               Right = 804
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "wr"
            Begin Extent = 
               Top = 6
               Left = 842
               Bottom = 136
               Right = 1052
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "wp"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 247
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WagepaidData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'Vw_WagepaidData';

