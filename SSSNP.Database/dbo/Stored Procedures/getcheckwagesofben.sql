 CREATE Procedure [dbo].[getcheckwagesofben]  
 @id varchar(8),    
@Attendancedate datetime    
 as begin  
   
 select *  from   tr_BenWagesPayment where intBenid like @id and (@Attendancedate between FromDate and ToDate ) 
 
 select intAttendanceid  from   tr_BenAttendanceDet where intBenid like @id and Convert(varchar(11),AttendanceDate,106)=Convert(varchar(11),@Attendancedate,106) and WageApplyFlag is not null
  
   
 end
