CREATE Procedure [dbo].[InsertUpdWorkProposal]          
(          
@intWorkid int,        
@WorkCode varchar(50),        
@IPid int,        
@intPrjid int,        
@MeetingDate datetime,        
@intStateid int,        
@intCountieid int,        
@intPayamid int,        
@intBomasid int,        
        
@Status varchar(20),        
@Created_by int,      
      
@PDC_ApprvDate datetime,      
      
@StatusDate datetime,      
@Remarks varchar(150),  
@TypeofMeeting varchar(3)      
)          
as begin          
          
          
if(@intWorkid>0)          
begin          
if((select COUNT(*) from tbl_WorkMaster where WorkCode=@WorkCode and intWorkid<> @intWorkid)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
update tbl_WorkMaster set TypeofMeeting=@TypeofMeeting, Status=@Status,StatusDate=@StatusDate,PDC_ApprvDate=@PDC_ApprvDate,Remarks=@Remarks, MeetingDate=@MeetingDate, intStateid=@intStateid,intCountieid=@intCountieid,intPayamid=@intPayamid,intBomasid=@intBomasid,Modified_by= @Created_by,Modified_dt= GETDATE()          
where intWorkid like @intWorkid          
select 1          
return 1          
end          
end          
else          
begin          
if((select COUNT(*) from tbl_WorkMaster where WorkCode=@WorkCode)>0)          
begin          
select 999          
return 999          
end          
else          
begin          
insert into dbo.tbl_WorkMaster (intTSTid, TypeofMeeting,StatusDate,PDC_ApprvDate,Remarks, WorkCode, IPid, intPrjid, MeetingDate, intStateid, intCountieid, intPayamid, intBomasid,  Status,   Created_by, Created_dt)          
values((select top 1 intTST from dbo.td_IPDetails where intIP like @IPid), @TypeofMeeting,@StatusDate,@PDC_ApprvDate,@Remarks,@WorkCode, @IPid, @intPrjid, @MeetingDate, @intStateid, @intCountieid, @intPayamid, @intBomasid,  @Status,   @Created_by, GETDATE())          
select @@IDENTITY          
return @@IDENTITY          
end          
end          
end




