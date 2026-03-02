CREATE Procedure [dbo].[WagespaidReport] (@intTST varchar(15),
@intIP varchar(15))
AS
BEGIN



  SELECT DISTINCT
    td_IPDetails.intIP,
    td_IPDetails.IPName,





    (SELECT
      COUNT(*) AS Expr1
    FROM dbo.tbl_WorkMaster
    WHERE (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP))

    AS TotalWorkProposed,


    (SELECT
      COUNT(*) AS Expr1
    FROM dbo.tbl_WorkMaster
    WHERE (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)
    AND (tbl_WorkMaster.Status LIKE 'Inprogress'
    OR tbl_WorkMaster.Status LIKE 'Invoice Appovals'
    OR tbl_WorkMaster.Status LIKE 'Invoice Request'
    OR tbl_WorkMaster.Status LIKE 'Approved'))

    AS TotalInprogress,




    --(SELECT     COUNT(*) AS Expr1                      
    --  FROM          dbo.tbl_WorkMaster                      
    --  WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)) -     (SELECT     COUNT(*) AS Expr1                      
    --  FROM          dbo.tbl_WorkMaster                      
    --  WHERE      (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP) and (tbl_WorkMaster.Status like 'Inprogress'))                      

    --    AS TotalInprogress ,      

    (SELECT
      COUNT(DISTINCT tbl_BeneficiaryDet.intBenid) AS Expr1
    FROM tbl_BeneficiaryDet INNER JOIN tr_BenfWorkDet  on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
    INNER JOIN tbl_WorkMaster 
      ON tbl_WorkMaster.intWorkid = tr_BenfWorkDet.intWorkid
    WHERE (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP))

    AS TotalBeneficiaries,
    
    (SELECT
      COUNT(DISTINCT tbl_BeneficiaryDet.intBenid) AS Expr1
    FROM tbl_BeneficiaryDet
    
    INNER JOIN tr_BenfWorkDet  on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
    INNER JOIN tbl_WorkMaster 
      ON tbl_WorkMaster.intWorkid = tr_BenfWorkDet.intWorkid
    WHERE (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP and tbl_BeneficiaryDet.Sex='M'))

    AS MaleBeneficiaries,
    
    (SELECT
      COUNT(DISTINCT tbl_BeneficiaryDet.intBenid) AS Expr1
    FROM tbl_BeneficiaryDet
    INNER JOIN tr_BenfWorkDet  on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
    INNER JOIN tbl_WorkMaster 
      ON tbl_WorkMaster.intWorkid = tr_BenfWorkDet.intWorkid
    WHERE (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP and tbl_BeneficiaryDet.Sex='F'))

    AS FemaleBeneficiaries,



    ISNULL((SELECT
      MAX(a.c)
    FROM ((SELECT
      (COUNT(DISTINCT tr_BenAttendanceDet.intAttendanceid)) AS c
    FROM tr_BenAttendanceDet
    INNER JOIN tbl_BeneficiaryDet
      ON tbl_BeneficiaryDet.intBenid = tr_BenAttendanceDet.intBenid

    WHERE (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP)
    AND tr_BenAttendanceDet.status LIKE 'Present'
    GROUP BY tr_BenAttendanceDet.intBenid
    )) AS a), 0) AS TotalWorkDays,

    --  (SELECT     COUNT(distinct tr_BenAttendanceDet.intBenid) AS Expr1                      
    --FROM tr_BenAttendanceDet    
    --inner join   tbl_BeneficiaryDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid    

    --WHERE      (tbl_BeneficiaryDet.intIP = dbo.td_IPDetails.intIP)    
    --and tr_BenAttendanceDet.Status like 'Present'    

    -- )                      

    --  AS TotalWorkDays ,    

    ISNULL((SELECT
      SUM(tr_BenWagesPayment.PaidAmount) AS Expr1
    FROM tr_BenWagesPayment
    INNER JOIN tbl_BeneficiaryDet
      ON tbl_BeneficiaryDet.intBenid = tr_BenWagesPayment.intBenid
      
   INNER JOIN tr_BenfWorkDet  on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
    INNER JOIN tbl_WorkMaster 
      ON tbl_WorkMaster.intWorkid = tr_BenfWorkDet.intWorkid
    WHERE (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)), 0)

    AS TotalWageAmt,
     ISNULL((SELECT
      SUM(tr_BenWagesPayment.PaidAmountUSD) AS Expr1
    FROM tr_BenWagesPayment
    INNER JOIN tbl_BeneficiaryDet
      ON tbl_BeneficiaryDet.intBenid = tr_BenWagesPayment.intBenid
      
   INNER JOIN tr_BenfWorkDet  on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid
    INNER JOIN tbl_WorkMaster 
      ON tbl_WorkMaster.intWorkid = tr_BenfWorkDet.intWorkid
    WHERE (tbl_WorkMaster.IPid = dbo.td_IPDetails.intIP)), 0)

    AS TotalWageAmt1


  FROM dbo.td_IPDetails
  WHERE td_IPDetails.intTST LIKE @intTST
  AND td_IPDetails.intIP LIKE @intIP


END



