
CREATE procedure [dbo].[getAttendanceReportNewFormat] -- '05/23/2016','05/30/2016',1015
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100)   
)  
as  
begin  

select distinct b.EnrollmentNo as [Enrollment Number]
,b.NameofHousehold as [Benficiary Name],
b.Age, b.Sex as Gender, b.SelectionCriteria as Component
, b.ContactNo as [Mobile Number],Convert(varchar(11),ba.AttendanceDate, 106) as [Date],ba.Status 
,cashp_FLI as 'Cash+ FLI'
,cashp_ECD as 'Cash+ ECD'
,cashP_WASH as 'Cash+ WASH'
 from 
 tr_BenAttendanceDet  as ba  
 inner join tbl_BeneficiaryDet as b 
 on b.intBenid= ba.intBenid   
where 
ba.AttendanceDate between @fromdate and @ToDate+1  
and intWorkActivityId like @intWorkActivityId 
and b.BeneficiaryType=(case when @group_type ='' then b.BeneficiaryType else @group_type end)
and isnull(b.GroupId,'')=(case when @group_code is null then isnull(b.GroupId,'')   else @group_code end) 
order by  b.EnrollmentNo asc

end