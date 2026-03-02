
CREATE procedure [dbo].[HouseHoldNumberVerificationAttedance]
(
@household_number varchar(120),
@attendance_date datetime,
@work_id varchar(8)
)
As
Begin
--Created by: Srikanth
--Created on; 24-03-2018
select * from tbl_BeneficiaryDet where household_number like @household_number

select * from tbl_WorkMaster where intWorkid like @work_id

select * from tr_BenAttendanceDet where intWorkActivityId like @work_id and intBenid in (select intBenid from tbl_BeneficiaryDet where household_number like @household_number) and Convert(varchar(11),AttendanceDate,103) like Convert(varchar(11),@attendance_date,103)

End

