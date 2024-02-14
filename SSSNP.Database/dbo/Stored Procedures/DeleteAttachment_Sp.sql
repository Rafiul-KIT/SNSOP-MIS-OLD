CREATE Procedure [dbo].[DeleteAttachment_Sp]
(@intTypeId int,@PhotoFileName varchar(50),@Type varchar(50))
as
begin
delete from tbl_Attachements
where intTypeId=@intTypeId
and PhotoFileName=@PhotoFileName
and Type=@Type

end



