CREATE Procedure [dbo].[InsertAttachements_Sp]
(@Type varchar(50),@intTypeId int,@PhotoFileName varchar(500),@Created_by int,@FileName varchar(50),@IntWorkDetailsTypeId int)
as
begin
insert into tbl_Attachements(Type,intTypeId,File_Path,Created_by,Created_dt,PhotoFileName,IntWorkDetailsTypeId)
values(@Type,@intTypeId,@PhotoFileName,@Created_by,getdate(),@FileName,@IntWorkDetailsTypeId)
end



