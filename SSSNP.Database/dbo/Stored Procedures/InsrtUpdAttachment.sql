CREATE Procedure [dbo].[InsrtUpdAttachment]
(
@intTypeId int,
@Photo image,
@PhotoFileName varchar(50),
@Type varchar(50),
@Created_by int
)

as begin
if((select COUNT(*) from PWImages.dbo.tbl_Attachements where Type=@Type and intTypeId=@intTypeId)>0)
begin

update PWImages.dbo.tbl_Attachements set Photo=@Photo, PhotoFileName=@PhotoFileName,Modified_by=@Created_by,Modified_dt=GETDATE() 

where intTypeId like @intTypeId and Type=@Type

select @intTypeId
return @intTypeId

end
else 
begin
insert into PWImages.dbo.tbl_Attachements (Type, intTypeId, Photo, PhotoFileName, Created_by, Created_dt)
values(@Type, @intTypeId, @Photo, @PhotoFileName, @Created_by, GETDATE())
select @@IDENTITY
return @@IDENTITY

end
end




