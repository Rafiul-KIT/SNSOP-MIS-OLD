CREATE Procedure [dbo].[BenefeciaryAttendance]      
(      
      
@WorkCode int,      
@intBenid int,      
@AttendanceDate datetime,      
@Status varchar(10),      
@Created_by int      
      
       
)      
as begin      
  --Commented by rushi    
--if((select COUNT(*) from tr_BenAttendanceDet where intBenid like @intBenid and AttendanceDate=@AttendanceDate)>0)    
--begin    
--select 999    
--return 999    
--end    
--else begin  


--Created by: Srikanth
--Created on: 06-01-2018
--Purpose : Update/Delte Attedance
 


--if((select COUNT(*) from tr_BenAttendanceDet where intBenid like @intBenid and AttendanceDate=@AttendanceDate)>0)    
--begin    
    
--if((select COUNT(*)  from tr_BenWagesPayment where intBenid like @intBenid and (@AttendanceDate between FromDate and ToDate ) )=0 ) 
   
--begin  
  
--update  dbo.tr_BenAttendanceDet set  
-- Status=@Status,Modified_by=@Created_by,Modified_dt=GETDATE() ,intWorkActivityId=@WorkCode 
--  where  intBenid=@intBenid and  AttendanceDate=@AttendanceDate      
    
--select 999    
--return 999    

--    end
    
--    select 999    
--return 999    

--end    
--else    
--    begin    
insert into dbo.tr_BenAttendanceDet (intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt)        

values(@WorkCode, @intBenid, @AttendanceDate, @Status, @Created_by, GETDATE())        
        
select 1        
return 1        
  --end   
  
 end 



