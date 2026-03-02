
CREATE PROCEDURE [dbo].[GetrptAttendanceFailureAPI] 

AS
BEGIN
SELECT
  Household_Number,
  work_id,
   CONVERT(char(10), AttedanceDate,126) as AttedanceDate,
  
  CONVERT(char(10), PullDate,126) as PullDate,
  Failure_Message
FROM Beneficiary_API_Failure_Attedance

END
