
CREATE procedure [dbo].[getBenfWorkAttendanceDetailsService]
As
Begin
select * from Vw_BenAttendance
--select b.intBenid, b.EnrollmentNo,b.household_number,w.intWorkid,w.WorkCode, CONVERT(char(10),bw.AttendanceDate,126) as AttendanceDate,bw.Status as AttendanceStatus from tr_BenAttendanceDet as bw inner join tbl_BeneficiaryDet as b on b.intBenid=bw.intBenid inner join tbl_WorkMaster as w on w.intWorkid=bw.intWorkActivityId  where  b.household_number is not null

End

