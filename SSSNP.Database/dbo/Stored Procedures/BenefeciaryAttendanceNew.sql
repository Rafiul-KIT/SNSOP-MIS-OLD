CREATE Procedure [dbo].[BenefeciaryAttendanceNew]      
(      
      
@WorkCode varchar(100),      
@EnrollmentNo varchar(100),      
@AttendanceDate datetime,      
@Status varchar(10),      
@Created_by int      
      
       
)      
as begin    


  Declare @WorkId int
  Declare @intBenid int
  set @WorkId=0
  set @intBenid=0
  set @WorkId=(select isnull(intWorkid,0) from tbl_WorkMaster where WorkCode =@WorkCode)
  set @intBenid=(select isnull(intBenid,0) from tbl_BeneficiaryDet where EnrollmentNo =@EnrollmentNo)

  Declare @IPid varchar(8)
  set @IPid=(select isnull(User_code,0) from tbl_Users where intUserid =@Created_by)

    Declare @IPidB  varchar(8)
  set @IPidB=(select isnull(intIP,0) from tbl_BeneficiaryDet where EnrollmentNo =@EnrollmentNo)

    Declare @IPidW  varchar(8)
  set @IPidW=(select isnull(IPid,0) from tbl_WorkMaster where WorkCode =@WorkCode)

if((select COUNT(*) from tr_BenAttendanceDet where intBenid like @intBenid and Convert(varchar(11), AttendanceDate,106)=Convert(varchar(11),@AttendanceDate,106))>0)    
begin    
    
if((select COUNT(*)  from tr_BenAttendanceDet where intBenid like @intBenid and Convert(varchar(11), AttendanceDate,106)=Convert(varchar(11),@AttendanceDate,106) and  WageApplyFlag is not null )>0 ) 
   
begin  
  
--update  dbo.tr_BenAttendanceDet set  
-- Status=@Status,Modified_by=@Created_by,Modified_dt=GETDATE() ,intWorkActivityId=@WorkId 
--  where  intBenid=@intBenid and  AttendanceDate=@AttendanceDate      
    
select 988    
return 988    

    end
	else
	Begin
	 select 999    
return 999
	End
    
       

end    
else    
    begin   
	if(@IPid<>@IPidB)
	Begin
	select 977        
    return 977  
	ENd
	else  if(@IPid<>@IPidW)
	Begin
	select 966        
    return 966 
	End
	Else
	Begin
insert into dbo.tr_BenAttendanceDet (intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt)        

values(@WorkId, @intBenid, @AttendanceDate, @Status, @Created_by, GETDATE())        
        
select 1        
return 1    
End    
  end   
    
 
----  Declare @WorkId int
----  Declare @intBenid int
----  set @WorkId=0
----  set @intBenid=0
----  set @WorkId=(select isnull(intWorkid,0) from tbl_WorkMaster where WorkCode =@WorkCode)
----  set @intBenid=(select isnull(intBenid,0) from tbl_BeneficiaryDet where EnrollmentNo =@EnrollmentNo)

------if((select COUNT(*) from tr_BenAttendanceDet where intBenid like @intBenid and AttendanceDate=@AttendanceDate)>0)    
------begin    
    
------if((select COUNT(*)  from tr_BenWagesPayment where intBenid like @intBenid and (@AttendanceDate between FromDate and ToDate ) )=0 ) 
   
------begin  
  
--------update  dbo.tr_BenAttendanceDet set  
-------- Status=@Status,Modified_by=@Created_by,Modified_dt=GETDATE() ,intWorkActivityId=@WorkId 
--------  where  intBenid=@intBenid and  AttendanceDate=@AttendanceDate      
    
------select 999    
------return 999    

------    end
    
------    select 999    
------return 999    

----if((select COUNT(*) from tr_BenAttendanceDet where intBenid like @intBenid and Convert(varchar(11), AttendanceDate,106)=Convert(varchar(11),@AttendanceDate,106))>0)    
----begin    
    
----if((select COUNT(*)  from tr_BenAttendanceDet where intBenid like @intBenid and Convert(varchar(11), AttendanceDate,106)=Convert(varchar(11),@AttendanceDate,106) and  WageApplyFlag is not null )>0 ) 
   
----begin  
  
------update  dbo.tr_BenAttendanceDet set  
------ Status=@Status,Modified_by=@Created_by,Modified_dt=GETDATE() ,intWorkActivityId=@WorkId 
------  where  intBenid=@intBenid and  AttendanceDate=@AttendanceDate      
    
----select 988    
----return 988    

----    end
----	else
----	Begin
----	 select 999    
----return 999
----	End
    
     
----end    
----else    
----    begin    
----insert into dbo.tr_BenAttendanceDet (intWorkActivityId, intBenid, AttendanceDate, Status, Created_by, Created_dt)        

----values(@WorkId, @intBenid, @AttendanceDate, @Status, @Created_by, GETDATE())        
        
----select 1        
----return 1        
----  end   
  
 end 



