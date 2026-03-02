CREATE Procedure [dbo].[GetBenefeciaryAttendance]     -- GetBenefeciaryAttendance '1114','01-03-2018','02'
(@WorkCode varchar(8),
@AttendanceDate datetime,
@GroupCode varchar(8)

)

--Created by: Srikanth
--Created on: 06-01-2018
--Purpose : Update/Delte Attedance
As
Begin
select * from tr_BenAttendanceDet a
inner join tbl_BeneficiaryDet b on a.intBenid = b.intBenid
where  intWorkActivityId like @WorkCode and  CONVERT(varchar(11), AttendanceDate ,103)=CONVERT(varchar(11), @AttendanceDate ,103)
and b.GroupId like @GroupCode and b.BeneficiaryType = 'Group'

End



