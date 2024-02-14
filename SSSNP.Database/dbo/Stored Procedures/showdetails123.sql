CREATE Procedure [dbo].[showdetails123]        
(          
  @FromDate datetime,            
  @ToDate datetime,            
  @intWorkActivityId varchar(10),    
  @Enrollment varchar(50)                       
 )             
as begin              
SELECT tbl_BeneficiaryDet.intBenid, tbl_BeneficiaryDet.EnrollmentNo,tbl_BeneficiaryDet.NameofHousehold,tbl_BeneficiaryDet.Sex,tbl_BeneficiaryDet.ContactNo, tr_BenAttendanceDet.Status,tr_BenAttendanceDet.AttendanceDate as Attendance_Date,tm_Bomas.BomaName 
  
    
      
             
FROM tbl_BeneficiaryDet              
INNER JOIN tr_BenAttendanceDet           
              
ON tbl_BeneficiaryDet.intBenid=tr_BenAttendanceDet.intBenid inner join tm_Bomas on tm_Bomas.intBomasid=tbl_BeneficiaryDet.intBomasid           
inner join tr_BenfWorkDet on tr_BenfWorkDet.intBenid=tbl_BeneficiaryDet.intBenid                inner join tbl_WorkMaster on tbl_WorkMaster.intWorkid=tr_BenfWorkDet.intWorkid           
where  (tr_BenAttendanceDet.AttendanceDate between         
@FromDate and @ToDate)  and tbl_WorkMaster.intWorkid like  @intWorkActivityId and tbl_BeneficiaryDet.EnrollmentNo like @Enrollment      
          
          
             
              
end




