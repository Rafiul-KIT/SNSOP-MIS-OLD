CREATE Procedure [dbo].[BenefeciaryAttendancebyID]  
(  
  
 
@intBenid int,  
@AttendanceDate datetime
  
   
)  
as begin  
  
select * from tr_BenAttendanceDet where intBenid like @intBenid and CONVERT(varchar(11), AttendanceDate,106)=CONVERT(varchar(11),@AttendanceDate,106)

end




