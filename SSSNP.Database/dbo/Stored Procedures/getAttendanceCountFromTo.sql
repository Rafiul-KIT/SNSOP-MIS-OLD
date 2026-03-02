CREATE Procedure [dbo].[getAttendanceCountFromTo]  
(  
@FromDate datetime,  
@ToDate datetime,  
@intWorkActivityId varchar(10),
@group_type varchar(100),
@group_code varchar(100)   
)  
as  
begin  
  
select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  
(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between @FromDate and @ToDate) and intWorkActivityId 

like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'
and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
and  (AttendanceDate between @FromDate and @ToDate)) as cnt,

(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where (AttendanceDate between @FromDate and @ToDate ) and intWorkActivityId
 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'
and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
and  (AttendanceDate between @FromDate and @ToDate ))*70 as Amt


from dbo.tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid  left outer join    
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid where tr_BenfWorkDet.intWorkid like @intWorkActivityId  

and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b where (AttendanceDate between @FromDate and @ToDate ))
and tbl_BeneficiaryDet.BeneficiaryType=(case when @group_type ='' then tbl_BeneficiaryDet.BeneficiaryType else @group_type end)
and isnull(tbl_BeneficiaryDet.GroupId,'')=(case when @group_code is null then isnull(tbl_BeneficiaryDet.GroupId,'')   else @group_code end)
order by tbl_BeneficiaryDet.EnrollmentNo asc
end




