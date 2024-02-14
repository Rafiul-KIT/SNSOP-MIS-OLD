CREATE Procedure[dbo].[InsertUpdateGroup_sp](
    @GroupId int,
	@State int ,
	@County int ,
	@Payam int ,
	@Boma int ,
	@GroupType varchar(100) ,
	@GroupCode varchar(100),
	@GroupStrength int,
	@Created_by varchar(10)
	)
	As 
	begin
	if((select COUNT(*) from GroupMaster where GroupId=@GroupId)>0)        
begin 
update GroupMaster
set State=@State,
County=@County,
Payam=@Payam,
Boma=@Boma,
GroupType=@GroupType,
GroupCode=@GroupCode,
GroupStrength=@GroupStrength,
Created_by=@Created_by,
Created_dt=GETDATE()
where GroupId=@GroupId

select @GroupId      
return @GroupId
end        
else  
      
begin
set @GroupId= (select isnull(Max(GroupId),0)+1 from GroupMaster) 
insert into GroupMaster(GroupId,State,County,Payam,Boma,GroupType,GroupCode,GroupStrength,Created_by,Created_dt)
values(@GroupId,@State,@County,@Payam,@Boma,@GroupType,@GroupCode,@GroupStrength,@Created_by,GETDATE())
select @GroupId      
return @GroupId
end
end



