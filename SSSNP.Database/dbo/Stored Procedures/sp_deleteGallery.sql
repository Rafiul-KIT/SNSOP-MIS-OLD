CREATE Procedure  [dbo].[sp_deleteGallery]                              
  (                              
                              
@ids varchar(50) ,            
@Attendance_Date datetime                             
                             
                              
 )                              
AS                              
begin                              
set nocount on                              
                              
delete from tr_BenAttendanceDet where intBenid like @ids    and       
    
   AttendanceDate like @Attendance_Date  
   --convert(varchar,cast( AttendanceDate as datetime),103)   like   convert(varchar,cast( @Attendance_Date as datetime),103)                               
                              
                              
                              
                              
end




