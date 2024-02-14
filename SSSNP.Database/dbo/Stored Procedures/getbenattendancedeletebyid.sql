CREATE Procedure [dbo].[getbenattendancedeletebyid]  
 @WorkCode varchar(8),  
 @intBenid varchar(8),  
 @AttendanceDate datetime  ,
 @Deletedby varchar(8)
 as begin 
 
 insert into  tr_BenAttendanceDetTrans
 (
  intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                      AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, Deletedby, DeletedDt

 )
 select  intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt, Modified_by, Modified_dt, 
                      AttendanceYear, PortYear, PortPhase, PortedSheetName, PaymentFlag, @Deletedby, getdate()
 from 
 tr_BenAttendanceDet where intBenid like @intBenid and (AttendanceDate between @AttendanceDate and @AttendanceDate) and intWorkActivityId like @WorkCode  

 Delete from tr_BenAttendanceDet where intBenid like @intBenid and (AttendanceDate between @AttendanceDate and @AttendanceDate) and intWorkActivityId like @WorkCode  
 select 1  
 return 1  
   
 end


 select * from tr_BenAttendanceDetTrans order by DeletedDt desc



