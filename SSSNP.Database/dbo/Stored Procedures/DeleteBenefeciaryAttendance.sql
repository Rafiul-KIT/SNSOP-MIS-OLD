CREATE Procedure [dbo].[DeleteBenefeciaryAttendance]   -- exec DeleteBenefeciaryAttendance '1114','01-03-2018','CST'
(@WorkCode varchar(8),
@AttendanceDate datetime,
@groupType varchar(10)
)

--Created by: Srikanth
--Created on: 06-01-2018
--Purpose : Update/Delte Attedance
As
Begin
Delete from tr_BenAttendanceDet 
where  intWorkActivityId like @WorkCode and  CONVERT(varchar(11), AttendanceDate ,103)=CONVERT(varchar(11), @AttendanceDate ,103)
and intBenid in (select intBenid from tbl_BeneficiaryDet where BeneficiaryType = @groupType)

select 1
return 1
End



