
CREATE procedure [dbo].[getGrievanceReportDetails] (@intBomasid varchar(10),
@FromDate date,
@ToDate date)
AS
BEGIN

  IF (@FromDate IS NULL
    AND @ToDate IS NULL)
  BEGIN
    SELECT
      tbl_Grievance.Grievance_RegistrationNo,
      tbl_Grievance.Complainant_Name,
      tbl_Grievance.Complainant_EnrolmentNo,
      tbl_Grievance.TypeOfComplaints,
      tbl_Grievance.Accused_Name,
      tbl_Grievance.Accused_EnrolmentNo,
      tm_Payams.PayamName,
      tm_Bomas.BomaName,
      tm_Counties.CountieName,
      tm_States.StateName,
	  CONVERT(VARCHAR(10), ComplaintRegisteredDate, 103) as ComplaintRegisteredDate,
	  CONVERT(VARCHAR(10), AddressedDate, 103) as AddressedDate,
      tbl_Grievance.*,
      td_IPDetails.IPName


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


    WHERE tbl_Grievance.intBomasid LIKE @intBomasid
  END
  ELSE
  BEGIN


    SELECT
      tbl_Grievance.Grievance_RegistrationNo,
      tbl_Grievance.Complainant_Name,
      tbl_Grievance.Complainant_EnrolmentNo,
      tbl_Grievance.TypeOfComplaints,
      tbl_Grievance.Accused_Name,
      tbl_Grievance.Accused_EnrolmentNo,
      tm_Payams.PayamName,
      tm_Bomas.BomaName,
      tm_Counties.CountieName,
      tm_States.StateName,
	  CONVERT(VARCHAR(10), ComplaintRegisteredDate, 103) as ComplaintRegisteredDate,
	  CONVERT(VARCHAR(10), AddressedDate, 103) as AddressedDate,
      tbl_Grievance.*,
      td_IPDetails.IPName


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
    AND tbl_Grievance.intBomasid LIKE @intBomasid




  END
END
