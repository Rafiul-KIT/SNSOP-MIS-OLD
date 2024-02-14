CREATE Procedure [dbo].[getAttendanceCount]  
(  
@Year varchar(10),  
@Month varchar(10),  
@intWorkActivityId varchar(10)   
)  
as  
begin  
  
select distinct tbl_BeneficiaryDet.intBenid,tbl_BeneficiaryDet.EnrollmentNo,  
tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo,tbl_BeneficiaryDet.Email,tbl_BeneficiaryDet.FamilyIncome,tm_Bomas.BomaName,  
(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where YEAR(AttendanceDate)=@Year and MONTH(AttendanceDate) = @Month and intWorkActivityId 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

and  MONTH(tr_BenAttendanceDet.AttendanceDate) =@Month and YEAR(tr_BenAttendanceDet.AttendanceDate) =@Year) as cnt,

(select ISNULL( COUNT(distinct tr_BenAttendanceDet.AttendanceDate),0) as Cnt from tr_BenAttendanceDet inner join tbl_BeneficiaryDet as a on a.intBenid=tr_BenAttendanceDet.intBenid where YEAR(AttendanceDate)=@Year and MONTH(AttendanceDate) = @Month and intWorkActivityId 
like @intWorkActivityId   
and tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid and tr_BenAttendanceDet.Status='Present'

and  MONTH(tr_BenAttendanceDet.AttendanceDate) =@Month and YEAR(tr_BenAttendanceDet.AttendanceDate) =@Year)*70 as Amt


 from dbo.tbl_BeneficiaryDet  inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid  left outer join    
tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid where tr_BenfWorkDet.intWorkid like @intWorkActivityId  

and tbl_BeneficiaryDet.intBenid in (select b.intBenid from tr_BenAttendanceDet as b)
end




