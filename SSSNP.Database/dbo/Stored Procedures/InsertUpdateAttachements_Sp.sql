CREATE Procedure [dbo].[InsertUpdateAttachements_Sp]
(@Type varchar(50),@intTypeId int,@PhotoFileName varchar(500),@Created_by int,@FileName varchar(50))
as
begin
if((select COUNT(*) from tbl_Attachements where Type=@Type and intTypeId=@intTypeId)>0)
begin
update tbl_Attachements
set File_Path=@PhotoFileName,
PhotoFileName=@FileName,
Modified_by=@Created_by,
Modified_dt=getdate()

end
else 
begin
insert into tbl_Attachements(Type,intTypeId,File_Path,Created_by,Created_dt,PhotoFileName)
values(@Type,@intTypeId,@PhotoFileName,@Created_by,getdate(),@FileName)
end
end



