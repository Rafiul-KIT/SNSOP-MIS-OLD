CREATE Procedure [dbo].[getAttendanceDeletebywork] --'',''   
@id varchar(8),      
@Attendancedate datetime      
    
as begin    
    
    
select distinct tbl_BeneficiaryDet.*,tr_BenAttendanceDet.AttendanceDate,tm_Bomas.BomaName,tr_BenAttendanceDet.Status  from  tbl_BeneficiaryDet inner join tr_BenAttendanceDet on tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid    
left outer join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid         
 where tr_BenAttendanceDet.intWorkActivityId like @id and (AttendanceDate between @Attendancedate and @Attendancedate)
 --CONVERT(varchar(11), AttendanceDate,106) like  CONVERT(varchar(11), @Attendancedate  ,106   )
 order by EnrollmentNo    
     
 end



