
CREATE procedure [dbo].[getGrievanceReport] (@intIP VarChar(15),
@FromDate date,
@ToDate date)
AS
BEGIN

  IF (@FromDate IS NULL
    AND @ToDate IS NULL)
  BEGIN
    SELECT DISTINCT
      td_IPDetails.intIP,
      td_IPDetails.IPName,
      tm_States.StateName,
      tm_Counties.CountieName,
      tbl_Grievance.intBomasid,
      tm_Payams.PayamName,
      tm_Bomas.BomaName,cast(@FromDate as date) as FDate,@ToDate as TDate, 
      COUNT(GrievanceId) AS NoOfReg

    FROM tbl_Grievance
    INNER JOIN tbl_Users
      ON tbl_Users.intUserid = tbl_Grievance.Created_by
    INNER JOIN td_IPDetails
      ON td_IPDetails.intIP = tbl_Users.User_code
    INNER JOIN tm_States
      ON tm_States.intStateid = tbl_Grievance.intStateid
    INNER JOIN tm_Counties
      ON tm_Counties.intCountieid = tbl_Grievance.intCountieid
    INNER JOIN tm_Payams
      ON tm_Payams.intPayamid = tbl_Grievance.intPayamid
    INNER JOIN tm_Bomas
      ON tm_Bomas.intBomasid = tbl_Grievance.intBomasid
    WHERE td_IPDetails.intIP LIKE @intIP
    GROUP BY tbl_Grievance.intBomasid,
             td_IPDetails.intIP,
             td_IPDetails.IPName,
             tm_Payams.PayamName,
             tm_Bomas.BomaName,
             tm_Counties.CountieName,
             tm_States.StateName,
             tm_Bomas.intBomasid
			 ORDER By
			 COUNT(GrievanceId)  desc

  END
  ELSE
  BEGIN
    SELECT DISTINCT
      td_IPDetails.intIP,
      td_IPDetails.IPName,
      tm_States.StateName,
      tm_Counties.CountieName,
      tbl_Grievance.intBomasid,
      tm_Payams.PayamName,
      tm_Bomas.BomaName,cast(@FromDate as date) as FDate,@ToDate as TDate, 
      COUNT(GrievanceId) AS NoOfReg

    FROM tbl_Grievance
    INNER JOIN tbl_Users
      ON tbl_Users.intUserid = tbl_Grievance.Created_by
    INNER JOIN td_IPDetails
      ON td_IPDetails.intIP = tbl_Users.User_code
    INNER JOIN tm_States
      ON tm_States.intStateid = tbl_Grievance.intStateid
    INNER JOIN tm_Counties
      ON tm_Counties.intCountieid = tbl_Grievance.intCountieid
    INNER JOIN tm_Payams
      ON tm_Payams.intPayamid = tbl_Grievance.intPayamid
    INNER JOIN tm_Bomas
      ON tm_Bomas.intBomasid = tbl_Grievance.intBomasid
    WHERE CONVERT(date, tbl_Grievance.ComplaintRegisteredDate) BETWEEN
    CONVERT(date, @FromDate) AND CONVERT(date, @ToDate)
    AND td_IPDetails.intIP LIKE @intIP
    GROUP BY tbl_Grievance.intBomasid,
             td_IPDetails.intIP,
             td_IPDetails.IPName,
             tm_Payams.PayamName,
             tm_Bomas.BomaName,
             tm_Counties.CountieName,
             tm_States.StateName,
             tm_Bomas.intBomasid
			 ORDER By
			 COUNT(GrievanceId) desc
  END
END
