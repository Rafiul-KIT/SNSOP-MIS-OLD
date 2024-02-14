CREATE Procedure [dbo].[InsUpdWorkProgress]        
(        
        
@intWorkid int,        
@WorkStartDate datetime,        
@Status varchar(10),        
@Remarks varchar(150),        
@Created_by int ,    
@Incharge varchar(50)       
        
         
)        
as begin        
if((select COUNT(*) from tbl_WorkProgress where intWorkid=@intWorkid)>0)        
begin        
        
update tbl_WorkProgress set  Incharge=@Incharge, WorkStartDate=@WorkStartDate, Status=@Status, Remarks=@Remarks, Modified_by=@Created_by, Modied_dt=GETDATE()      where intWorkid=@intWorkid     
  update tbl_WorkMaster set Status=@Status where intWorkid=@intWorkid  
 declare  @intWorkProgres int
 set @intWorkProgres=(select intWorkProgres from tbl_WorkProgress where intWorkid=@intWorkid)       
select @intWorkProgres        
return @intWorkProgres       
        
end        
else        
begin        
insert into tbl_WorkProgress (Incharge, intWorkid, WorkStartDate, Status, Remarks,  Created_by, Created_dt)        
values(@Incharge,@intWorkid, @WorkStartDate, @Status, @Remarks,  @Created_by, GETDATE())        
 update tbl_WorkMaster set Status=@Status where intWorkid=@intWorkid            
select @@IDENTITY        
return @@IDENTITY        
end        
end



