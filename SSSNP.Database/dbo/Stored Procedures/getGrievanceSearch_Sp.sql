CREATE PROCEDURE [dbo].[getGrievanceSearch_Sp] (@Grievance_RegistrationNo varchar(500),
@intStateid varchar(10),
@intCountieid varchar(10),
@intPayamid varchar(10),
@intBomasid varchar(10),
@IPID varchar(10),
@Complainant_Name varchar(500),
@Complainant_EnrolmentNo varchar(500),
@TypeOfComplaints varchar(500),
@Accused_Name varchar(500),
@Accused_EnrolmentNo varchar(500))
AS
BEGIN
 
  SELECT
    tbl_Grievance.*,
    tm_Payams.PayamName,
    tm_Bomas.BomaName,
    tm_Counties.CountieName,
    tm_States.StateName
 
  FROM tbl_Grievance
  INNER JOIN tm_States
    ON tm_States.intStateid = tbl_Grievance.intStateid
  INNER JOIN tm_Counties
    ON tm_Counties.intCountieid = tbl_Grievance.intCountieid
  INNER JOIN tm_Payams
    ON tm_Payams.intPayamid = tbl_Grievance.intPayamid
  INNER JOIN tm_Bomas
    ON tm_Bomas.intBomasid = tbl_Grievance.intBomasid

  WHERE tbl_Grievance.intStateid LIKE @intStateid
  AND tbl_Grievance.intCountieid LIKE @intCountieid
  AND tbl_Grievance.intPayamid LIKE @intPayamid
  AND tbl_Grievance.intBomasid LIKE @intBomasid
  AND isnull(tbl_Grievance.Grievance_RegistrationNo,'') LIKE '%' + @Grievance_RegistrationNo + '%'
  AND isnull(tbl_Grievance.Complainant_Name,'') LIKE '%' + @Complainant_Name + '%'
  AND isnull(tbl_Grievance.TypeOfComplaints,'') LIKE '%' + @TypeOfComplaints + '%'
  AND isnull(tbl_Grievance.Accused_Name,'') LIKE '%' + @Accused_Name + '%'
  AND isnull(tbl_Grievance.Complainant_EnrolmentNo,'') LIKE '%' + @Complainant_EnrolmentNo + '%'
  AND isnull(tbl_Grievance.Accused_EnrolmentNo,'') LIKE '%' + @Accused_EnrolmentNo + '%'
  AND tbl_Grievance.Created_by like @IPID


END