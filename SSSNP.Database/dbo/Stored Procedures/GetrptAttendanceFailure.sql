
CREATE PROCEDURE [dbo].[GetrptAttendanceFailure] 
(@workid nvarchar(15), @householdNo nvarchar(500), @Date datetime, @ADate datetime)
AS
BEGIN
SELECT
  Household_Number,
  work_id,
  CONVERT(varchar(11), AttedanceDate, 106) AS AttedanceDate,
  CONVERT(varchar(11), PullDate, 106) AS PullDate,
  Failure_Message
FROM Beneficiary_API_Failure_Attedance
WHERE CONVERT(varchar(11), PullDate, 106) = CONVERT(varchar(11), @Date, 106)
AND household_number LIKE @householdNo
AND work_id LIKE @workid
AND CONVERT(varchar(11), AttedanceDate, 106) = CONVERT(varchar(11), @ADate, 106)
END
